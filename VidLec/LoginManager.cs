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
        LogForm _logForm = new LogForm();
        Comm _comm;
        Logger _logger;

        public LoginManager(bool hasVlcLib)
        {
            // First initialize the logger
            if (Properties.Settings.Default.LoggingEnable)
            {
                // Show log form before creating loggers
                _logForm.Show();
                // Create loggers
                _logger = LogManager.GetCurrentClassLogger();
                // Set debug log level for log box
                SetLoggingBoxLogLevel(LogLevel.Debug, Properties.Settings.Default.LoggingVerbose);
            }
            if (!hasVlcLib)
            {
                _logger.Error("Could not extract VLC library!");
                MessageBox.Show("This program cannot start without the VLC library!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            _comm = new Comm();
        }

        public void AutoLogin()
        {
            if (_comm.CheckNet())
            {
                _logger.Debug("Connected to the internet");
                Login();
            }
            else
            {
                _logger.Info("Computer not connected to the internet, entering offline mode");
                AppConfig.AppInstance.LoginResult = LoginResult.Offline;
            }
            Application.Run(new LectureSelector(this, _logForm));
        }

        public LoginResult Login(bool forceOnline = false)
        {
            InitializationStatus initStatus = GetInitializationFlag();
            if (Properties.Settings.Default.LoggingVerbose)
            {
                if (initStatus.HasFlag(InitializationStatus.Cookie))
                    _logger.Debug("Saved cookies found");
                if (initStatus.HasFlag(InitializationStatus.Credentials))
                    _logger.Debug("Saved credentials found");
                if (initStatus.HasFlag(InitializationStatus.CouldNotGetCatalog))
                    _logger.Error("Could not find catalog");
            }
            LoginResult loginResult = GetLoginResult(initStatus, forceOnline);
            if (loginResult.HasFlag(LoginResult.Failed))
                loginResult = AskLogin();
            if (loginResult.HasFlag(LoginResult.Offline))
                _logger.Info("Going offline");

            AppConfig.AppInstance.LoginResult = loginResult;
            return loginResult;
        }

        private LoginResult AskLogin()
        {
            if ((new LoginForm()).ShowDialog() == DialogResult.OK)
                return LoginResult.Success;
            return LoginResult.Offline;
        }

        [Flags]
        private enum InitializationStatus
        {
            Null = 0,
            Cookie = 1,
            Credentials = 2,
            CouldNotGetCatalog = 4
        }

        [Flags]
        public enum LoginResult
        {
            Null = 0,
            Offline = 1,
            Failed = 2,
            Success = 4
        }

        private InitializationStatus GetInitializationFlag()
        {
            InitializationStatus status = InitializationStatus.Null;
            // See what the user has saved
            if (_comm.SetCatalogUrl())
            {
                if (Properties.Settings.Default.LoginCookieData != "")
                    status |= InitializationStatus.Cookie;
                if (Properties.Settings.Default.Username != "" && Properties.Settings.Default.Password != "")
                    status |= InitializationStatus.Credentials;
                return status;
            }
            return InitializationStatus.CouldNotGetCatalog;
        }

        private LoginResult GetLoginResult(InitializationStatus initStatus, bool forceOnline = false)
        {
            if ((!forceOnline && Properties.Settings.Default.OfflineByDefault) || initStatus.HasFlag(InitializationStatus.CouldNotGetCatalog))
                return LoginResult.Offline;

            if (initStatus.HasFlag(InitializationStatus.Cookie))
            {
                if (_comm.ValidateCookie(true))
                    return LoginResult.Success;
                _logger.Debug("Cookie is invalid, removing..");
                Properties.Settings.Default.LoginCookieData = "";
                Properties.Settings.Default.Save();
            }

            if (initStatus.HasFlag(InitializationStatus.Credentials))
            {
                _logger.Debug("Logging in using credentials..");
                if (_comm.Login(Properties.Settings.Default.Username,
                    Properties.Settings.Default.Password,
                    true,
                    Properties.Settings.Default.SaveCookies))
                    return LoginResult.Success;
                
            }

            return LoginResult.Failed;
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
