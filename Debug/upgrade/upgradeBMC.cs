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
            // 先设置全局样式（指定具体颜色，而不是依赖默认值）
            dg_upgradeProcessBar.DefaultCellStyle.BackColor = Color.White;
            dg_upgradeProcessBar.DefaultCellStyle.SelectionBackColor = Color.White;  // 选中时也是白色
            dg_upgradeProcessBar.DefaultCellStyle.ForeColor = Color.Black;
            dg_upgradeProcessBar.DefaultCellStyle.SelectionForeColor = Color.Black;
            
            // 清除行级别的默认样式
            dg_upgradeProcessBar.RowsDefaultCellStyle.BackColor = Color.Empty;
            dg_upgradeProcessBar.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
            dg_upgradeProcessBar.RowTemplate.DefaultCellStyle.BackColor = Color.Empty;
            
            // 绑定 CellFormatting 事件
            dg_upgradeProcessBar.CellFormatting += Dg_upgradeProcessBar_CellFormatting;
            
            // 初始化进度管理器
            BMCProgressManager progressManager = new BMCProgressManager(dg_upgradeProcessBar);
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

        private void Dg_upgradeProcessBar_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 只处理进度列（索引 1）
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                string value = e.Value?.ToString();
                if (!string.IsNullOrEmpty(value) && value.Contains("%"))
                {
                    string percentStr = value.TrimEnd('%');
                    if (int.TryParse(percentStr, out int percent))
                    {
                        if (percent == 100)
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.SelectionBackColor = Color.LightGreen;
                        }
                        else
                        {
                            e.CellStyle.BackColor = Color.LightPink;
                            e.CellStyle.SelectionBackColor = Color.LightPink;
                        }
                    }
                }
            }
        }

        // 简化 UpdateDataGridView，只设置值，不设置颜色
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

            DataGridViewRow row;
            if (!ipRowMap.ContainsKey(ip))
            {
                row = new DataGridViewRow();
                row.CreateCells(dg_upgradeProcessBar);
                row.Cells[0].Value = ip;
                dg_upgradeProcessBar.Rows.Add(row);
                ipRowMap[ip] = row;
            }
            else
            {
                row = ipRowMap[ip];
            }

            // 只设置值，不设置颜色（颜色由 CellFormatting 自动处理）
            row.Cells[1].Value = $"{percent}%";
            row.Cells[2].Value = percent == 100 ? "完成" : stage;
            row.Cells[3].Value = DateTime.Now.ToString("HH:mm:ss");
            
            // 强制设置样式（多种方式）
            if (percent == 100)
            {
                // 方式1：直接设置
                row.Cells[1].Style.BackColor = Color.LightGreen;
                row.Cells[1].Style.ForeColor = Color.DarkGreen;
                row.Cells[1].Style.SelectionBackColor = Color.LightGreen;
                row.Cells[1].Style.SelectionForeColor = Color.DarkGreen;
                
                // 方式2：通过 DefaultCellStyle
                row.DefaultCellStyle.BackColor = Color.LightGreen;
                
                // 方式3：通过单元格的 Style 应用
                var style = new DataGridViewCellStyle();
                style.BackColor = Color.LightGreen;
                style.ForeColor = Color.DarkGreen;
                row.Cells[1].Style = style;
                
                // 方式4：强制刷新
                dg_upgradeProcessBar.InvalidateCell(1, row.Index);
            }
            else
            {
                row.Cells[1].Style.BackColor = Color.LightPink;
                row.Cells[1].Style.ForeColor = Color.DarkRed;
                row.Cells[1].Style.SelectionBackColor = Color.LightPink;
                row.Cells[1].Style.SelectionForeColor = Color.DarkRed;
            }
            
            // 确保行不被选中
            row.Selected = false;
            
            // 强制刷新
            dg_upgradeProcessBar.Refresh();
            Application.DoEvents();
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
        private bool _isTelnetRunning = false;  // 状态标志
        private async void bt_telnet_Click(object sender, EventArgs e)
        {
            if (_isTelnetRunning)
            {
                Console.WriteLine("操作正在进行中，请勿重复点击");
                return;
            }

            // 设置标志
            _isTelnetRunning = true;
            try
            {
                dg_upgradeProcessBar.Rows.Clear();
                // // 解析用户输入的多个IP尾数
                string[] ipTails = GetRange.GetIPRange(cb_upgradeIP.Text.Trim()).ToArray();
                if (ipTails.Length == 0)
                {
                    Console.WriteLine("指定 的IP 地址，格式错误");
                    return;
                }
                string filePath = tb_fileTelnet.Text;

                var batchManager = new UpgradeManagerBatch(tb_upgradeLog_AppendText);

                // 调用批量升级方法，并传入一个匿名函数来更新UI日志
                await batchManager.StartBatchUpgradeAsync(ipTails, filePath);
            }
            finally
            {
                // 确保标志一定会被清除
                _isTelnetRunning = false;
            }
        }
        
        private bool _isGradeHpmRunning = false;  // 状态标志
        private async void bt_hpm_Click(object sender, EventArgs e)
        {
            if (_isGradeHpmRunning)
            {
                Console.WriteLine("操作正在进行中，请勿重复点击");
                return;
            }
            // 设置标志
            _isGradeHpmRunning = true;
            try
            {
                dg_upgradeProcessBar.Rows.Clear();
                // // 解析用户输入的多个IP尾数
                string[] ipTails = GetRange.GetIPRange(cb_upgradeIP.Text.Trim()).ToArray();
                if (ipTails.Length == 0)
                {
                    Console.WriteLine("指定 的IP 地址，格式错误");
                    return;
                }
                string filePath = tb_fileHpm.Text;

                var batchManager = new UpgradeManagerBatch(tb_upgradeLog_AppendText);

                // 调用批量升级方法，并传入一个匿名函数来更新UI日志
                await batchManager.StartBatchUpgradeAsync(ipTails, filePath);
            }
            finally
            {
                // 确保标志一定会被清除
                _isGradeHpmRunning = false;
            }   
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tb_upgradeLog_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
