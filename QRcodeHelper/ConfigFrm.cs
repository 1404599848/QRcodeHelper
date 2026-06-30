using QRcodeHelper.Services;
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
    public partial class ConfigFrm : Form
    {
        public ConfigFrm()
        {
            InitializeComponent();
            LoadConfig();
        }

        private void LoadConfig()
        {
            AppConfig.Load();
            txtPort.Text = AppConfig.SerialPort;
            nudBaud.Value = AppConfig.BaudRate;
            chkAlertEnabled.Checked = AppConfig.AlertEnabled;
            nudFlashMs.Value = AppConfig.FlashIntervalMs;
            nudDuration.Value = AppConfig.AlertDurationSec;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AppConfig.SerialPort = txtPort.Text.Trim();
            AppConfig.BaudRate = (int)nudBaud.Value;
            AppConfig.AlertEnabled = chkAlertEnabled.Checked;
            AppConfig.FlashIntervalMs = (int)nudFlashMs.Value;
            AppConfig.AlertDurationSec = (int)nudDuration.Value;

            if (!string.IsNullOrEmpty(txtNewPwd.Text))
            {
                AppConfig.ConfigPassword = txtNewPwd.Text;
            }

            AppConfig.Save();
            MessageBox.Show("配置已保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
