using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VidLec
{
    public class LoginManager
    {
        LogForm logForm = new LogForm();
        Comm comm;
        Logger logger;

        public LoginManager()
        {
            // First initialize the logger
            if (Properties.Settings.Default.LoggingEnable)
            {
                // Show log form before creating loggers
                logForm.Show();
                // Create loggers
                logger = LogManager.GetCurrentClassLogger();
                // Set debug log level for log box
                SetLoggingBoxLogLevel(LogLevel.Debug, Properties.Settings.Default.LoggingVerbose);
            }
            comm = new Comm();
        }

        public void AutoLogin()
        {
            if (comm.CheckNet())
            {
                logger.Debug("Connected to the internet");
                Login();
                Application.Run(new LectureSelector(this, logForm));
            }
            else
            {
                logger.Info("Computer not connected to the internet, entering offline mode");
                AppConfig.AppInstance.loginResult = LoginResult.OFFLINE;
            }
        }

        public LoginResult Login(bool forceOnline = false)
        {
            InitializationStatus initStatus = GetInitializationFlag();
            if (Properties.Settings.Default.LoggingVerbose)
            {
                if (initStatus.HasFlag(InitializationStatus.COOKIE))
                    logger.Debug("Saved cookies found");
                if (initStatus.HasFlag(InitializationStatus.CREDENTIALS))
                    logger.Debug("Saved credentials found");
                if (initStatus.HasFlag(InitializationStatus.COULD_NOT_GET_CATALOG))
                    logger.Error("Could not find catalog");
            }
            LoginResult loginResult = GetLoginResult(initStatus, forceOnline);
            if (loginResult.HasFlag(LoginResult.FAILED))
                loginResult = askLogin();
            if (loginResult.HasFlag(LoginResult.OFFLINE))
                logger.Info("Going offline");

            AppConfig.AppInstance.loginResult = loginResult;
            return loginResult;
        }

        private LoginResult askLogin()
        {
            if ((new LoginForm()).ShowDialog() == DialogResult.OK)
                return LoginResult.SUCCESS;
            else
                return LoginResult.OFFLINE;
        }

        [Flags]
        private enum InitializationStatus
        {
            NULL = 0,
            COOKIE = 1,
            CREDENTIALS = 2,
            COULD_NOT_GET_CATALOG = 4
        }

        public enum LoginResult
        {
            NULL = 0,
            OFFLINE = 1,
            FAILED = 2,
            SUCCESS = 4
        }

        private InitializationStatus GetInitializationFlag()
        {
            InitializationStatus status = InitializationStatus.NULL;
            // See what the user has saved
            if (comm.SetCatalogURL())
            {
                if (Properties.Settings.Default.LoginCookieData != "")
                    status |= InitializationStatus.COOKIE;
                if (Properties.Settings.Default.Username != "" && Properties.Settings.Default.Password != "")
                    status |= InitializationStatus.CREDENTIALS;
                return status;
            }
            else
                return InitializationStatus.COULD_NOT_GET_CATALOG;
        }

        private LoginResult GetLoginResult(InitializationStatus initStatus, bool forceOnline = false)
        {
            if ((!forceOnline && Properties.Settings.Default.OfflineByDefault) || initStatus.HasFlag(InitializationStatus.COULD_NOT_GET_CATALOG))
                return LoginResult.OFFLINE;

            if (initStatus.HasFlag(InitializationStatus.COOKIE))
            {
                if (comm.ValidateCookie(true))
                    return LoginResult.SUCCESS;
                else
                {
                    logger.Debug("Cookie is invalid, removing..");
                    Properties.Settings.Default.LoginCookieData = "";
                    Properties.Settings.Default.Save();
                }
            }

            if (initStatus.HasFlag(InitializationStatus.CREDENTIALS))
            {
                logger.Debug("Logging in using credentials..");
                if (comm.Login(Properties.Settings.Default.Username,
                    Properties.Settings.Default.Password,
                    true,
                    Properties.Settings.Default.SaveCookies))
                    return LoginResult.SUCCESS;
                
            }

            return LoginResult.FAILED;
        }

        /// <summary>
        /// Enables or disables a specific log level in the logform logbox
        /// </summary>
        internal void SetLoggingBoxLogLevel(LogLevel level, bool enable)
        {
            LoggingRule rule = LogManager.Configuration.LoggingRules.FirstOrDefault(p => p.Targets.Contains(p.Targets.FirstOrDefault(t => t.Name == "box")));
            if (rule != null)
            {
                LogManager.Configuration.LoggingRules.Remove(rule);
                if (enable)
                {
                    rule.EnableLoggingForLevel(level);
                }
                else
                {
                    rule.DisableLoggingForLevel(level);
                }
                LogManager.Configuration.LoggingRules.Add(rule);
                LogManager.ReconfigExistingLoggers();
            }
        }
    }
}
