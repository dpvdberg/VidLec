using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VidLec.DataClasses.Requests;

namespace VidLec
{
    class AppConfig
    {
        public static class AppInstance
        {
            public static LoginManager.LoginResult LoginResult;
            public static bool OnlineMode = false;
            public static string CookieData = "";
            public static string Username = "";
            public static string Password = "";

            public static string CatalogUrl = "";
            public static string CatalogId = "";
            public static Folder RootFolder = null;
        }

        public static class AppChosenVariables
        {
            public static int CatalogDetailsRetentionDays = 7;
        }

        public static class SiteData
        {
            /// <summary>
            /// URL constants
            /// </summary>
            public const string BaseUrl = "http://videocollege.tue.nl";
            public const string LoginUrl = BaseUrl + "/Mediasite/Login/";
            public const string CatalogDetailsUrl = BaseUrl + "/Mediasite/Catalog/Data/GetCatalogDetails";
            public const string PresentationForFolderUrl = BaseUrl + "/Mediasite/Catalog/Data/GetPresentationsForFolder";
            public const string VideoForPresentationUrl = BaseUrl + "/Mediasite/PlayerService/PlayerService.svc/json/GetPlayerOptions";

            // Redirect (302) header name
            public const string RedirectHeaderName = "Location";

            /// <summary>
            /// Login related constants
            /// </summary>
            public const string RequestDataTypeHeader = "application/json; charset=utf-8";
            public const string LoginContentTypeHeader = "application/x-www-form-urlencoded";
            public const string CookieFieldName = "MediasiteAuth";
            public const string CookieHeaderName = "Set-Cookie";
            public const string ValidCookieMagicKeyword = "Authenticated=\"True\"";
            public const string CatalogFieldName = "CatalogId";
            public static Dictionary<string, string> LoginPostParameters = new Dictionary<string, string>()
            {
                { "UserName", AppInstance.Username},
                { "Password", AppInstance.Password},
                { "RememberMe", "false"}
            };

            /// <summary>
            /// JSON constants
            /// </summary>
            public const string JsonNavigationFoldersName = "NavigationFolders";

            public static byte[] GetLoginPostData()
            {
                string postData = "";

                LoginPostParameters["UserName"] = AppInstance.Username;
                LoginPostParameters["Password"] = AppInstance.Password;

                foreach (KeyValuePair<string, string> parameter in LoginPostParameters)
                {
                    postData += $"{parameter.Key}={parameter.Value}&";
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
                o["CatalogId"] = AppInstance.CatalogId;
                o["CurrentFolderId"] = AppInstance.CatalogId;
                o["Url"] = AppInstance.CatalogUrl;
                return o.ToString();
            }

            public static string GetPresentationsForFolderRequestBody(Folder folder)
            {
                GetPresentationsForFolderRequest body = new GetPresentationsForFolderRequest();
                body.IsViewPage = true;
                body.IsNewFolder = true;
                body.AuthTicket = null;
                body.CatalogId = AppInstance.CatalogId;
                body.CurrentFolderId = folder.DynamicFolderId;
                body.RootDynamicFolderId = AppInstance.CatalogId;
                body.ItemsPerPage = 100;
                body.PageIndex = 0;
                body.PermissionMask = "Execute";
                body.CatalogSearchType = "SearchInFolder";
                body.SortBy = "Date";
                body.SortDirection = "Descending";
                body.StartDate = null;
                body.EndDate = null;
                body.StatusFilterList = null;
                body.PreviewKey = null;
                body.Tags = new List<object>();

                return JsonConvert.SerializeObject(body);
            }

            public static string GetVideoForPresentationRequestBody(Presentation presentation)
            {
                GetVideoForPresentationRequest body = new GetVideoForPresentationRequest();
                body.getPlayerOptionsRequest = new GetPlayerOptionsRequest();
                body.getPlayerOptionsRequest.QueryString = $"?catalog={AppInstance.CatalogId}";
                body.getPlayerOptionsRequest.ResourceId = presentation.Id;
                body.getPlayerOptionsRequest.UseScreenReader = false;

                return JsonConvert.SerializeObject(body);
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
            public const int ConnectionTries = 3;
            public const int ConnectionTimeout = 3000;

            /// <summary>
            /// Auxiliary
            /// </summary>
            public const string AppName = "VidLec";
            public const string DateTimeFtm = "O";

            #region Text
            public const string LoggingInText = "Logging in..";
            public const string LoggedIn = "Logged in";
            public const string ServerError = "Server error";
            public const string OnlineModeText = "Online mode activated";
            public const string OfflineModeText = "Offline mode activated";

            public const string NoConnectionText = "No network connection available, entering offline mode..";

            public const string StatusWaitText = "Waiting for user input";
            public const string LoadingDataText = "Getting data from server..";
            public const string LoadedText = "Loaded successfully";
            public const string LoadingErrorText = "Error loading folders..";
            #endregion
            #region Path and system variables
            public static readonly string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            public static readonly string AppDataFolder = Path.Combine(LocalAppData, AppName);
            public const string CatalogDetailsSubDir = "CatalogDetails";
            public const string VideoSubDir = "Videos";
            public const string LogSubDir = "logs";
            public const string LibVlcDir = "lib";
            #endregion
        }

        /// <summary>
        /// Colors to be used
        /// </summary>
        public static class AppColors
        {
            public static readonly Color ErrorText = Color.Red;
            public static readonly Color OkText = Color.SeaGreen;
            public static readonly Color BlueText = Color.RoyalBlue;
        }
    }
}
