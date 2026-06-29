using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRcodeHelper
{
    public partial class InfoFrm : Form
    {
        private TimeSpan Timeout;

        public InfoFrm(string info, string Level)
        {
            InitializeComponent();
            lbInfo.Text = info;
            lbLevel.Text = Level;

            timerCountdown.Start();
            Timeout = new TimeSpan(0, 0, 30);
            timerCountdown.Tick += TimerCountdown_Tick;
            timerCountdown.Interval = 1000;
        }

        private void TimerCountdown_Tick(object sender, EventArgs e)
        {
            Timeout = Timeout.Subtract(new TimeSpan(0, 0, 1));

            if (Timeout.TotalSeconds < 0.0)//当倒计时完毕时
            {
                timerCountdown.Stop();
                lbTime.Text = null;

                Timeout = new TimeSpan(0, 0, 30);
                this.DialogResult = DialogResult.Cancel;
            }
            lbTime.Text = Timeout.Seconds.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
