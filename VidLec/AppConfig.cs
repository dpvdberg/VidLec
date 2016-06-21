using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidLec
{
    class AppConfig
    {
        public static class AppInstance
        {
            public static LoginManager.LoginResult loginResult;
            public static bool onlineMode = false;
            public static string cookieData = "";
            public static string username = "";
            public static string password = "";

            public static string catalogURL = "";
            public static string catalogId = "";
            public static Folder rootFolder = null;
        }

        public static class AppChosenVariables
        {
            public static int catalogDetailsRetentionDays = 7;
        }

        public static class SiteData
        {
            /// <summary>
            /// URL constants
            /// </summary>
            public const string baseURL = "http://videocollege.tue.nl";
            public const string loginURL = baseURL + "/Mediasite/Login/";
            public const string catalogDetailsURL = baseURL + "/Mediasite/Catalog/Data/GetCatalogDetails";

            // Redirect (302) header name
            public const string redirectHeaderName = "Location";

            /// <summary>
            /// Login related constants
            /// </summary>
            public const string catalogContentTypeHeader = "application/json; charset=utf-8";
            public const string loginContentTypeHeader = "application/x-www-form-urlencoded";
            public const string cookieFieldName = "MediasiteAuth";
            public const string cookieHeaderName = "Set-Cookie";
            public const string validCookieMagicKeyword = "Authenticated=\"True\"";
            public const string catalogFieldName = "CatalogId";
            public static Dictionary<string, string> loginPostParameters = new Dictionary<string, string>()
            {
                { "UserName", AppInstance.username},
                { "Password", AppInstance.password},
                { "RememberMe", "false"}
            };

            /// <summary>
            /// JSON constants
            /// </summary>
            public const string jsonNavigationFoldersName = "NavigationFolders";

            public static byte[] GetLoginPostData()
            {
                string postData = "";

                loginPostParameters["UserName"] = AppInstance.username;
                loginPostParameters["Password"] = AppInstance.password;

                foreach (KeyValuePair<string, string> parameter in loginPostParameters)
                {
                    postData += string.Format("{0}={1}&", parameter.Key, parameter.Value);
                }

                return Encoding.ASCII.GetBytes(postData.Trim('&'));
            }

            public static string GetCookieValue(string cookieName, string data)
            {
                try {
                    string cookieKeyWord = cookieName + "=";
                    int startIndex = data.IndexOf(cookieKeyWord) + cookieKeyWord.Length;
                    return data.Substring(startIndex, data.IndexOf(';', startIndex) - startIndex);
                } catch (Exception)
                {
                    return "";
                }
            }

            public static string GetCatalogRequestBody()
            {
                JObject o = new JObject();
                o["CatalogId"] = AppInstance.catalogId;
                o["CurrentFolderId"] = AppInstance.catalogId;
                o["Url"] = AppInstance.catalogURL;
                return o.ToString();
            }
        }

        /// <summary>
        /// Constants
        /// Text, links, numbers & data
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// Numeric constants
            /// </summary>
            public const int connectionTries = 5;
            public const int connectionTimeout = 5000;

            /// <summary>
            /// Auxiliary
            /// </summary>
            public const string appName = "VidLec";
            public const string DateTimeFTM = "O";

            #region Text
            public const string loggingInText = "Logging in..";
            public const string loggedIn = "Logged in";
            public const string serverError = "Server error";
            public const string onlineModeText = "Online mode activated";
            public const string offlineModeText = "Offline mode activated";

            public const string noConnectionText = "No network connection available, entering offline mode..";

            public const string statusWaitText = "Waiting for user input";
            public const string loadingDataText = "Getting data from server..";
            public const string loadedText = "Loaded successfully";
            public const string loadingErrorText = "Error loading folders..";
            #endregion
            #region Path and system variables
            public static readonly string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            public static readonly string appDataFolder = Path.Combine(localAppData, appName);
            public const string catalogDetailsSubDir = "CatalogDetails";
            public const string videoSubDir = "Videos";
            public const string logSubDir = "logs";
            #endregion
        }

        /// <summary>
        /// Colors to be used
        /// </summary>
        public static class AppColors
        {
            public static readonly Color ErrorText = Color.Red;
            public static readonly Color OKText = Color.SeaGreen;
            public static readonly Color BlueText = Color.RoyalBlue;
        }
    }
}
