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
using Newtonsoft.Json.Linq;
using Vlc.DotNet.Forms.Samples;

namespace VidLec
{
    public partial class LectureSelector : MaterialForm
    {
        private readonly LoginManager _loginManager;
        private static Logger _logger;
        private readonly FileManager _fileManager;
        private readonly LogForm _logForm;
        private readonly Comm _comm;

        // This bool is used to prevent events firing when
        // changing control values during runtime
        private bool _initializingSettings = true;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loginManager">LoginManager instance</param>
        /// <param name="logForm">LogForm instance</param>
        public LectureSelector(LoginManager loginManager, LogForm logForm)
        {
            //test
            //LectureViewer lectureViewer = new LectureViewer();
            //lectureViewer.Show();


            InitializeComponent();
            SetFormSettings();
            if (Properties.Settings.Default.LoggingEnable)
                _logger = LogManager.GetCurrentClassLogger();
            // Setup this form
            SetupListViews();
            // Set parameter forms
            this._loginManager = loginManager;
            this._logForm = logForm;
            // Create class instances
            _comm = new Comm();
            _fileManager = new FileManager();

            ChangeNetworkStatus(AppConfig.AppInstance.LoginResult == LoginManager.LoginResult.Success);

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900,
                Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        /// <summary>
        /// Sets the form default settings
        /// </summary>
        private void SetFormSettings()
        {
            _initializingSettings = true;
            loggingDebugToolStripMenuItem.Checked = Properties.Settings.Default.LoggingVerbose;
            loggingEnableToolStripMenuItem.Checked = Properties.Settings.Default.LoggingEnable;
            saveCookiesToolStripMenuItem.Checked = Properties.Settings.Default.SaveCookies;
            offlineByDefaultToolStripMenuItem.Checked = Properties.Settings.Default.OfflineByDefault;
            saveCatalogDetailsToolStripMenuItem.Checked = Properties.Settings.Default.SaveCatalogDetails;
            _initializingSettings = false;
        }

        /// <summary>
        /// Configurate the listviews
        /// </summary>
        private void SetupListViews()
        {
            // Configure the first tree
            tlvAll.CanExpandGetter = x => ((Folder) x).ChildFolders.Count > 0;
            tlvAll.ChildrenGetter = x => ((Folder) x).ChildFolders;

            tlvAllClmCount.AspectToStringConverter =
                x => x is int ? ((int) x == 0 ? "" : x.ToString()) : "";

            olvPresentations.ContextMenu = new ContextMenu();
            olvPresentations.ContextMenu.MenuItems.Add("Item 1");
            olvPresentations.ContextMenu.MenuItems.Add("Item 2");

            olvPresClmDate.AspectToStringFormat = "{0:dd-MM-yyy}";
            olvPresClmDate.AspectGetter = delegate(object x)
            {
                string date = ((Presentation) x).FullStartDate;
                DateTime dt;
                if (DateTime.TryParseExact(date,
                    "MM/dd/yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out dt))
                    return dt.Date;
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
                if (_comm.CheckNet())
                {
                    bool isLoggedIn = AppConfig.AppInstance.LoginResult == LoginManager.LoginResult.Success;
                    if (!isLoggedIn && _loginManager.Login(true) != LoginManager.LoginResult.Success)
                    {
                        AppConfig.AppInstance.OnlineMode = false;
                        _logger.Info("Failed to log in to the server, offline mode activated");
                        SetStatus(AppConfig.Constants.OfflineModeText, AppConfig.AppColors.BlueText);
                    }
                    AppConfig.AppInstance.OnlineMode = true;
                    _logger.Info("Online mode activated");
                    SetStatus(AppConfig.Constants.OnlineModeText, AppConfig.AppColors.OkText);
                }
                else
                {
                    AppConfig.AppInstance.OnlineMode = false;
                    _logger.Info("Cannot connect to the internet, offline mode activated");
                    SetStatus(AppConfig.Constants.OfflineModeText, AppConfig.AppColors.BlueText);
                }
            }
            // Force offline
            else
            {
                AppConfig.AppInstance.OnlineMode = false;
                _logger.Info("Offline mode activated");
                SetStatus(AppConfig.Constants.OfflineModeText, AppConfig.AppColors.BlueText);
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
                new Thread(delegate()
                {
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
            if (!_initializingSettings)
            {
                Properties.Settings.Default.SaveCookies = saveCookiesToolStripMenuItem.Checked;
                Properties.Settings.Default.OfflineByDefault = offlineByDefaultToolStripMenuItem.Checked;
                Properties.Settings.Default.SaveCatalogDetails = saveCatalogDetailsToolStripMenuItem.Checked;
                Properties.Settings.Default.LoggingVerbose = loggingDebugToolStripMenuItem.Checked;

                _loginManager.SetLoggingBoxLogLevel(LogLevel.Debug, loggingDebugToolStripMenuItem.Checked);
                if (sender == loggingDebugToolStripMenuItem)
                    _logger.Debug($"Debug logging set to: {loggingDebugToolStripMenuItem.Checked}");

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
            Folder storedRoot = _fileManager.GetStoredCatalogDetails();
            if (Properties.Settings.Default.SaveCatalogDetails && storedRoot != null)
            {
                _logger.Debug("Using saved catalog details");
                AppConfig.AppInstance.RootFolder = storedRoot;
            }
            else
            {
                _logger.Debug("Getting catalog details from comm");
                string rawCatalogDetails = _comm.GetCatalogDetails();
                _logger.Debug("Serialize catalog details");
                Folder rootFolder = DataParser.ParseCatalogDetails(rawCatalogDetails);
                if (Properties.Settings.Default.SaveCatalogDetails)
                {
                    _logger.Debug("Saving serialized classes to file");
                    _fileManager.SaveCatalogDetails(rootFolder);
                }
                AppConfig.AppInstance.RootFolder = rootFolder;
            }

            if (AppConfig.AppInstance.RootFolder == null)
            {
                _logger.Error("Could not get a root folder");
                return false;
            }
            _logger.Debug("Successfully loaded root folder");
            return true;
        }

        #region GUI events

        private void LectureSelector_Load(object sender, EventArgs e)
        {
            _logger.Debug("Form loaded, attemting to set root folder for catalog details");
            if (SetRootFolder())
            {
                tlvAll.AddObject(AppConfig.AppInstance.RootFolder);
                tlvAll.Expand(AppConfig.AppInstance.RootFolder);
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
            _logForm.Show();
            _logForm.Focus();
        }

        private void viewLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string logFilePath = Path.Combine(
                SimpleLayout.Evaluate(LogManager.Configuration.Variables["logDirectory"].Text) +
                SimpleLayout.Evaluate(LogManager.Configuration.Variables["currentDate"].Text) +
                SimpleLayout.Evaluate(LogManager.Configuration.Variables["logFileExtension"].Text));
                //.Replace('/','\\');
            _logger.Debug($"Trying to open logfile located at \"{logFilePath}\"");
            Process.Start(logFilePath);
        }

        private void loggingEnableToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (loggingEnableToolStripMenuItem.Checked)
            {
                _logForm.Show();
                _logForm.Focus();
                LogManager.EnableLogging();
                _logger.Info("Logging enabled");
            }
            else
            {
                _logForm.Hide();
                LogManager.DisableLogging();
            }
            Properties.Settings.Default.LoggingEnable = loggingEnableToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void deleteSavedCookiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LoginCookieData = "";
            Properties.Settings.Default.Save();
            _logger.Info("Deleted saved cookie (note: Cookies for current session are still cached)");
        }

        private void deleteSavedCredentailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Username = "";
            Properties.Settings.Default.Password = "";
            Properties.Settings.Default.Save();
            _logger.Info("Deleted saved credentials");
        }

        private void resetAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            SetFormSettings();
            _logger.Info("Reset all settings to default");
        }

        List<Folder> _expandedObjects = null;
        string _lastSearchString = "";

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                if (_lastSearchString == "")
                {
                    _expandedObjects = new List<Folder>();
                    foreach (Folder f in tlvAll.ExpandedObjects)
                        _expandedObjects.Add(f);
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
                foreach (Folder f in _expandedObjects)
                    tlvAll.Expand(f);
            }
            _lastSearchString = txtSearch.Text;
        }

        private void tlvAll_ItemActivate(object sender, EventArgs e)
        {
            Folder f = (Folder)tlvAll.SelectedObject;
            olvPresentations.ClearObjects();

            TextOverlay textOverlay = new TextOverlay();
            textOverlay.Alignment = ContentAlignment.MiddleCenter;
            textOverlay.TextColor = Color.Black;
            textOverlay.BackColor = Color.AntiqueWhite;
            textOverlay.BorderColor = Color.Black;
            textOverlay.BorderWidth = 2f;
            textOverlay.Font = new Font("Microsoft Sans Serif", 36);
            textOverlay.Text = "Loading..";

            olvPresentations.OverlayText = textOverlay;

            Thread thread = new Thread(() => ThreadedPresentationGetter(f));
            thread.Start();
        }
        #endregion

        private void ThreadedPresentationGetter(Folder folder)
        {
            JObject o = _comm.GetPresentationsForFolder(folder);
            if (o == null)
            {
                _logger.Error("Could not get presentations for this folder..");
                return;
            }
            Presentation[] presentations = new Presentation[(int)o["TotalItems"]];
            JArray rawPresentations = (JArray) o["PresentationDetailsList"];

            for (int i = 0; i < rawPresentations.Count; i++)
            {
                Presentation presentation = rawPresentations[i].ToObject<Presentation>();
                presentations[i] = presentation;
            }

            olvPresentations.Invoke(new Action(() => PresentationLoader(presentations)));
        }

        private void PresentationLoader(Presentation[] presentations)
        {
            olvPresentations.AddObjects(presentations);
            olvPresentations.OverlayText = null;
        }

        private void olvPresentations_ItemActivate(object sender, EventArgs e)
        {
            Presentation p = (Presentation)olvPresentations.SelectedObject;
            JObject o = _comm.GetPlayerOptionsForPresentation(p);
            if (o == null)
            {
                _logger.Error("Could not get player options for this presentation..");
                return;
            }

            try
            {
                Dictionary<string, string> videoUrls = new Dictionary<string, string>();
                JArray streams = (JArray) o["d"]["Presentation"]["Streams"];
                bool multipleStreams = streams.Count > 1;
                int i = 1;
                foreach (JObject stream in streams)
                {
                    foreach (JObject urls in stream["VideoUrls"])
                    {
                        videoUrls.Add((multipleStreams ? $"Stream{i} - " : null) + $"{urls["MediaType"]}",
                            urls["Location"].ToString());
                    }
                    i++;
                }

                if (videoUrls.Count == 0)
                {
                    _logger.Error("No player options found for this presentation");
                    return;
                }

                (new LectureViewer(videoUrls)).Show();
            }
            catch (Exception)
            {
                _logger.Error("Player options for presentation was unexpected");
            }
        }
    }
}