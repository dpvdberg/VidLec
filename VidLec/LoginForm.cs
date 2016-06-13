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
            logger.Debug("Asking comm class to log in");
            Console.WriteLine(txtPassword.Text);
            bool success = comm.Login(txtUsername.Text, txtPassword.Text, chkRemember.Checked, chkSaveCookie.Checked);
            if (success)
            {
                LS.SetStatus("Logged in", AppConfig.AppColors.BlueText);
                this.Close();
            } else
            {
                lblLoginStatus.ForeColor = AppConfig.AppColors.ErrorText;
                lblLoginStatus.Text = "Invalid login, see log..";
            }
        }
    }
}
