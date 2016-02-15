using BrightIdeasSoftware;
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
        Config cfg = new Config();
        Comm comm = new Comm();

        public LectureSelector()
        {
            InitializeComponent();
            cfg.onlineMode = comm.CheckNet();
            SetupListViews();
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
                    if (f == 0)
                        return "";
                    else
                        return f.ToString();
                }
                else
                    return "";
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

            if (cfg.onlineMode)
            {
                SetStatus(Config.Constants.onlineModeText, Config.AppColors.OKText);
                //loadFoldersAsync();
            }
            else
            {
                SetStatus(Config.Constants.offlineModeText, Config.AppColors.BlueText);
            }
        }

        public void SetStatus(string status, Color color)
        {
            toolStripStatusLabel.ForeColor = color;
            toolStripStatusLabel.Text = status;
        }
    }
}
