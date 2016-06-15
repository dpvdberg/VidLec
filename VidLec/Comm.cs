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
            logger = LogManager.GetCurrentClassLogger();
            logger.Debug("Created logger");
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

        public bool Login(string username, string password, bool remember, bool saveCookie)
        {
            if (remember)
            {
                Properties.Settings.Default.Username = username;
                Properties.Settings.Default.Password = password;
                Properties.Settings.Default.Save();
            }

            AppConfig.AppInstance.username = username;
            AppConfig.AppInstance.password = password;

            bool loginValid =  SetLoginCookie(saveCookie);
            if (loginValid)
                ValidateCookie();
            return loginValid;
        }

        /// <summary>
        /// Finds and sets the catalog url in the AppConfig
        /// </summary>
        public bool SetCatalogURL()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(AppConfig.SiteData.baseURL);

            webRequest.Method = "GET";
            webRequest.AllowAutoRedirect = false;

            HttpWebResponse myResp = (HttpWebResponse)webRequest.GetResponse();
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
        /// <returns></returns>
        public bool ValidateCookie()
        {
            logger.Debug("Testing cookie..");
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(AppConfig.AppInstance.catalogURL);

            CookieContainer loginCookieContainer = new CookieContainer();
            Uri target = new Uri(AppConfig.AppInstance.catalogURL);
            loginCookieContainer.Add(new Cookie(AppConfig.SiteData.cookieFieldName,
                AppConfig.SiteData.GetCookieValue(
                    AppConfig.SiteData.cookieFieldName,
                    AppConfig.AppInstance.cookieData)) { Domain = target.Host});
            webRequest.CookieContainer = loginCookieContainer;
            webRequest.Method = "GET";
            webRequest.AllowAutoRedirect = false;

            HttpWebResponse myResp = (HttpWebResponse)webRequest.GetResponse();
            if (myResp.StatusCode == HttpStatusCode.OK)
            {
                using (var reader = new StreamReader(myResp.GetResponseStream(), ASCIIEncoding.ASCII))
                {
                    string pageData = reader.ReadToEnd();
                    if (pageData.Contains(AppConfig.SiteData.validCookieMagicKeyword))
                    {
                        logger.Info("Cookie valid, logged in!");
                        AppConfig.AppInstance.cookieValid = true;
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

            AppConfig.AppInstance.cookieValid = false;
            return false;
        }

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
        /// 
        /// </summary>
        /// <param name="saveCookie">Determines wheter the cookie is saved to the app user settings file</param>
        /// <returns></returns>
        private bool SetLoginCookie(bool saveCookie)
        {
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(AppConfig.SiteData.loginURL);
            byte[] postData = AppConfig.SiteData.GetLoginPostData();

            webRequest.Method = "POST";
            webRequest.ContentLength = postData.Length;
            webRequest.ContentType = AppConfig.SiteData.contentTypeHeader;
            // Make sure we do not redirect, cookie can be found in redirect
            // This will also improve performance
            webRequest.AllowAutoRedirect = false;

            using (var stream = webRequest.GetRequestStream())
            {
                stream.Write(postData, 0, postData.Length);
            }

            HttpWebResponse myResp = (HttpWebResponse)webRequest.GetResponse();
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
