﻿using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using NLog.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VidLec
{
    public partial class LectureSelector : Form
    {
        private static LogForm logForm = new LogForm();
        private static Logger logger;
        Comm comm;
        
        public LectureSelector()
        {
            InitializeComponent();
            if (Properties.Settings.Default.loggingEnable)
            {
                // Show log form before creating loggers
                logForm.Show();
                // Create loggers
                logger = LogManager.GetCurrentClassLogger();
                logger.Debug("Created logger");
            }
            // Setup this form
            SetupListViews();
            SetFormSettings();            
            comm = new Comm();
            // Handle network status
            AppConfig.AppInstance.onlineMode = comm.CheckNet();
            ChangeNetworkStatus(!Properties.Settings.Default.OfflineByDefault);
        }

        /// <summary>
        /// Sets the form default settings
        /// </summary>
        private void SetFormSettings()
        {
            loggingDebugToolStripMenuItem.Checked = Properties.Settings.Default.loggingVerbose;
            loggingEnableToolStripMenuItem.Checked = Properties.Settings.Default.loggingEnable;
        }

        /// <summary>
        /// Make sure the user is logged in
        /// i.e. we have a cookie or username + password
        /// </summary>
        private void EnsureLogin()
        {
            if ((Properties.Settings.Default.Username == "" || Properties.Settings.Default.password == "") &&
                Properties.Settings.Default.loginCookieData == "" && AppConfig.AppInstance.onlineMode)
            {
                logger.Info("Online mode and no credentials or cookie data saved, asking for login..");
                SetStatus("Asking for login", AppConfig.AppColors.BlueText);
                (new LoginForm(this)).Show();
            }
        }

        /// <summary>
        /// Sets the logger config
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
                    SetStatus(AppConfig.Constants.onlineModeText, AppConfig.AppColors.OKText);
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


        private void loggingDebugToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        { 
            SetBoxLoggingLevel(LogLevel.Debug, loggingDebugToolStripMenuItem.Checked);
            logger.Debug(string.Format("Debug logging set to: {0}", loggingDebugToolStripMenuItem.Checked));
        }


        private void openLogWindowStripMenuItem_Click(object sender, EventArgs e)
        {
            logForm.Show();
            logForm.Focus();
        }

        private void loggingEnableToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (loggingEnableToolStripMenuItem.Checked)
            {
                logForm.Show();
                logForm.Focus();
                LogManager.EnableLogging();
                logger.Info("Logging enabled");
            } else {
                logForm.Hide();
                LogManager.DisableLogging();
            }
        }

        private void viewLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string logFilePath = Path.Combine(Path.GetTempPath(),
                ((Type)typeof(LectureSelector)).Namespace,
                AppConfig.Constants.logSubDir,
                SimpleLayout.Evaluate(LogManager.Configuration.Variables["currentDate"].Text) + SimpleLayout.Evaluate(LogManager.Configuration.Variables["logFileExtension"].Text));
            logger.Debug(string.Format("Trying to open logfile located at \"{0}\"", logFilePath));
            Process.Start(logFilePath);
        }

        private void LectureSelector_Shown(object sender, EventArgs e)
        {
            EnsureLogin();
        }

        #endregion
    }
}
