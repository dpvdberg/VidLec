using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VidLec
{
    public partial class LoginForm : Form
    {
        Comm comm = new Comm();
        Logger logger = LogManager.GetCurrentClassLogger();
        LectureSelector LS;

        public LoginForm(LectureSelector LS)
        {
            // Pass the LectureSelector instance
            this.LS = LS;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LS.SetStatus(AppConfig.Constants.loggingInText, AppConfig.AppColors.OKText);
            lblLoginStatus.ForeColor = AppConfig.AppColors.OKText;
            lblLoginStatus.Text = "Logging in..";
            if (!bgwLogin.IsBusy)
                bgwLogin.RunWorkerAsync();
        }

        private void bgwLogin_DoWork(object sender, DoWorkEventArgs e)
        {
            logger.Debug("Asking comm class to log in");
            Properties.Settings.Default.SaveCookies = chkSaveCookie.Checked;
            Properties.Settings.Default.Save();
            e.Result = comm.Login(txtUsername.Text, txtPassword.Text, chkRemember.Checked, chkSaveCookie.Checked);
        }

        private void bgwLogin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool success = (bool)e.Result;
            if (success)
            {
                LS.SetStatus(AppConfig.Constants.loggedIn, AppConfig.AppColors.OKText);
                this.Close();
            }
            else
            {
                lblLoginStatus.ForeColor = AppConfig.AppColors.ErrorText;
                lblLoginStatus.Text = "Invalid login, see log..";
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            
            toolTip1.AutoPopDelay = toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            
            toolTip1.SetToolTip(this.chkSaveCookie, "This is recommended\nAuto-login will be considerably faster");
        }
    }
}
