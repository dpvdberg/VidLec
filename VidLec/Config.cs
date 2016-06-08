using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidLec
{
    class Config
    {
        /// <summary>
        /// Static variables
        /// </summary>
        public static bool onlineMode = false;
        public static bool loggingEnable = true;
        public static bool loggingVerbose = true;

        public static class UserData
        {
            public static string username = "test";
            public static string password = "123";
            public static string cookieData = "";
        }

        public static class SiteData
        {
            public const string loginURL = "http://videocollege.tue.nl/Mediasite/Login/";
            public const string contentTypeHeader = "application/x-www-form-urlencoded";
            public const string cookieFieldName = "MediasiteAuth";
            public const string cookieHeaderName = "Set-Cookie";
            public static Dictionary<string, string> loginPostParameters = new Dictionary<string, string>()
            {
                { "UserName", UserData.username},
                { "Password", UserData.password},
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
        }

        /// <summary>
        /// Constants
        /// Text, links, numbers & data
        /// </summary>
        public static class Constants
        {
            const int statusWaitResetInterval = 800;

            #region Text
            public const string onlineModeText = "Online mode activated";
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
