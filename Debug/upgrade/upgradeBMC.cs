using BmcUpgradeTool;
using Debug.tools;
using Debug.upgrade;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Debug.MRForm;
using static System.Windows.Forms.AxHost;

namespace Debug {

    public partial class UpgradeBMC : Form, InterfaceINI{
        public UpgradeBMC() {
            InitializeComponent();
        }
        


        private class AreaZone {
            public int min;
            public int max;
            public AreaZone(int min, int max) {
                this.min = min;
                this.max = max;
            }
        }

        private static int CONST_MIN = 50000;
        private static int CONST_MAX = 50000;
        AreaZone[] g_areaFilter = new AreaZone[]{
            //new AreaZone(112578 - CONST_MIN, 112578+CONST_MAX), // 112578
            //new AreaZone(199839 - CONST_MIN, 199839+CONST_MAX), // 199839
            new AreaZone(193256 - CONST_MIN, 193256+CONST_MAX), // 112578
        };

        string key_loginName = "loginName";
        string key_loginPwd = "loginPwd";
        string key_fileTelnet = "fileTelnet";
        string key_fileHpm = "fileHpm";

        public void loadINI()
        {
            IniHelper iniHelper = new IniHelper();
            iniHelper.IniLoader2Form(this);

            string loginName = iniHelper.getString(this.Text, key_loginName, "Administrator");
            string loginPwd = iniHelper.getString(this.Text, key_loginPwd, "ttytty`12");
            string fileTelnet = iniHelper.getString(this.Text, key_fileTelnet, string.Empty);
            string fileHpm = iniHelper.getString(this.Text, key_fileHpm, string.Empty);
            try
            {
                tb_loginUserName.Text = loginName;
                tb_loginPwd.Text = loginPwd;
                tb_fileTelnet.Text = fileTelnet;
                tb_fileHpm.Text = fileHpm;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void updateINI()
        {
            IniHelper iniHelper = new IniHelper();
            iniHelper.IniUpdate2File(this);
            
            iniHelper.writeString(this.Text, key_loginName, tb_loginUserName.Text);
            iniHelper.writeString(this.Text, key_loginPwd, tb_loginPwd.Text);
            iniHelper.writeString(this.Text, key_fileTelnet, tb_fileTelnet.Text);
            iniHelper.writeString(this.Text, key_fileHpm, tb_fileHpm.Text);
        }

        private void UpgradeBMC_Load(object sender, EventArgs e)
        {
            loadINI();
            //DataGridViewInit();
            // 初始化进度管理器
            BMCProgressManager progressManager = new BMCProgressManager(dg_upgradeProcessBar);
                // 添加列头点击事件
            dg_upgradeProcessBar.ColumnHeaderMouseClick += DataGridView1_ColumnHeaderMouseClick;
        }

        private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 只对 IP 列进行排序
            if (dg_upgradeProcessBar.Columns[e.ColumnIndex].Name == "IP")
            {
                //SortDataGridViewByIP();
            }
        }
        private void UpgradeBMC_FormClosing(object sender, FormClosingEventArgs e)
        {
            updateINI();
        }

        private void tb_fileTelnet_DoubleClick(object sender, EventArgs e)
        {
            //弹出打开 选择 telnet 破解文件的 对话框
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "telnet破解文件|*.hpm";

            if(fileDialog.ShowDialog() == DialogResult.OK) {
                tb_fileTelnet.Text = fileDialog.FileName;
            }
        }

        private void tb_fileBMCUpgradeHpm_DoubleClick(object sender, EventArgs e)
        {//弹出打开 hpm升级包 对话框
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "hpm升级包|*.hpm";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                tb_fileHpm.Text = fileDialog.FileName;
            }
        }
        private Dictionary<string, DataGridViewRow> ipRowMap = new Dictionary<string, DataGridViewRow>();

        private void UpdateDataGridView(BMCProgressInfo progressInfo)
        {
            string ip = progressInfo.ip;
            int percent = progressInfo.percent;
            string stage = progressInfo.stage;
            if (dg_upgradeProcessBar.InvokeRequired)
            {
                dg_upgradeProcessBar.Invoke(new Action(() => UpdateDataGridView(progressInfo)));
                return;
            }

            // 查找或创建行
            DataGridViewRow row;
            if (!ipRowMap.ContainsKey(ip))
            {
                // 添加新行
                row = new DataGridViewRow();
                row.CreateCells(dg_upgradeProcessBar);
                row.Cells[0].Value = ip;
                dg_upgradeProcessBar.Rows.Add(row);
                ipRowMap[ip] = row;

                // 添加后立即排序
                SortDataGridViewByIP();
            }
            else
            {
                row = ipRowMap[ip];
            }

            // 更新进度和阶段
            row.Cells[1].Value = $"{percent}%";
            row.Cells[2].Value = stage;
            row.Cells[3].Value = DateTime.Now.ToString("HH:mm:ss");

            // 根据百分比设置颜色
            if (percent == 100)
            {
                row.Cells[1].Style.BackColor = Color.LightGreen;
                row.Cells[1].Style.ForeColor = Color.DarkGreen;
                row.Cells[2].Value = "完成";
            }
            else
            {
                row.Cells[1].Style.BackColor = Color.LightPink;
                row.Cells[1].Style.ForeColor = Color.DarkRed;
            }
        }
        private void SortDataGridViewByIP()
        {
            if (dg_upgradeProcessBar.Rows.Count == 0) return;

            // 保存所有数据
            var rowsData = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dg_upgradeProcessBar.Rows)
            {
                // 创建新行并复制数据
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dg_upgradeProcessBar);

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    newRow.Cells[i].Value = row.Cells[i].Value;
                    newRow.Cells[i].Style = row.Cells[i].Style.Clone();
                }
                rowsData.Add(newRow);
            }

            // 排序（基于原始 IP）
            var sortedRows = rowsData
                .OrderBy(r => IPToLong(r.Cells[0].Value?.ToString()))
                .ToList();

            // 重新填充
            dg_upgradeProcessBar.Rows.Clear();
            ipRowMap.Clear();

            foreach (var row in sortedRows)
            {
                string ip = row.Cells[0].Value?.ToString();
                dg_upgradeProcessBar.Rows.Add(row);
                ipRowMap[ip] = row;
            }
        }
       
        private long IPToLong(string ip)
        {
            if (string.IsNullOrEmpty(ip)) return 0;

            string[] parts = ip.Split('.');
            if (parts.Length != 4) return 0;

            long result = 0;
            for (int i = 0; i < 4; i++)
            {
                if (byte.TryParse(parts[i], out byte b))
                {
                    result = (result << 8) | b;
                }
            }
            return result;
        }

        // 在你的升级方法中调用
        private void updateProcessBar(string message)
        {
            ParseToProgressInfo parseToProgressInfo = new ParseToProgressInfo();
            BMCProgressInfo progressInfo = parseToProgressInfo.ParseProgressInfo(message);

            UpdateDataGridView(progressInfo);
        }

        public void tb_upgradeLog_AppendText(string message)
        {
            if (message.Contains("升级中") || message.Contains("破解中"))
            {
                updateProcessBar(message);
            }
            if (message.EndsWith(Environment.NewLine) || tb_upgradeLog.Lines.Length == 0)
            {
                tb_upgradeLog.AppendText($"{DateTime.Now:HH:mm:ss} {message}");
            }
            else
            {
                // 获取除最后一行外的所有内容

                var lines = tb_upgradeLog.Lines;
                if (lines.Length > 0 && string.IsNullOrEmpty(lines[lines.Length - 1]))
                {
                    lines = lines.Take(lines.Length - 1).ToArray();
                }
                var allButLast = string.Join(Environment.NewLine, lines, 0, lines.Length - 1); //上一行的内容

                // 重新设置文本：保留前面的行 + 新的最后一行
                tb_upgradeLog.Text = allButLast +  $"\r{DateTime.Now:HH:mm:ss} " + message;
            }
            // 如果是 WinForms，还可以让滚动条自动滚到最下方
            tb_upgradeLog.SelectionStart = tb_upgradeLog.Text.Length;
            tb_upgradeLog.ScrollToCaret();
        }
        private async void bt_telnet_Click(object sender, EventArgs e)
        {
            // // 解析用户输入的多个IP尾数
            string[] ipTails = GetRange.GetIPRange(cb_upgradeIP.Text.Trim()).ToArray();
            if (ipTails.Length == 0)
            {
                Console.WriteLine("指定 的IP 地址，格式错误");
                return;
            }
            // 禁用按钮防止重复点击
            bt_telnet.Enabled = false;
            string filePath = tb_fileTelnet.Text;

            var batchManager = new UpgradeManagerBatch(tb_upgradeLog_AppendText);

            // 调用批量升级方法，并传入一个匿名函数来更新UI日志
            await batchManager.StartBatchUpgradeAsync(ipTails, filePath);

            // 全部完成后恢复按钮
            bt_telnet.Enabled = true;
            //MessageBox.Show("批量升级流程已结束！");
        }
        private async void bt_hpm_Click(object sender, EventArgs e)
        {
            // // 解析用户输入的多个IP尾数
            string[] ipTails = GetRange.GetIPRange(cb_upgradeIP.Text.Trim()).ToArray();
            if (ipTails.Length == 0)
            {
                Console.WriteLine("指定 的IP 地址，格式错误");
                return;
            }
            // 禁用按钮防止重复点击
            bt_hpm.Enabled = false;
            string filePath = tb_fileHpm.Text;

            var batchManager = new UpgradeManagerBatch(tb_upgradeLog_AppendText);

            // 调用批量升级方法，并传入一个匿名函数来更新UI日志
            await batchManager.StartBatchUpgradeAsync(ipTails, filePath);

            // 全部完成后恢复按钮
            bt_hpm.Enabled = true;
            //MessageBox.Show("批量升级流程已结束！");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tb_upgradeLog_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
