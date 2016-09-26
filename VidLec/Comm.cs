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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VidLec
{
    class Comm
    {
        private Logger _logger;

        public Comm()
        {
            if (Properties.Settings.Default.LoggingEnable)
                _logger = LogManager.GetCurrentClassLogger();
        }

        #region Arbitrary comm related methods
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

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
        /// Login to the video lecture site.
        /// </summary>
        /// <param name="username">The users' username</param>
        /// <param name="password">The users' password</param>
        /// <param name="remember">Whether or not to save credentials</param>
        /// <param name="saveCookie">Whether or not to save cookies</param>
        /// <returns></returns>
        public bool Login(string username, string password, bool remember, bool saveCookie)
        {

            Properties.Settings.Default.Username = remember ? username : "";
            Properties.Settings.Default.Password = remember ? password : "";
            Properties.Settings.Default.Save();

            AppConfig.AppInstance.Username = username;
            AppConfig.AppInstance.Password = password;

            bool loginValid = SetLoginCookie(saveCookie);
            if (loginValid)
                loginValid = ValidateCookie();
            return loginValid;
        }

        /// <summary>
        /// Finds and sets the catalog url in the AppConfig
        /// </summary>
        public bool SetCatalogUrl(int tryCounter = 1)
        {
            if (tryCounter > AppConfig.Constants.ConnectionTries)
            {
                _logger.Debug("Could not find catalog URL, stopping..");
                return false;
            }
            if (tryCounter > 1)
                _logger.Debug($"Requesting catalog URL, try {tryCounter}");
            else
                _logger.Debug("Requesting catalog URL..");
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(AppConfig.SiteData.BaseUrl);

            webRequest.Method = "GET";
            webRequest.AllowAutoRedirect = false;
            webRequest.Timeout = AppConfig.Constants.ConnectionTimeout;

            HttpWebResponse myResp = null;
            try
            {
                myResp = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    _logger.Debug("Request timed out");
                    return SetCatalogUrl(tryCounter + 1);
                }
                throw;
            }
            if (myResp.StatusCode == HttpStatusCode.Redirect)
            {
                string[] redirectHeader = myResp.Headers.GetValues(AppConfig.SiteData.RedirectHeaderName);
                if (redirectHeader != null && redirectHeader.Length == 1)
                {
                    AppConfig.AppInstance.CatalogUrl = redirectHeader[0];
                    _logger.Debug($"Got catalog url: {redirectHeader[0]}");
                    return true;
                }
                _logger.Error("Login cookie header was not found");
            }
            else
                _logger.Error($"Login reponse was unexpected ({(int) myResp.StatusCode})");
            return false;
        }

        /// <summary>
        /// Checks if the cookie is valid to use as a log-in procedure.
        /// </summary>
        /// <returns>Validity of the cookie</returns>
        public bool ValidateCookie(bool useSavedCookie = false, int tryCounter = 1)
        {
            if (tryCounter > AppConfig.Constants.ConnectionTries)
            {
                _logger.Debug("Could not validate cookie, stopping..");
                return false;
            }
            if (tryCounter > 1)
                _logger.Debug($"Testing cookie, try {tryCounter}");
            else
                _logger.Debug("Testing cookie..");
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(AppConfig.AppInstance.CatalogUrl);

            CookieContainer loginCookieContainer = new CookieContainer();
            Uri target = new Uri(AppConfig.AppInstance.CatalogUrl);
            loginCookieContainer.Add(new Cookie(AppConfig.SiteData.CookieFieldName,
                AppConfig.SiteData.GetCookieValue(
                    AppConfig.SiteData.CookieFieldName,
                    useSavedCookie ? Properties.Settings.Default.LoginCookieData : AppConfig.AppInstance.CookieData)) { Domain = target.Host});
            webRequest.CookieContainer = loginCookieContainer;
            webRequest.Method = "GET";
            webRequest.AllowAutoRedirect = false;
            webRequest.Timeout = AppConfig.Constants.ConnectionTimeout;

            HttpWebResponse myResp;
            try {
                myResp = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    _logger.Debug("Request timed out");
                    return ValidateCookie(useSavedCookie, tryCounter + 1);
                }
                throw;
            }
            if (myResp.StatusCode == HttpStatusCode.OK)
            {
                using (var reader = new StreamReader(myResp.GetResponseStream(), ASCIIEncoding.ASCII))
                {
                    string pageData = reader.ReadToEnd();
                    if (pageData.Contains(AppConfig.SiteData.ValidCookieMagicKeyword))
                    {
                        if (useSavedCookie)
                            AppConfig.AppInstance.CookieData = Properties.Settings.Default.LoginCookieData;
                        _logger.Info("Cookie valid, logged in!");
                        if (GetCatalogId(pageData))
                            _logger.Debug("Found catalogId");
                        else
                            _logger.Error("Could not find catalogId");
                        return true;
                    }
                    _logger.Error("Response using cookie was unexpected");
                }
            }
            else if (myResp.StatusCode == HttpStatusCode.Redirect)
                _logger.Error("Saved cookie invalid, login attempt gave redirect (302)");
            else
                _logger.Error($"Login reponse was unexpected ({(int) myResp.StatusCode})");

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
                int beginIndex = pageData.IndexOf(AppConfig.SiteData.CatalogFieldName);
                int startIndex = pageData.IndexOf('\'', beginIndex) + 1;
                int stopIndex = pageData.IndexOf('\'', startIndex);
                string rawId = pageData.Substring(startIndex, stopIndex - startIndex);
                if (rawId != "")
                {
                    AppConfig.AppInstance.CatalogId = rawId;
                    return true;
                }
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
            if (tryCounter > AppConfig.Constants.ConnectionTries)
            {
                _logger.Debug("Could get catalog ID, stopping..");
                return "";
            }
            if (tryCounter > 1)
                _logger.Debug($"Getting catalog ID, try {tryCounter}");
            else
                _logger.Debug("Getting catalog ID..");
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(AppConfig.SiteData.CatalogDetailsUrl);
            byte[] postData = Encoding.UTF8.GetBytes(AppConfig.SiteData.GetCatalogRequestBody());

            CookieContainer loginCookieContainer = new CookieContainer();
            Uri target = new Uri(AppConfig.AppInstance.CatalogUrl);
            loginCookieContainer.Add(new Cookie(AppConfig.SiteData.CookieFieldName,
                AppConfig.SiteData.GetCookieValue(
                    AppConfig.SiteData.CookieFieldName,
                    AppConfig.AppInstance.CookieData))
            { Domain = target.Host });
            webRequest.CookieContainer = loginCookieContainer;
            webRequest.Method = "POST";
            webRequest.ContentLength = postData.Length;
            webRequest.ContentType = AppConfig.SiteData.RequestDataTypeHeader;
            webRequest.Timeout = AppConfig.Constants.ConnectionTimeout;

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
                    _logger.Debug("Request timed out");
                    return GetCatalogDetails(tryCounter + 1);
                }
                throw;
            }

            if (myResp.StatusCode == HttpStatusCode.OK)
                using (var reader = new StreamReader(myResp.GetResponseStream(), ASCIIEncoding.ASCII))
                    return reader.ReadToEnd();
            _logger.Error($"Unexpected response while receiving catalog details ({(int) myResp.StatusCode})");
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
            if (tryCounter > AppConfig.Constants.ConnectionTries)
            {
                _logger.Debug("Could not get cookie, stopping..");
                return false;
            }
            if (tryCounter > 1)
                _logger.Debug($"Getting cookie, try {tryCounter}");
            else
                _logger.Debug("Getting cookie..");
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(AppConfig.SiteData.LoginUrl);
            byte[] postData = AppConfig.SiteData.GetLoginPostData();

            webRequest.Method = "POST";
            webRequest.ContentLength = postData.Length;
            webRequest.ContentType = AppConfig.SiteData.LoginContentTypeHeader;
            // Make sure we do not redirect, cookie can be found in redirect
            // This will also improve performance
            webRequest.AllowAutoRedirect = false;
            webRequest.Timeout = AppConfig.Constants.ConnectionTimeout;

            try
            {
                using (var stream = webRequest.GetRequestStream())
                {
                    stream.Write(postData, 0, postData.Length);
                }
            }
            catch (Exception)
            {
                _logger.Debug("Could not get request stream");
                return SetLoginCookie(saveCookie, tryCounter + 1);
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
                    _logger.Debug("Request timed out");
                    return SetLoginCookie(saveCookie, tryCounter + 1);
                }
                throw;
            }
            if (myResp.StatusCode == HttpStatusCode.Redirect)
            {
                string[] setCookieHeader = myResp.Headers.GetValues(AppConfig.SiteData.CookieHeaderName);
                if (setCookieHeader != null)
                {
                    string loginCookie = setCookieHeader.FirstOrDefault(p => p.Contains(AppConfig.SiteData.CookieFieldName));
                    if (loginCookie == default(string))
                    {
                        _logger.Error("Login field in header was not found");
                    }
                    else
                    {
                        AppConfig.AppInstance.CookieData = loginCookie;
                        if (saveCookie)
                        {
                            Properties.Settings.Default.LoginCookieData = loginCookie;
                            Properties.Settings.Default.Save();
                        }
                        _logger.Debug($"Got loginCookie:\n{loginCookie}");
                        return true;
                    }
                }
                else
                    _logger.Error("Login cookie header was not found");
            }
            else
                _logger.Error($"Login reponse was unexpected ({(int) myResp.StatusCode})");
            return false;
        }

        public JObject GetPresentationsForFolder(Folder folder)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(AppConfig.SiteData.PresentationForFolderUrl);
            byte[] postData = Encoding.UTF8.GetBytes(AppConfig.SiteData.GetPresentationsForFolderRequestBody(folder));

            CookieContainer loginCookieContainer = new CookieContainer();
            Uri target = new Uri(AppConfig.AppInstance.CatalogUrl);
            loginCookieContainer.Add(new Cookie(AppConfig.SiteData.CookieFieldName,
                AppConfig.SiteData.GetCookieValue(
                    AppConfig.SiteData.CookieFieldName,
                    AppConfig.AppInstance.CookieData))
            { Domain = target.Host });
            webRequest.CookieContainer = loginCookieContainer;
            webRequest.Method = "POST";
            webRequest.ContentLength = postData.Length;
            webRequest.ContentType = AppConfig.SiteData.RequestDataTypeHeader;
            webRequest.Timeout = AppConfig.Constants.ConnectionTimeout;

            try
            {
                using (var stream = webRequest.GetRequestStream())
                {
                    stream.Write(postData, 0, postData.Length);
                }
            }
            catch (Exception)
            {
                _logger.Debug("Could not get request stream");
                return null;
            }

            HttpWebResponse myResp;
            try
            {
                myResp = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    _logger.Debug("Request timed out");
                    return null;
                }
                throw;
            }
            try
            {
                if (myResp.StatusCode == HttpStatusCode.OK)
                    using (var reader = new StreamReader(myResp.GetResponseStream(), ASCIIEncoding.ASCII))
                        return JObject.Parse(reader.ReadToEnd());
            }
            catch (Exception)
            {
                _logger.Error($"Unexpected response while receiving catalog details ({(int) myResp.StatusCode})");
            }
            return null;
        }

        public JObject GetPlayerOptionsForPresentation(Presentation presentation)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(AppConfig.SiteData.VideoForPresentationUrl);
            byte[] postData = Encoding.UTF8.GetBytes(AppConfig.SiteData.GetVideoForPresentationRequestBody(presentation));

            CookieContainer loginCookieContainer = new CookieContainer();
            Uri target = new Uri(AppConfig.AppInstance.CatalogUrl);
            loginCookieContainer.Add(new Cookie(AppConfig.SiteData.CookieFieldName,
                AppConfig.SiteData.GetCookieValue(
                    AppConfig.SiteData.CookieFieldName,
                    AppConfig.AppInstance.CookieData))
            { Domain = target.Host });
            webRequest.CookieContainer = loginCookieContainer;
            webRequest.Method = "POST";
            webRequest.ContentLength = postData.Length;
            webRequest.ContentType = AppConfig.SiteData.RequestDataTypeHeader;
            webRequest.Timeout = AppConfig.Constants.ConnectionTimeout;

            try
            {
                using (var stream = webRequest.GetRequestStream())
                {
                    stream.Write(postData, 0, postData.Length);
                }
            }
            catch (Exception)
            {
                _logger.Debug("Could not get request stream");
                return null;
            }

            HttpWebResponse myResp;
            try
            {
                myResp = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    _logger.Debug("Request timed out");
                    return null;
                }
                throw;
            }

            try
            {
                if (myResp.StatusCode == HttpStatusCode.OK)
                    using (var reader = new StreamReader(myResp.GetResponseStream(), ASCIIEncoding.ASCII))
                        return JObject.Parse(reader.ReadToEnd());
            }
            catch (Exception)
            {
                _logger.Error($"Unexpected response while receiving catalog details ({(int)myResp.StatusCode})");
            }
            return null;
        }
    }
}
