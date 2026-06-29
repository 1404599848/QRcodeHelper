using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using Keyence.AutoID.SDK;
using QRcodeHelper.Models;
using QRcodeHelper.Services;

namespace QRcodeHelper
{
    public partial class MainForm : Form
    { 
        private ReaderAccessor m_reader = new ReaderAccessor();
        private ReaderSearcher m_searcher = new ReaderSearcher();
        List<NicSearchResult> m_nicList = new List<NicSearchResult>();
        [DllImport("user32.dll")]
        public static extern int MessageBeep(uint uType);
        //发出不同类型的声音的参数如下：  
        //Ok = 0x00000000,  
        //Error = 0x00000010,  
        //Question = 0x00000020,  
        //Warning = 0x00000030,  
        //Information = 0x00000040  
        readonly uint beepOk = 0x00000000;
        readonly uint beepError = 0x00000010;
        private InfoFrm _infoFrm;
        [DllImport("kernel32.dll", EntryPoint = "Beep")]
        // 第一个参数是指频率的高低，越大越高，第二个参数是指响的时间多长  
        public static extern int Beep(
            int dwFreq,
            int dwDuration
            );

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //居中
            //var headerStyle = new DataGridViewCellStyle();
            //headerStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dataGridView1.ColumnHeadersDefaultCellStyle = headerStyle;
            //this.dataGridView1.RowsDefaultCellStyle = headerStyle;
            dataGridView1.RowHeadersVisible = false;
            DatabaseService.InitSQLiteDb();
            //DatabaseService.TestData();

            if (!Directory.Exists("./Exports"))
                Directory.CreateDirectory("./Exports");

            cbIsPassed.SelectedIndex = 0;
            cbLevel.SelectedIndex = 0;

            m_nicList = m_searcher.ListUpNic();
            if (m_nicList != null && m_nicList.Count > 0)
            {
                for (int i = 0; i < m_nicList.Count; i++)
                {
                    cbNics.Items.Add(m_nicList[i].NicIpAddr + "/" + m_nicList[i].NicName);
                }
                var lastIndex = int.Parse(ConfigurationManager.AppSettings["LastIndex"]);
                if (lastIndex < m_nicList.Count - 1)
                    cbNics.SelectedIndex = lastIndex;
                else
                    cbNics.SelectedIndex = 0;
            }
            else
            {
                SchBtn.Enabled = false;
            }
        }

        private void SchBtn_Click(object sender, EventArgs e)
        {           
            //m_searcher.IsSearching is true while searching readers.
            if (!m_searcher.IsSearching)
            {
                m_searcher.SelectedNicSearchResult = m_nicList[cbNics.SelectedIndex];
                cbNics.Enabled = false;
                SchBtn.Enabled = false;
                SctBtn.Enabled = false;
                cbReaders.Items.Clear();
                //Start searching readers.
                m_searcher.Start((res) =>
                {
                    //Define searched actions here.Defined actions work asynchronously.
                    //"SearchListUp" works when a reader was searched.
                    BeginInvoke(new delegateUserControl(SearchListUp), res.IpAddress);
                });
            }
        }
        private void SctBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (cbReaders.Items.Count == 0)
            {
                SctBtn.Checked = false;
                return;
            }
            if (SctBtn.Checked)
            {
                SctBtn.Text = "断开连接";
                if (cbReaders.SelectedItem != null)
                {
                    //Stop liveview.
                    liveviewForm1.EndReceive();
                    //Set ip address of liveview.
                    liveviewForm1.IpAddress = cbReaders.SelectedItem.ToString();
                    //Start liveview.
                    liveviewForm1.BeginReceive();
                    //Set ip address of ReaderAccessor.
                    m_reader.IpAddress = cbReaders.SelectedItem.ToString();
                    //Connect TCP/IP.
                    m_reader.Connect((data) =>
                    {
                        //Define received data actions here.Defined actions work asynchronously.
                        //"ReceivedDataWrite" works when reading data was received.
                        BeginInvoke(new delegateUserControl(ReceivedDataWrite), Encoding.ASCII.GetString(data));
                    });
                    cbNics.Enabled = false;
                    SchBtn.Enabled = false;
                    cbReaders.Enabled = false;
                    btnOn.Enabled = true;
                    btnOff.Enabled = true;
                    SchBtn.Enabled = true;
                }
            }
            else
            {
                SctBtn.Text = "连接设备";
                cbNics.Enabled = true;
                cbReaders.Enabled = true;
                SctBtn.Enabled = true;
                btnOn.Enabled = false;
                btnOff.Enabled = false;
                liveviewForm1.EndReceive();
                ReceivedDataWrite(m_reader.ExecCommand("LOFF"));
                m_reader.Disconnect();
            }
        }
        private void btnON_Click(object sender, EventArgs e)
        {
            if(cbReaders.SelectedItem != null)
            {
                ReceivedDataWrite(m_reader.ExecCommand("LON"));
            }
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            if (cbReaders.SelectedItem != null)
            {
                ReceivedDataWrite($@"触发结束{m_reader.ExecCommand("LOFF")}");
            }
        }

        //delegateUserControl is for controlling UserControl from other threads.
        private delegate void delegateUserControl(string str);
        private void ReceivedDataWrite(string receivedData)
        {
            DataText.Text= $@"[{DateTime.Now}]  {receivedData}";
            if (!string.IsNullOrWhiteSpace(receivedData) && receivedData.Length > 0 && receivedData.Contains(":"))
            {
                var infos = receivedData.Split(':');
                var record = new QRCodeRecord()
                {
                    QRCode = infos[0],
                    Level = infos[1].Substring(0, 1)
                };
                using (var conn = DatabaseService.CreateConnection())
                {
                    conn.Execute(@"
                    INSERT INTO QRCodeRecords VALUES(
                        NULL, @QRCode, @Level, @CreationTime, @IsPassed
                    )", record);
                }

                if (record.IsPassed == IsPassed.通过)
                    MessageBeep(beepOk);
                else
                {
                    Task.Run(() =>
                    {
                        MessageBeep(beepError);
                        Thread.Sleep(2000);
                        MessageBeep(beepError);
                    });

                    this.WindowState = FormWindowState.Normal;
                    //激活窗体并给予它焦点
                    this.Activate();
                    this.Show();

                    if (_infoFrm != null)
                    {
                        _infoFrm.Close();
                        _infoFrm.Dispose();
                    }
                    _infoFrm = new InfoFrm(record.QRCode, record.Level);
                    _infoFrm.ShowDialog();
                }
            }
        }
        private void SearchListUp(string ip)
        {
            if (ip != "")
            {
                cbNics.Enabled = true;
                SchBtn.Enabled = true;
                SctBtn.Enabled = true;
                cbReaders.Enabled = true;
                cbReaders.Items.Add(ip);
                cbReaders.SelectedIndex = cbReaders.Items.Count - 1;
                XmlHelper.ChangeConfig("LastIndex", cbNics.SelectedIndex.ToString());
            }
            else
            {
                cbNics.Enabled = true;
                SchBtn.Enabled = true;
                cbReaders.Enabled = true;
                SctBtn.Enabled = true;
            }
        }
        //Dispose before closing Form for avoiding error.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //this.WindowState = FormWindowState.Minimized;
                this.Hide();
                //隐藏任务栏区图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                notifyIcon1.Visible = true;

                e.Cancel = true;
            }
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            var beginTime = dtBegin.Value.Date;
            var endTime = dtEnd.Value.Date.AddDays(1);

            string sqlIsPassed = null;
            if (cbIsPassed.Text == "通过")
                sqlIsPassed = "AND IsPassed = 0";
            else if (cbIsPassed.Text == "未通过")
                sqlIsPassed = "AND IsPassed = 1";

            string sqlLevel = null;
            if (cbLevel.Text != "全部")
                sqlLevel = $@"AND Level = '{cbLevel.Text}'";


            using (var conn = DatabaseService.CreateConnection())
            {
                var records = conn.Query<QRCodeRecord>($@"SELECT * FROM QRCodeRecords WHERE CreationTime >= @beginTime AND CreationTime < @endTime {sqlIsPassed} {sqlLevel} AND QRCode like '%{txtCode.Text}%'", new { beginTime, endTime});

                dataGridView1.DataSource = records;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var beginTime = dtBegin.Value.Date;
            var endTime = dtEnd.Value.Date.AddDays(1);

            string sqlIsPassed = null;
            if (cbIsPassed.Text == "通过")
                sqlIsPassed = "AND IsPassed = 0";
            else if (cbIsPassed.Text == "未通过")
                sqlIsPassed = "AND IsPassed = 1";

            string sqlLevel = null;
            if (cbLevel.Text != "全部")
                sqlLevel = $@"AND Level = '{cbLevel.Text}'";


            using (var conn = DatabaseService.CreateConnection())
            {
                var records = conn.Query<QRCodeRecord>($@"SELECT * FROM QRCodeRecords WHERE CreationTime >= @beginTime AND CreationTime < @endTime {sqlIsPassed} {sqlLevel} AND QRCode like '%{txtCode.Text}%'", new { beginTime, endTime });

                var dataMap = new Dictionary<string, string>()
                {
                    { "Id", "序号" },
                    { "QRCode", "二维码" },
                    { "CreationTime", "创建时间" },
                    { "Level", "判定等级" },
                    { "IsPassed", "是否通过" },
                };

                var excelFileGenerator =
                    new ExcelFileGenerator<QRCodeRecord>(records.ToList());
                var excelMemoryStream = excelFileGenerator.ExportDataToExcel(dataMap);
                File.WriteAllBytes($@"./Exports/二维码记录_{DateTime.Now:yyyyMMddHHmmss}.xls", excelMemoryStream.ToArray());

                MessageBox.Show("导出成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            using (var conn = DatabaseService.CreateConnection())
            {
                Random random = new Random();
                var record = new QRCodeRecord()
                {
                    QRCode = "Test" + random.Next(100, 1000),
                    Level = "C"
                };


                DataText.Text = $@"二维码{record.QRCode}, 等级{record.Level}";
                conn.Execute(@"
                    INSERT INTO QRCodeRecords VALUES(
                        NULL, @QRCode, @Level, @CreationTime, @IsPassed
                    )", record);

                var frm = new InfoFrm($@"二维码:{record.QRCode}", "C");
                frm.ShowDialog();
            }
        }

        private void mlQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                m_reader.ExecCommand("LOFF");
                m_reader.Dispose();
                m_searcher.Dispose();
                liveviewForm1.Dispose();
                System.Environment.Exit(0);
                this.Dispose();
                this.Close();
            }
        }

        private void mlShowFrm_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //还原窗体显示    
            this.WindowState = FormWindowState.Normal;
            //激活窗体并给予它焦点
            this.Activate();
            this.Show();
            //任务栏区显示图标
            this.ShowInTaskbar = true;
            //托盘区图标隐藏
            notifyIcon1.Visible = true;
        }
    }
}
