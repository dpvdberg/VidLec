using NLog;
using NLog.Config;
using NLog.Layouts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace VidLec
{
    public partial class LectureSelector : Form
    {
        private static LogForm logForm = new LogForm();
        private static Logger logger;
        private FileManager fileManager;
        Comm comm;
        
        public LectureSelector()
        {
            InitializeComponent();
            if (Properties.Settings.Default.LoggingEnable)
            {
                // Show log form before creating loggers
                logForm.Show();
                // Create loggers
                logger = LogManager.GetCurrentClassLogger();
            }
            // Setup this form
            SetupListViews();
            SetFormSettings();    
            // Create class instances
            comm = new Comm();
            fileManager = new FileManager();
            // Handle network status
            AppConfig.AppInstance.onlineMode = comm.CheckNet();
        }

        /// <summary>
        /// Sets the form default settings
        /// </summary>
        private void SetFormSettings()
        {
            loggingDebugToolStripMenuItem.Checked = Properties.Settings.Default.LoggingVerbose;
            loggingEnableToolStripMenuItem.Checked = Properties.Settings.Default.LoggingEnable;
            saveCookiesToolStripMenuItem.Checked = Properties.Settings.Default.SaveCookies;
            offlineByDefaultToolStripMenuItem.Checked = Properties.Settings.Default.OfflineByDefault;
            saveCatalogDetailsToolStripMenuItem.Checked = Properties.Settings.Default.SaveCatalogDetails;
        }

        private enum LoginResult
        {
            COOKIE_OK,
            COOKIE_FAIL,
            CREDENTIALS_OK,
            CREDENTIALS_FAIL,
            NOTHINGSAVED,
            COULD_NOT_GET_CATALOG
        } 

        /// <summary>
        /// Make sure the user is logged in
        /// i.e. we have a valid cookie
        /// 
        /// Expects the user to be online
        /// </summary>
        private void EnsureLogin(BackgroundWorker bg, ref DoWorkEventArgs e)
        {
            bg.ReportProgress(10);
            if (comm.SetCatalogURL())
            {
                bg.ReportProgress(60);
                if (Properties.Settings.Default.LoginCookieData != "")
                {
                    AppConfig.AppInstance.cookieData = Properties.Settings.Default.LoginCookieData;
                    if (comm.ValidateCookie())
                    {
                        logger.Debug("Comm login using cookie successful");
                        e.Result = LoginResult.COOKIE_OK;
                    }
                    else
                    {
                        logger.Debug("Comm returned false cookie flag, erasing cookie data and retrying login procedure..");
                        Properties.Settings.Default.LoginCookieData = "";
                        Properties.Settings.Default.Save();
                        e.Result = LoginResult.COOKIE_FAIL;
                    }
                }
                else if (Properties.Settings.Default.Username != "" && Properties.Settings.Default.Password != "")
                {
                    logger.Debug("Found saved credentials");
                    if (comm.Login(Properties.Settings.Default.Username,
                        Properties.Settings.Default.Password,
                        true,
                        Properties.Settings.Default.SaveCookies))
                    {
                        logger.Debug("Logged in using credentials");
                        e.Result = LoginResult.CREDENTIALS_OK;
                    }
                    else
                    {
                        logger.Info("credentials invalid, could not log in");
                        e.Result = LoginResult.CREDENTIALS_FAIL;
                    }
                }
                else
                {
                    logger.Info("Online mode and no credentials or cookie data saved, asking for login..");
                    e.Result = LoginResult.NOTHINGSAVED;
                }
            }
            else
            {
                logger.Error("Getting catalog URL failed, aborting login..");
                e.Result = LoginResult.COULD_NOT_GET_CATALOG;
            }
            bg.ReportProgress(100);
        }

        /// <summary>
        /// Enables or disables a specific log level in the logform logbox
        /// </summary>
        private void SetBoxLoggingLevel(LogLevel level, bool enable)
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

        /// <summary>
        /// Configurate the listviews
        /// </summary>
        private void SetupListViews()
        {
            // Configure the first tree
            tlvAll.CanExpandGetter = delegate (object x) { return ((Folder)x).childFolders.Count > 0 ? true : false; };
            tlvAll.ChildrenGetter = delegate (object x) { return ((Folder)x).childFolders; };

            tlvAllClmCount.AspectToStringConverter = delegate (object x)
            {
                return x is int ? ((int) x == 0 ? "" : x.ToString()) : "";
            };


            olvPresClmDate.AspectToStringFormat = "{0:dd-MM-yyy}";
            olvPresClmDate.AspectGetter = delegate (object x) {
                string date = ((Presentation)x).FullStartDate;
                DateTime dt;
                if (DateTime.TryParseExact(date,
                                            "MM/dd/yyyy HH:mm:ss",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out dt))
                    return dt.Date;
                else
                    return null;
            };
        }

        /// <summary>
        /// Attempts to change network status
        /// </summary>
        /// <param name="setOnline"></param>
        public void ChangeNetworkStatus(bool setOnline)
        {
            // Try to connect to the internet
            if (setOnline)
            {
                if (comm.CheckNet())
                {
                    AppConfig.AppInstance.onlineMode = true;
                    logger.Info("Connected to the internet");
                    SetStatus(AppConfig.Constants.loggingInText, AppConfig.AppColors.OKText);
                    //bgwLogin.RunWorkerAsync();
                    ///*
                    comm.SetCatalogURL();
                    comm.Login(Properties.Settings.Default.Username,
                        Properties.Settings.Default.Password,
                        true,
                        Properties.Settings.Default.SaveCookies);
                    test();
                    //*/
                }
                else
                {
                    AppConfig.AppInstance.onlineMode = false;
                    logger.Info("Cannot connect to the internet");
                    SetStatus(AppConfig.Constants.offlineModeText, AppConfig.AppColors.BlueText);
                }
            }
            // Force offline
            else
            {
                AppConfig.AppInstance.onlineMode = false;
                logger.Info("Offline mode activated");
                SetStatus(AppConfig.Constants.offlineModeText, AppConfig.AppColors.BlueText);
            }
        }

        public void SetStatus(string status, Color color)
        {
            toolStripStatusLabel.ForeColor = color;
            toolStripStatusLabel.Text = status;
        }

        public void SetProgress(int percentage)
        {
            statusStrip.Invoke(new Action(() => toolStripProgressBar.Value = percentage));
            if (percentage == 100)
            {
                new Thread(delegate () {
                    Thread.Sleep(1000);
                    SetProgress(0);
                }).Start();
            }
        }

        #region GUI events

        private void LectureSelector_Load(object sender, EventArgs e)
        {
            logger.Debug("Form loaded");
        }

        private void DropDownSetOnline_Click(object sender, EventArgs e)
        {
            ChangeNetworkStatus(true);
        }

        private void DropDownForceOffline_Click(object sender, EventArgs e)
        {
            ChangeNetworkStatus(false);
        }


        private void openLogWindowStripMenuItem_Click(object sender, EventArgs e)
        {
            logForm.Show();
            logForm.Focus();
        }

        private void viewLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string logFilePath = Path.Combine(AppConfig.Constants.appDataFolder,
                AppConfig.Constants.logSubDir,
                SimpleLayout.Evaluate(LogManager.Configuration.Variables["currentDate"].Text) + SimpleLayout.Evaluate(LogManager.Configuration.Variables["logFileExtension"].Text));
            logger.Debug(string.Format("Trying to open logfile located at \"{0}\"", logFilePath));
            Process.Start(logFilePath);
        }

        private void LectureSelector_Shown(object sender, EventArgs e)
        {
            ChangeNetworkStatus(!Properties.Settings.Default.OfflineByDefault);
        }

        private void saveCookiesToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SaveCookies = saveCookiesToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void loggingDebugToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            SetBoxLoggingLevel(LogLevel.Debug, loggingDebugToolStripMenuItem.Checked);
            logger.Debug(string.Format("Debug logging set to: {0}", loggingDebugToolStripMenuItem.Checked));
            Properties.Settings.Default.LoggingVerbose = loggingDebugToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void loggingEnableToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (loggingEnableToolStripMenuItem.Checked)
            {
                logForm.Show();
                logForm.Focus();
                LogManager.EnableLogging();
                logger.Info("Logging enabled");
            }
            else
            {
                logForm.Hide();
                LogManager.DisableLogging();
            }
            Properties.Settings.Default.LoggingEnable = loggingEnableToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }


        private void offlineByDefaultToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.OfflineByDefault = offlineByDefaultToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void deleteSavedCookiesToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.LoginCookieData = "";
            Properties.Settings.Default.Save();
        }

        private void deleteSavedCredentailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Username = "";
            Properties.Settings.Default.Password = "";
            Properties.Settings.Default.Save();
        }

        private void resetAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            SetFormSettings();
            logger.Info("Reset all settings to default");
        }

        private void saveCatalogDetailsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SaveCatalogDetails = saveCatalogDetailsToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }
        #endregion

        #region Background workers/threads
        private void bgwLogin_DoWork(object sender, DoWorkEventArgs e)
        {
            EnsureLogin(bgwLogin, ref e);
        }
        

        private void bgwLogin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        { 
            switch ((LoginResult) e.Result)
            {
                case LoginResult.COOKIE_OK:
                case LoginResult.CREDENTIALS_OK:
                    SetStatus(AppConfig.Constants.loggedIn, AppConfig.AppColors.OKText);
                    break;
                case LoginResult.COOKIE_FAIL:
                    logger.Debug("Failed to log in, retrying..");
                    bgwLogin.RunWorkerAsync();
                    break;
                case LoginResult.CREDENTIALS_FAIL:
                case LoginResult.NOTHINGSAVED:
                    SetStatus("Asking for login", AppConfig.AppColors.BlueText);
                    (new LoginForm(this)).Show();
                    break;
                case LoginResult.COULD_NOT_GET_CATALOG:
                    SetStatus(AppConfig.Constants.serverError, AppConfig.AppColors.ErrorText);
                    break;
            }
        }

        private void bgwLogin_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SetProgress(e.ProgressPercentage);
        }

        private void bgwCatalogLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void test()
        {
            Folder savedRoot = fileManager.getCatalogDetails();
            if (Properties.Settings.Default.SaveCatalogDetails && savedRoot != null)
            {
                AppConfig.AppInstance.rootFolder = savedRoot;
            }
            else
            {
                logger.Debug("Getting catalog details from comm");
                string rawCatalogDetails = comm.GetCatalogDetails();
                logger.Debug("Serialize catalog details");
                Folder rootFolder = DataParser.ParseCatalogDetails(rawCatalogDetails);
                if (Properties.Settings.Default.SaveCatalogDetails)
                {
                    logger.Debug("Saving serialized classes to file");
                    fileManager.saveCatalogDetails(rootFolder);
                }
            }
        }

        private void bgwCatalogLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        #endregion
    }
}
