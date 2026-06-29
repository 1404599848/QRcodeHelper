namespace QRcodeHelper
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
         }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColQRCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCreationTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColIsPassed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cbLevel = new System.Windows.Forms.ComboBox();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbIsPassed = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.BtnQuery = new System.Windows.Forms.Button();
            this.cbReaders = new System.Windows.Forms.ComboBox();
            this.DataText = new System.Windows.Forms.TextBox();
            this.btnOn = new System.Windows.Forms.Button();
            this.SctBtn = new System.Windows.Forms.CheckBox();
            this.cbNics = new System.Windows.Forms.ComboBox();
            this.SchBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOff = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.liveviewForm1 = new Keyence.AutoID.SDK.LiveviewForm();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mlShowFrm = new System.Windows.Forms.ToolStripMenuItem();
            this.mlQuit = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColId,
            this.ColQRCode,
            this.ColCreationTime,
            this.ColLevel,
            this.ColIsPassed});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(5, 116);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(769, 454);
            this.dataGridView1.TabIndex = 0;
            // 
            // ColId
            // 
            this.ColId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColId.DataPropertyName = "Id";
            this.ColId.HeaderText = "序号";
            this.ColId.MinimumWidth = 6;
            this.ColId.Name = "ColId";
            this.ColId.ReadOnly = true;
            this.ColId.Width = 66;
            // 
            // ColQRCode
            // 
            this.ColQRCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColQRCode.DataPropertyName = "QRCode";
            this.ColQRCode.HeaderText = "二维码";
            this.ColQRCode.MinimumWidth = 6;
            this.ColQRCode.Name = "ColQRCode";
            this.ColQRCode.ReadOnly = true;
            this.ColQRCode.Width = 81;
            // 
            // ColCreationTime
            // 
            this.ColCreationTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColCreationTime.DataPropertyName = "CreationTime";
            this.ColCreationTime.HeaderText = "创建时间";
            this.ColCreationTime.MinimumWidth = 6;
            this.ColCreationTime.Name = "ColCreationTime";
            this.ColCreationTime.ReadOnly = true;
            this.ColCreationTime.Width = 96;
            // 
            // ColLevel
            // 
            this.ColLevel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColLevel.DataPropertyName = "Level";
            this.ColLevel.HeaderText = "判定等级";
            this.ColLevel.MinimumWidth = 6;
            this.ColLevel.Name = "ColLevel";
            this.ColLevel.ReadOnly = true;
            this.ColLevel.Width = 96;
            // 
            // ColIsPassed
            // 
            this.ColIsPassed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColIsPassed.DataPropertyName = "IsPassed";
            this.ColIsPassed.HeaderText = "是否通过";
            this.ColIsPassed.MinimumWidth = 6;
            this.ColIsPassed.Name = "ColIsPassed";
            this.ColIsPassed.ReadOnly = true;
            this.ColIsPassed.Width = 96;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cbLevel);
            this.panel2.Controls.Add(this.dtEnd);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.dtBegin);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cbIsPassed);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtCode);
            this.panel2.Controls.Add(this.btnExport);
            this.panel2.Controls.Add(this.BtnQuery);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(5, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(769, 93);
            this.panel2.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(490, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "判定等级";
            // 
            // cbLevel
            // 
            this.cbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevel.FormattingEnabled = true;
            this.cbLevel.Items.AddRange(new object[] {
            "全部",
            "A",
            "B",
            "C",
            "D",
            "F",
            "-"});
            this.cbLevel.Location = new System.Drawing.Point(568, 15);
            this.cbLevel.Name = "cbLevel";
            this.cbLevel.Size = new System.Drawing.Size(121, 23);
            this.cbLevel.TabIndex = 10;
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(277, 51);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(167, 25);
            this.dtEnd.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(243, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "至";
            // 
            // dtBegin
            // 
            this.dtBegin.Location = new System.Drawing.Point(63, 51);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(167, 25);
            this.dtBegin.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "是否通过";
            // 
            // cbIsPassed
            // 
            this.cbIsPassed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsPassed.FormattingEnabled = true;
            this.cbIsPassed.Items.AddRange(new object[] {
            "全部",
            "通过",
            "未通过"});
            this.cbIsPassed.Location = new System.Drawing.Point(326, 15);
            this.cbIsPassed.Name = "cbIsPassed";
            this.cbIsPassed.Size = new System.Drawing.Size(121, 23);
            this.cbIsPassed.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "二维码";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(72, 14);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(127, 25);
            this.txtCode.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport.Location = new System.Drawing.Point(618, 47);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(72, 33);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // BtnQuery
            // 
            this.BtnQuery.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnQuery.Location = new System.Drawing.Point(507, 47);
            this.BtnQuery.Name = "BtnQuery";
            this.BtnQuery.Size = new System.Drawing.Size(72, 33);
            this.BtnQuery.TabIndex = 0;
            this.BtnQuery.Text = "查询";
            this.BtnQuery.UseVisualStyleBackColor = true;
            this.BtnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // cbReaders
            // 
            this.cbReaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReaders.Enabled = false;
            this.cbReaders.FormattingEnabled = true;
            this.cbReaders.Location = new System.Drawing.Point(7, 66);
            this.cbReaders.Margin = new System.Windows.Forms.Padding(4);
            this.cbReaders.Name = "cbReaders";
            this.cbReaders.Size = new System.Drawing.Size(262, 23);
            this.cbReaders.TabIndex = 6;
            // 
            // DataText
            // 
            this.DataText.BackColor = System.Drawing.SystemColors.Control;
            this.DataText.Location = new System.Drawing.Point(15, 185);
            this.DataText.Margin = new System.Windows.Forms.Padding(4);
            this.DataText.MaxLength = 10;
            this.DataText.Multiline = true;
            this.DataText.Name = "DataText";
            this.DataText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DataText.Size = new System.Drawing.Size(317, 62);
            this.DataText.TabIndex = 5;
            // 
            // btnOn
            // 
            this.btnOn.Enabled = false;
            this.btnOn.Location = new System.Drawing.Point(15, 110);
            this.btnOn.Margin = new System.Windows.Forms.Padding(4);
            this.btnOn.Name = "btnOn";
            this.btnOn.Size = new System.Drawing.Size(107, 32);
            this.btnOn.TabIndex = 4;
            this.btnOn.Text = "触发";
            this.btnOn.UseVisualStyleBackColor = true;
            this.btnOn.Click += new System.EventHandler(this.btnON_Click);
            // 
            // SctBtn
            // 
            this.SctBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.SctBtn.AutoSize = true;
            this.SctBtn.BackColor = System.Drawing.Color.Transparent;
            this.SctBtn.Enabled = false;
            this.SctBtn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SctBtn.Location = new System.Drawing.Point(278, 65);
            this.SctBtn.Margin = new System.Windows.Forms.Padding(4);
            this.SctBtn.Name = "SctBtn";
            this.SctBtn.Size = new System.Drawing.Size(77, 25);
            this.SctBtn.TabIndex = 8;
            this.SctBtn.Text = "连接设备";
            this.SctBtn.UseVisualStyleBackColor = false;
            this.SctBtn.CheckedChanged += new System.EventHandler(this.SctBtn_CheckedChanged);
            // 
            // cbNics
            // 
            this.cbNics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNics.FormattingEnabled = true;
            this.cbNics.Location = new System.Drawing.Point(7, 30);
            this.cbNics.Margin = new System.Windows.Forms.Padding(4);
            this.cbNics.Name = "cbNics";
            this.cbNics.Size = new System.Drawing.Size(262, 23);
            this.cbNics.TabIndex = 9;
            // 
            // SchBtn
            // 
            this.SchBtn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SchBtn.Location = new System.Drawing.Point(277, 27);
            this.SchBtn.Margin = new System.Windows.Forms.Padding(4);
            this.SchBtn.Name = "SchBtn";
            this.SchBtn.Size = new System.Drawing.Size(77, 29);
            this.SchBtn.TabIndex = 7;
            this.SchBtn.Text = "搜索";
            this.SchBtn.UseVisualStyleBackColor = true;
            this.SchBtn.Click += new System.EventHandler(this.SchBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOff);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.liveviewForm1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbNics);
            this.groupBox1.Controls.Add(this.DataText);
            this.groupBox1.Controls.Add(this.cbReaders);
            this.groupBox1.Controls.Add(this.SchBtn);
            this.groupBox1.Controls.Add(this.btnOn);
            this.groupBox1.Controls.Add(this.SctBtn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 575);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "配置";
            // 
            // btnOff
            // 
            this.btnOff.Enabled = false;
            this.btnOff.Location = new System.Drawing.Point(147, 110);
            this.btnOff.Margin = new System.Windows.Forms.Padding(4);
            this.btnOff.Name = "btnOff";
            this.btnOff.Size = new System.Drawing.Size(107, 32);
            this.btnOff.TabIndex = 15;
            this.btnOff.Text = "触发结束";
            this.btnOff.UseVisualStyleBackColor = true;
            this.btnOff.Click += new System.EventHandler(this.btnOff_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "实时画面：";
            // 
            // liveviewForm1
            // 
            this.liveviewForm1.BackColor = System.Drawing.Color.Black;
            this.liveviewForm1.BinningType = Keyence.AutoID.SDK.LiveviewForm.ImageBinningType.OneQuarter;
            this.liveviewForm1.ImageFormat = Keyence.AutoID.SDK.LiveviewForm.ImageFormatType.Jpeg;
            this.liveviewForm1.ImageQuality = 5;
            this.liveviewForm1.IpAddress = "192.168.100.100";
            this.liveviewForm1.Location = new System.Drawing.Point(15, 295);
            this.liveviewForm1.Margin = new System.Windows.Forms.Padding(5);
            this.liveviewForm1.Name = "liveviewForm1";
            this.liveviewForm1.PullTimeSpan = 100;
            this.liveviewForm1.Size = new System.Drawing.Size(331, 272);
            this.liveviewForm1.TabIndex = 13;
            this.liveviewForm1.TimeoutMs = 2000;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "读取内容：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(380, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox2.Size = new System.Drawing.Size(779, 575);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "记录";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "3GRC";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mlShowFrm,
            this.mlQuit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 52);
            this.contextMenuStrip1.Text = "123";
            // 
            // mlShowFrm
            // 
            this.mlShowFrm.Name = "mlShowFrm";
            this.mlShowFrm.Size = new System.Drawing.Size(153, 24);
            this.mlShowFrm.Text = "显示主界面";
            this.mlShowFrm.Click += new System.EventHandler(this.mlShowFrm_Click);
            // 
            // mlQuit
            // 
            this.mlQuit.Name = "mlQuit";
            this.mlQuit.Size = new System.Drawing.Size(153, 24);
            this.mlQuit.Text = "退出";
            this.mlQuit.Click += new System.EventHandler(this.mlQuit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 585);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "3GCR";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BtnQuery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbIsPassed;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColQRCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCreationTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColIsPassed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbLevel;
        private System.Windows.Forms.ComboBox cbReaders;
        private System.Windows.Forms.TextBox DataText;
        private System.Windows.Forms.Button btnOn;
        private System.Windows.Forms.CheckBox SctBtn;
        private System.Windows.Forms.ComboBox cbNics;
        private System.Windows.Forms.Button SchBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private Keyence.AutoID.SDK.LiveviewForm liveviewForm1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOff;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mlShowFrm;
        private System.Windows.Forms.ToolStripMenuItem mlQuit;
    }
}

