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
    public partial class LogForm : Form
    {
        private double backgroundOpacity = 0.4;
        public LogForm()
        {
            this.Opacity = backgroundOpacity;
            InitializeComponent();
        }

        private void LogForm_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1.0;
        }

        private void LogForm_Deactivate(object sender, EventArgs e)
        {
            if (!this.Disposing)
                this.Opacity = backgroundOpacity;
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
