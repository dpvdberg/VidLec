using BrightIdeasSoftware;
using MaterialSkin;
using MaterialSkin.Controls;
using NLog;
using NLog.Config;
using NLog.Layouts;
using System;
using System.Collections;
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
    public partial class LectureSelector : MaterialForm
    {
        private LoginManager loginManager;
        private static Logger logger;
        private FileManager fileManager;
        private LogForm logForm;
        private Comm comm;

        // This bool is used to prevent events firing when
        // changing control values during runtime
        private bool initializingSettings = true;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loginManager">LoginManager instance</param>
        /// <param name="logForm">LogForm instance</param>
        public LectureSelector(LoginManager loginManager, LogForm logForm)
        {
            InitializeComponent();
            SetFormSettings();
            if (Properties.Settings.Default.LoggingEnable)
                logger = LogManager.GetCurrentClassLogger();
            // Setup this form
            SetupListViews();
            // Set parameter forms
            this.loginManager = loginManager;
            this.logForm = logForm;
            // Create class instances
            comm = new Comm();
            fileManager = new FileManager();

            ChangeNetworkStatus(AppConfig.AppInstance.loginResult == LoginManager.LoginResult.SUCCESS);

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        /// <summary>
        /// Sets the form default settings
        /// </summary>
        private void SetFormSettings()
        {
            initializingSettings = true;
            loggingDebugToolStripMenuItem.Checked = Properties.Settings.Default.LoggingVerbose;
            loggingEnableToolStripMenuItem.Checked = Properties.Settings.Default.LoggingEnable;
            saveCookiesToolStripMenuItem.Checked = Properties.Settings.Default.SaveCookies;
            offlineByDefaultToolStripMenuItem.Checked = Properties.Settings.Default.OfflineByDefault;
            saveCatalogDetailsToolStripMenuItem.Checked = Properties.Settings.Default.SaveCatalogDetails;
            initializingSettings = false;
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
                    bool isLoggedIn = AppConfig.AppInstance.loginResult == LoginManager.LoginResult.SUCCESS;
                    if (!isLoggedIn && loginManager.Login(true) != LoginManager.LoginResult.SUCCESS)
                    {
                        AppConfig.AppInstance.onlineMode = false;
                        logger.Info("Failed to log in to the server, offline mode activated");
                        SetStatus(AppConfig.Constants.offlineModeText, AppConfig.AppColors.BlueText);
                    }
                    AppConfig.AppInstance.onlineMode = true;
                    logger.Info("Online mode activated");
                    SetStatus(AppConfig.Constants.onlineModeText, AppConfig.AppColors.OKText);
                }
                else
                {
                    AppConfig.AppInstance.onlineMode = false;
                    logger.Info("Cannot connect to the internet, offline mode activated");
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

        /// <summary>
        /// Sets the status label
        /// </summary>
        /// <param name="status">Status text</param>
        /// <param name="color">Text color</param>
        public void SetStatus(string status, Color color)
        {
            toolStripStatusLabel.ForeColor = color;
            toolStripStatusLabel.Text = status;
        }

        /// <summary>
        /// Sets the progress bar, thread safe
        /// </summary>
        /// <param name="percentage">0-100 percentage</param>
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

        /// <summary>
        /// Save toolstip checked items to settings
        /// </summary>
        /// <param name="sender">The object calling this event</param>
        /// <param name="e">Event arguments</param>
        private void SaveSettings(object sender, EventArgs e)
        {
            if (!initializingSettings) {
                Properties.Settings.Default.SaveCookies = saveCookiesToolStripMenuItem.Checked;
                Properties.Settings.Default.OfflineByDefault = offlineByDefaultToolStripMenuItem.Checked;
                Properties.Settings.Default.SaveCatalogDetails = saveCatalogDetailsToolStripMenuItem.Checked;
                Properties.Settings.Default.LoggingVerbose = loggingDebugToolStripMenuItem.Checked;

                loginManager.SetLoggingBoxLogLevel(LogLevel.Debug, loggingDebugToolStripMenuItem.Checked);
                if (sender == loggingDebugToolStripMenuItem)
                    logger.Debug(string.Format("Debug logging set to: {0}", loggingDebugToolStripMenuItem.Checked));

                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Sets the AppInstance root folder by first trying to get stored catalog details.
        /// If these are not found, comm class will generate new catalog details.
        /// </summary>
        /// <returns>Wheter finding a root was successful</returns>
        private bool SetRootFolder()
        {
            Folder storedRoot = fileManager.GetStoredCatalogDetails();
            if (Properties.Settings.Default.SaveCatalogDetails && storedRoot != null)
            {
                logger.Debug("Using saved catalog details");
                AppConfig.AppInstance.rootFolder = storedRoot;
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
                    fileManager.SaveCatalogDetails(rootFolder);
                }
                AppConfig.AppInstance.rootFolder = rootFolder;
            }

            if (AppConfig.AppInstance.rootFolder == null)
            {
                logger.Error("Could not get a root folder");
                return false;
            }
            else
            {
                logger.Debug("Successfully loaded root folder");
                return true;
            }
        }

        #region GUI events

        private void LectureSelector_Load(object sender, EventArgs e)
        {
            logger.Debug("Form loaded, attemting to set root folder for catalog details");
            if (SetRootFolder())
            {
                tlvAll.AddObject(AppConfig.AppInstance.rootFolder);
                tlvAll.Expand(AppConfig.AppInstance.rootFolder);
            }
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
            string logFilePath = Path.Combine(
                SimpleLayout.Evaluate(LogManager.Configuration.Variables["logDirectory"].Text) +
                SimpleLayout.Evaluate(LogManager.Configuration.Variables["currentDate"].Text) +
                SimpleLayout.Evaluate(LogManager.Configuration.Variables["logFileExtension"].Text));//.Replace('/','\\');
            logger.Debug(string.Format("Trying to open logfile located at \"{0}\"", logFilePath));
            Process.Start(logFilePath);
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

        private void deleteSavedCookiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LoginCookieData = "";
            Properties.Settings.Default.Save();
            logger.Info("Deleted saved cookie (note: Cookies for current session are still cached)");
        }

        private void deleteSavedCredentailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Username = "";
            Properties.Settings.Default.Password = "";
            Properties.Settings.Default.Save();
            logger.Info("Deleted saved credentials");
        }

        private void resetAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            SetFormSettings();
            logger.Info("Reset all settings to default");
        }

        List<Folder> expandedObjects = null;
        string lastSearchString = "";
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                if (lastSearchString == "")
                {
                    expandedObjects = new List<Folder>();
                    foreach (Folder f in tlvAll.ExpandedObjects)
                        expandedObjects.Add(f);
                }

                tlvAll.ExpandAll();
                TextMatchFilter filter = new TextMatchFilter(tlvAll, txtSearch.Text);
                tlvAll.ModelFilter = filter;
                tlvAll.DefaultRenderer = new HighlightTextRenderer(filter);
            }
            else
            {
                tlvAll.ModelFilter = null;
                tlvAll.CollapseAll();
                foreach (Folder f in expandedObjects)
                    tlvAll.Expand(f);
            }
            lastSearchString = txtSearch.Text;
        }
        #endregion
    }
}
