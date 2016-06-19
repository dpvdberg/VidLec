using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace VidLec
{
    class Comm
    {
        private Logger logger;

        public Comm()
        {
            if (Properties.Settings.Default.LoggingEnable)
                logger = LogManager.GetCurrentClassLogger();
        }

        #region Arbitrary comm related methods
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        /// <summary>
        /// Returns if the computer is connected to the internet
        /// </summary>
        /// <returns></returns>
        public bool CheckNet()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="remember"></param>
        /// <param name="saveCookie"></param>
        /// <returns></returns>
        public bool Login(string username, string password, bool remember, bool saveCookie)
        {

            Properties.Settings.Default.Username = remember ? username : "";
            Properties.Settings.Default.Password = remember ? password : "";
            Properties.Settings.Default.Save();

            AppConfig.AppInstance.username = username;
            AppConfig.AppInstance.password = password;

            bool loginValid = SetLoginCookie(saveCookie);
            if (loginValid)
                loginValid = ValidateCookie();
            return loginValid;
        }

        /// <summary>
        /// Finds and sets the catalog url in the AppConfig
        /// </summary>
        public bool SetCatalogURL(int tryCounter = 1)
        {
            if (tryCounter > 5)
            {
                logger.Debug("Could not find catalog URL, stopping..");
                return false;
            }
            else if (tryCounter > 1)
                logger.Debug(string.Format("Requesting catalog URL, try {0}", tryCounter));
            else
                logger.Debug("Requesting catalog URL..");
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(AppConfig.SiteData.baseURL);

            webRequest.Method = "GET";
            webRequest.AllowAutoRedirect = false;
            webRequest.Timeout = 5000;

            HttpWebResponse myResp = null;
            try
            {
                myResp = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    logger.Debug("Request timed out");
                    return SetCatalogURL(tryCounter + 1);
                }
                else throw;
            }
            if (myResp.StatusCode == HttpStatusCode.Redirect)
            {
                string[] redirectHeader = myResp.Headers.GetValues(AppConfig.SiteData.redirectHeaderName);
                if (redirectHeader != null && redirectHeader.Length == 1)
                {
                    AppConfig.AppInstance.catalogURL = redirectHeader[0];
                    logger.Debug(string.Format("Got catalog url: {0}", redirectHeader[0]));
                    return true;
                }
                else
                    logger.Error("Login cookie header was not found");
            }
            else
                logger.Error(string.Format("Login reponse was unexpected ({0})", (int)myResp.StatusCode));
            return false;
        }

        /// <summary>
        /// Checks if the cookie is valid to use as a log-in procedure
        /// </summary>
        /// <returns>Validity of the cookie</returns>
        public bool ValidateCookie(bool useSavedCookie = false, int tryCounter = 1)
        {
            if (tryCounter > 5)
            {
                logger.Debug("Could not validate cookie, stopping..");
                return false;
            }
            else if (tryCounter > 1)
                logger.Debug(string.Format("Testing cookie, try {0}", tryCounter));
            else
                logger.Debug("Testing cookie..");
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(AppConfig.AppInstance.catalogURL);

            CookieContainer loginCookieContainer = new CookieContainer();
            Uri target = new Uri(AppConfig.AppInstance.catalogURL);
            loginCookieContainer.Add(new Cookie(AppConfig.SiteData.cookieFieldName,
                AppConfig.SiteData.GetCookieValue(
                    AppConfig.SiteData.cookieFieldName,
                    useSavedCookie ? Properties.Settings.Default.LoginCookieData : AppConfig.AppInstance.cookieData)) { Domain = target.Host});
            webRequest.CookieContainer = loginCookieContainer;
            webRequest.Method = "GET";
            webRequest.AllowAutoRedirect = false;
            webRequest.Timeout = 5000;

            HttpWebResponse myResp = null;
            try {
                myResp = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    logger.Debug("Request timed out");
                    return ValidateCookie(useSavedCookie, tryCounter + 1);
                }
                else throw;
            }
            if (myResp.StatusCode == HttpStatusCode.OK)
            {
                using (var reader = new StreamReader(myResp.GetResponseStream(), ASCIIEncoding.ASCII))
                {
                    string pageData = reader.ReadToEnd();
                    if (pageData.Contains(AppConfig.SiteData.validCookieMagicKeyword))
                    {
                        if (useSavedCookie)
                            AppConfig.AppInstance.cookieData = Properties.Settings.Default.LoginCookieData;
                        logger.Info("Cookie valid, logged in!");
                        if (GetCatalogId(pageData))
                            logger.Debug("Found catalogId");
                        else
                            logger.Error("Could not find catalogId");
                        return true;
                    }
                    else
                        logger.Error("Response using cookie was unexpected");
                }
            }
            else if (myResp.StatusCode == HttpStatusCode.Redirect)
                logger.Error("Saved cookie invalid, login attempt gave redirect (302)");
            else
                logger.Error(string.Format("Login reponse was unexpected ({0})", (int)myResp.StatusCode));

            return false;
        }

        /// <summary>
        /// Finds catalog id in given page data
        /// </summary>
        /// <param name="pageData">Raw page data</param>
        /// <returns>CatalogId string</returns>
        private bool GetCatalogId(string pageData)
        {
            try {
                int beginIndex = pageData.IndexOf(AppConfig.SiteData.catalogFieldName);
                int startIndex = pageData.IndexOf('\'', beginIndex) + 1;
                int stopIndex = pageData.IndexOf('\'', startIndex);
                string rawId = pageData.Substring(startIndex, stopIndex - startIndex);
                if (rawId != "")
                {
                    AppConfig.AppInstance.catalogId = rawId;
                    return true;
                }
                else
                    return false;
            } catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the JSON-formatted catalog details from the web
        /// </summary>
        /// <returns>Raw catalog details</returns>
        public string GetCatalogDetails(int tryCounter = 1)
        {
            if (tryCounter > 5)
            {
                logger.Debug("Could get catalog ID, stopping..");
                return "";
            }
            else if (tryCounter > 1)
                logger.Debug(string.Format("Getting catalog ID, try {0}", tryCounter));
            else
                logger.Debug("Getting catalog ID..");
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(AppConfig.SiteData.catalogDetailsURL);
            byte[] postData = Encoding.UTF8.GetBytes(AppConfig.SiteData.GetCatalogRequestBody());

            CookieContainer loginCookieContainer = new CookieContainer();
            Uri target = new Uri(AppConfig.AppInstance.catalogURL);
            loginCookieContainer.Add(new Cookie(AppConfig.SiteData.cookieFieldName,
                AppConfig.SiteData.GetCookieValue(
                    AppConfig.SiteData.cookieFieldName,
                    AppConfig.AppInstance.cookieData))
            { Domain = target.Host });
            webRequest.CookieContainer = loginCookieContainer;
            webRequest.Method = "POST";
            webRequest.ContentLength = postData.Length;
            webRequest.ContentType = AppConfig.SiteData.catalogContentTypeHeader;
            webRequest.Timeout = 5000;

            using (var stream = webRequest.GetRequestStream())
            {
                stream.Write(postData, 0, postData.Length);
            }

            HttpWebResponse myResp = null;
            try
            {
                myResp = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    logger.Debug("Request timed out");
                    return GetCatalogDetails(tryCounter + 1);
                }
                else throw;
            }

            if (myResp.StatusCode == HttpStatusCode.OK)
                using (var reader = new StreamReader(myResp.GetResponseStream(), ASCIIEncoding.ASCII))
                    return reader.ReadToEnd();
            else
                logger.Error(string.Format("Unexpected response while receiving catalog details ({0})", (int) myResp.StatusCode));
            return "";
        }

        /// <summary>
        /// Tries to log in and finds the cookie header
        /// Can save the found cookie to the settings file
        /// </summary>
        /// <param name="saveCookie">Determines wheter the cookie is saved to the app user settings file</param>
        /// <returns>Whether a cookie is found or not</returns>
        private bool SetLoginCookie(bool saveCookie, int tryCounter = 1)
        {
            if (tryCounter > 5)
            {
                logger.Debug("Could not get cookie, stopping..");
                return false;
            }
            else if (tryCounter > 1)
                logger.Debug(string.Format("Getting cookie, try {0}", tryCounter));
            else
                logger.Debug("Getting cookie..");
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(AppConfig.SiteData.loginURL);
            byte[] postData = AppConfig.SiteData.GetLoginPostData();

            webRequest.Method = "POST";
            webRequest.ContentLength = postData.Length;
            webRequest.ContentType = AppConfig.SiteData.loginContentTypeHeader;
            // Make sure we do not redirect, cookie can be found in redirect
            // This will also improve performance
            webRequest.AllowAutoRedirect = false;
            webRequest.Timeout = 5000;

            using (var stream = webRequest.GetRequestStream())
            {
                stream.Write(postData, 0, postData.Length);
            }

            HttpWebResponse myResp = null;
            try
            {
                myResp = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    logger.Debug("Request timed out");
                    return SetLoginCookie(saveCookie, tryCounter + 1);
                }
                else throw;
            }
            if (myResp.StatusCode == HttpStatusCode.Redirect)
            {
                string[] setCookieHeader = myResp.Headers.GetValues(AppConfig.SiteData.cookieHeaderName);
                if (setCookieHeader != null)
                {
                    string loginCookie = setCookieHeader.FirstOrDefault(p => p.Contains(AppConfig.SiteData.cookieFieldName));
                    if (loginCookie == default(string))
                    {
                        logger.Error("Login field in header was not found");
                    }
                    else
                    {
                        AppConfig.AppInstance.cookieData = loginCookie;
                        if (saveCookie)
                        {
                            Properties.Settings.Default.LoginCookieData = loginCookie;
                            Properties.Settings.Default.Save();
                        }
                        logger.Debug(string.Format("Got loginCookie:\n{0}", loginCookie));
                        return true;
                    }
                }
                else
                    logger.Error("Login cookie header was not found");
            }
            else
                logger.Error(string.Format("Login reponse was unexpected ({0})", (int)myResp.StatusCode));
            return false;
        }
    }
}
