using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        Config cfg = new Config();
        Comm comm = new Comm();
        
        public LectureSelector()
        {
            InitializeComponent();
            SetupListViews();
            logForm.Show();
            logger = LogManager.GetLogger("LectureSelector");
            logger.Debug("Created logger");
            cfg.onlineMode = comm.CheckNet();
            ChangeNetworkStatus(true);
        }

        private void SetupListViews()
        {
            // Configure the first tree
            tlvAll.CanExpandGetter = delegate (object x) { return ((Folder)x).childFolders.Count > 0 ? true : false; };
            tlvAll.ChildrenGetter = delegate (object x) { return ((Folder)x).childFolders; };

            tlvAllClmCount.AspectToStringConverter = delegate (object x)
            {
                if (x is int)
                {
                    int f = (int)x;
                    return f == 0 ? "" : f.ToString();
                }
                else
                {
                    return "";
                }
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
        /// Tries to change network status
        /// </summary>
        /// <param name="setOnline"></param>
        public void ChangeNetworkStatus(bool setOnline)
        {
            // Try to connect to the internet
            if (setOnline)
            {
                if (comm.CheckNet())
                {
                    cfg.onlineMode = true;
                    ConfigureNetworkStatus(true);
                    logger.Info("Connected to the internet");
                    SetStatus(Config.Constants.onlineModeText, Config.AppColors.OKText);
                }
                else
                {
                    cfg.onlineMode = false;
                    logger.Info("Cannot connect to the internet");
                    SetStatus(Config.Constants.offlineModeText, Config.AppColors.BlueText);
                }
            }
            // Force offline
            else
            {
                cfg.onlineMode = false;
                logger.Info("Offline mode activated");
                SetStatus(Config.Constants.offlineModeText, Config.AppColors.BlueText);
            }
        }

        /// <summary>
        /// Initializes the program in network state
        /// </summary>
        public void ConfigureNetworkStatus(bool SetOnline)
        {

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

        #endregion
    
    }
}
