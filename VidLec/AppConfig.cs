using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidLec
{
    class AppConfig
    {
        public static class AppInstance
        {
            public static bool onlineMode = false;
            public static bool cookieValid = false;
            public static string cookieData = "";
            public static string username = "";
            public static string password = "";

            public static string catalogURL = "";
            public static string catalogId = "";
        }

        public static class SiteData
        {
            /// <summary>
            /// URL constants
            /// </summary>
            public const string baseURL = "http://videocollege.tue.nl";
            public const string loginURL = baseURL + "/Mediasite/Login/";

            // Redirect (302) header name
            public const string redirectHeaderName = "Location";

            /// <summary>
            /// Login related constants
            /// </summary>
            public const string contentTypeHeader = "application/x-www-form-urlencoded";
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

            public static byte[] GetLoginPostData()
            {
                string postData = "";

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
        }

        /// <summary>
        /// Constants
        /// Text, links, numbers & data
        /// </summary>
        public static class Constants
        {
            const int statusWaitResetInterval = 800;

            #region Text
            public const string loggingInText = "Logging in..";
            public const string loggedIn = "Logged in";
            public const string serverError = "Server error";
            public const string offlineModeText = "Offline mode activated";

            public const string noConnectionText = "No network connection available, entering offline mode..";

            public const string statusWaitText = "Waiting for user input";
            public const string loadingDataText = "Getting data from server..";
            public const string loadedText = "Loaded successfully";
            public const string loadingErrorText = "Error loading folders..";
            #endregion
            #region Path and system variables
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
