namespace QRcodeHelper
{
    partial class ConfigFrm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblBaud = new System.Windows.Forms.Label();
            this.nudBaud = new System.Windows.Forms.NumericUpDown();
            this.grpAlert = new System.Windows.Forms.GroupBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.nudDuration = new System.Windows.Forms.NumericUpDown();
            this.lblFlashMs = new System.Windows.Forms.Label();
            this.nudFlashMs = new System.Windows.Forms.NumericUpDown();
            this.grpPwd = new System.Windows.Forms.GroupBox();
            this.lblNewPwd = new System.Windows.Forms.Label();
            this.txtNewPwd = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkAlertEnabled = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaud)).BeginInit();
            this.grpAlert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlashMs)).BeginInit();
            this.grpPwd.SuspendLayout();
            this.SuspendLayout();

            // lblPort
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(15, 20);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(43, 15);
            this.lblPort.Text = "串口：";

            // txtPort
            this.txtPort.Location = new System.Drawing.Point(65, 16);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(60, 23);

            // lblBaud
            this.lblBaud.AutoSize = true;
            this.lblBaud.Location = new System.Drawing.Point(145, 20);
            this.lblBaud.Name = "lblBaud";
            this.lblBaud.Size = new System.Drawing.Size(55, 15);
            this.lblBaud.Text = "波特率：";

            // nudBaud
            this.nudBaud.Location = new System.Drawing.Point(205, 16);
            this.nudBaud.Maximum = 115200;
            this.nudBaud.Minimum = 2400;
            this.nudBaud.Name = "nudBaud";
            this.nudBaud.Size = new System.Drawing.Size(100, 23);
            this.nudBaud.Value = 9600;

            

            // grpAlert
            this.grpAlert.Controls.Add(this.chkAlertEnabled);
            this.grpAlert.Controls.Add(this.lblDuration);
            this.grpAlert.Controls.Add(this.nudDuration);
            this.grpAlert.Controls.Add(this.lblFlashMs);
            this.grpAlert.Controls.Add(this.nudFlashMs);
            this.grpAlert.Location = new System.Drawing.Point(12, 60);
            this.grpAlert.Name = "grpAlert";
            this.grpAlert.Size = new System.Drawing.Size(300, 100);
            this.grpAlert.Text = "报警参数";

            // lblFlashMs
            this.lblFlashMs.AutoSize = true;
            this.lblFlashMs.Location = new System.Drawing.Point(10, 25);
            this.lblFlashMs.Name = "lblFlashMs";
            this.lblFlashMs.Size = new System.Drawing.Size(79, 15);
            this.lblFlashMs.Text = "闪灯间隔ms：";
            // nudFlashMs
            this.nudFlashMs.Location = new System.Drawing.Point(95, 21);
            this.nudFlashMs.Maximum = 5000;
            this.nudFlashMs.Minimum = 100;
            this.nudFlashMs.Increment = 100;
            this.nudFlashMs.Name = "nudFlashMs";
            this.nudFlashMs.Size = new System.Drawing.Size(80, 23);
            this.nudFlashMs.Value = 500;

            // lblDuration
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(190, 25);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(67, 15);
            this.lblDuration.Text = "持续秒数：";
            // nudDuration
            this.nudDuration.Location = new System.Drawing.Point(260, 21);
            this.nudDuration.Maximum = 300;
            this.nudDuration.Minimum = 1;
            this.nudDuration.Name = "nudDuration";
            this.nudDuration.Size = new System.Drawing.Size(30, 23);
            this.nudDuration.Value = 10;

            // chkAlertEnabled
            this.chkAlertEnabled.AutoSize = true;
            this.chkAlertEnabled.Location = new System.Drawing.Point(10, 55);
            this.chkAlertEnabled.Name = "chkAlertEnabled";
            this.chkAlertEnabled.Size = new System.Drawing.Size(90, 21);
            this.chkAlertEnabled.TabIndex = 20;
            this.chkAlertEnabled.Text = "启用警报";

            // grpPwd
            this.grpPwd.Controls.Add(this.lblNewPwd);
            this.grpPwd.Controls.Add(this.txtNewPwd);
            this.grpPwd.Location = new System.Drawing.Point(12, 170);
            this.grpPwd.Name = "grpPwd";
            this.grpPwd.Size = new System.Drawing.Size(300, 60);
            this.grpPwd.Text = "修改密码";

            // lblNewPwd
            this.lblNewPwd.AutoSize = true;
            this.lblNewPwd.Location = new System.Drawing.Point(10, 25);
            this.lblNewPwd.Name = "lblNewPwd";
            this.lblNewPwd.Size = new System.Drawing.Size(67, 15);
            this.lblNewPwd.Text = "新密码：";

            // txtNewPwd
            this.txtNewPwd.Location = new System.Drawing.Point(80, 21);
            this.txtNewPwd.Name = "txtNewPwd";
            this.txtNewPwd.Size = new System.Drawing.Size(120, 23);
            this.txtNewPwd.UseSystemPasswordChar = true;

            // btnSave
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(75, 335);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(170, 335);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // ConfigFrm
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 380);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblBaud);
            this.Controls.Add(this.nudBaud);
            this.Controls.Add(this.grpAlert);
            this.Controls.Add(this.grpPwd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "系统配置";
            ((System.ComponentModel.ISupportInitialize)(this.nudBaud)).EndInit();
            this.grpAlert.ResumeLayout(false);
            this.grpAlert.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlashMs)).EndInit();
            this.grpPwd.ResumeLayout(false);
            this.grpPwd.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblBaud;
        private System.Windows.Forms.NumericUpDown nudBaud;
        private System.Windows.Forms.GroupBox grpAlert;
        private System.Windows.Forms.Label lblFlashMs;
        private System.Windows.Forms.NumericUpDown nudFlashMs;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.NumericUpDown nudDuration;
        private System.Windows.Forms.CheckBox chkAlertEnabled;
        private System.Windows.Forms.GroupBox grpPwd;
        private System.Windows.Forms.Label lblNewPwd;
        private System.Windows.Forms.TextBox txtNewPwd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
