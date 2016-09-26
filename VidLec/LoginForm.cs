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
        Logger _logger;
        Comm _comm;

        public LoginForm()
        {
            // First initialize the logger
            if (Properties.Settings.Default.LoggingEnable)
                _logger = LogManager.GetCurrentClassLogger();
            _comm = new Comm();            
            InitializeComponent();
            chkSaveCookie.Checked = Properties.Settings.Default.SaveCookies;
            txtUsername.Text = Properties.Settings.Default.Username;
            txtPassword.Text = Properties.Settings.Default.Password;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!bgwLogin.IsBusy)
            {
                SetStatus(AppConfig.Constants.LoggingInText, AppConfig.AppColors.OkText);
                bgwLogin.RunWorkerAsync();
            }
        }

        private void bgwLogin_DoWork(object sender, DoWorkEventArgs e)
        {
            _logger.Debug("Asking comm class to log in");
            Properties.Settings.Default.SaveCookies = chkSaveCookie.Checked;
            Properties.Settings.Default.Save();
            e.Result = _comm.Login(txtUsername.Text, txtPassword.Text, chkRemember.Checked, chkSaveCookie.Checked);
        }

        private void bgwLogin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((bool)e.Result)
            {
                this.DialogResult = DialogResult.OK;
                Close();
            } else
            {
                SetStatus("Log-in failed", AppConfig.AppColors.ErrorText);
            }
        }

        private void SetStatus(string status, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = status;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            
            toolTip1.AutoPopDelay = toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            
            toolTip1.SetToolTip(this.chkSaveCookie, "This is recommended\nAuto-login will be considerably faster");
        }

        private void btnOffline_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
