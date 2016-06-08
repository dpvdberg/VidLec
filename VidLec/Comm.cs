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

        public void test()
        {
            bool bla = SetLoginCookie();
            Console.WriteLine(bla.ToString());
        }
        
        public bool SetLoginCookie()
        {
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(Config.SiteData.loginURL);
            byte[] postData = Config.SiteData.GetLoginPostData();

            webRequest.Method = "POST";
            webRequest.ContentLength = postData.Length;
            webRequest.ContentType = Config.SiteData.contentTypeHeader;
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
                string[] setCookieHeader = myResp.Headers.GetValues(Config.SiteData.cookieHeaderName);
                if (setCookieHeader != null)
                {
                    string loginCookie = setCookieHeader.FirstOrDefault(p => p.Contains(Config.SiteData.cookieFieldName));
                    if (loginCookie == default(string))
                    {
                        logger.Error("Login field in header was not found");
                        return false;
                    }
                    else
                    {
                        Config.UserData.cookieData = loginCookie;
                        logger.Debug(string.Format("Found loginCookie:\n{0}", loginCookie));
                        return true;
                    }
                }
                else
                {
                    logger.Error("Login cookie header was not found");
                    return false;
                }
            }
            else
            {
                logger.Error(string.Format("Login reponse was unexpected ({0})", (int)myResp.StatusCode));
                return false;
            }
        }
    }
}
