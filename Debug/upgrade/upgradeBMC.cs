using BmcUpgradeTool;
using Debug.IPMITool;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Debug.MRForm;
using static System.Windows.Forms.AxHost;

namespace Debug {

    public partial class UpgradeBMC : Form, InterfaceINI{
        
        private string g_queryBMCFirmwareKey = "BMC版本";
        private string g_queryBIOSFirmwareKey = "BIOS版本";

        public UpgradeBMC() {
            InitializeComponent();
        }
        
        public enum DeviceType
        {
            BMC = 0,
            BIOS = 1,
            CPLD = 2
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
        public void loadINI()
        {
            IniHelper iniHelper = new IniHelper();
            iniHelper.IniLoader2Form(this);
        }

        public void updateINI()
        {
            IniHelper iniHelper = new IniHelper();
            iniHelper.IniUpdate2File(this);
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
            // 添加列头点击事件
            dg_upgradeProcessBar.ColumnHeaderMouseClick += DataGridView1_ColumnHeaderMouseClick;

            cb_ipmiCmd.Items.Add(g_queryBMCFirmwareKey);
            cb_ipmiCmd.Items.Add(g_queryBIOSFirmwareKey);
        }
        private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 只对 IP 列进行排序
            if (dg_upgradeProcessBar.Columns[e.ColumnIndex].Name == "IP")
            {
                SortDataGridViewByIP();
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

            // 获取当前列的排序方向（假设 IP 列是第 0 列）
            SortOrder currentSortOrder = dg_upgradeProcessBar.Columns[0].HeaderCell.SortGlyphDirection;

            // 如果没有排序方向，默认升序
            if (currentSortOrder == SortOrder.None)
                currentSortOrder = SortOrder.Ascending;

            IOrderedEnumerable<DataGridViewRow> sortedRows;
            if (currentSortOrder == SortOrder.Ascending)
            {
                sortedRows = rowsData.OrderBy(r => IPToLong(r.Cells[0].Value?.ToString()));
            }
            else
            {
                sortedRows = rowsData.OrderByDescending(r => IPToLong(r.Cells[0].Value?.ToString()));
            }

            // 重新填充
            dg_upgradeProcessBar.Rows.Clear();
            ipRowMap.Clear();

            foreach (var row in sortedRows.ToList())
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
            string percent = progressInfo.percent;
            string msg = progressInfo.msg;

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
            row.Cells[2].Value = DateTime.Now.ToString("HH:mm:ss");
            row.Cells[3].Value = msg; 
            
            if (percent == "100")
            {
                row.Cells[1].Style.BackColor = Color.LightGreen;
                row.Cells[1].Style.ForeColor = Color.DarkGreen;
                row.Cells[1].Style.SelectionBackColor = Color.LightGreen;
                row.Cells[1].Style.SelectionForeColor = Color.DarkGreen;
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

        public void tb_upgradeLog_AppendText(object message)
        {
            if (tb_upgradeLog.InvokeRequired)
            {
                tb_upgradeLog.Invoke(new Action<object>(tb_upgradeLog_AppendText), message);
                return;
            }
            switch (message)
            {
                case string str:
                    tb_upgradeLog.AppendText($"{DateTime.Now:HH:mm:ss} {str}");
                    break;
                case BMCProgressInfo progressInfo:
                    UpdateDataGridView(progressInfo);
                    break;
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
                ipRowMap.Clear();
                // // 解析用户输入的多个IP尾数
                string[] ipTails = GetRange.GetIPRange(cb_upgradeIP.Text.Trim()).ToArray();
                if (ipTails.Length == 0)
                {
                    Console.WriteLine("指定 的IP 地址，格式错误");
                    return;
                }
                string filePath = tb_fileTelnet.Text;

                var batchManager = new RedfishManager(tb_upgradeLog_AppendText);

                // 调用批量升级方法，并传入一个匿名函数来更新UI日志
                await batchManager.UpgradeBatchAsync(ipTails, filePath);
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
                ipRowMap.Clear();
                // // 解析用户输入的多个IP尾数
                string[] ipTails = GetRange.GetIPRange(cb_upgradeIP.Text.Trim()).ToArray();
                if (ipTails.Length == 0)
                {
                    Console.WriteLine("指定 的IP 地址，格式错误");
                    return;
                }
                string filePath = tb_fileHpm.Text;

                var batchManager = new RedfishManager(tb_upgradeLog_AppendText);

                // 调用批量升级方法，并传入一个匿名函数来更新UI日志
                await batchManager.UpgradeBatchAsync(ipTails, filePath);
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

        private void bt_ipmiCmd_Click(object sender, EventArgs e)
        {
            dg_upgradeProcessBar.Rows.Clear();
            ipRowMap.Clear();
            string[] ipTails = GetRange.GetIPRange(cb_upgradeIP.Text.Trim()).ToArray();
            if (ipTails.Length == 0)
            {
                tb_upgradeLog_AppendText("指定 的IP 地址，格式错误");
                return;
            }
            if (cb_ipmiCmd.Text == g_queryBMCFirmwareKey)
            {
                var batchManager = new RedfishManager(tb_upgradeLog_AppendText);
                _ = batchManager.GetFirmwaretBatchAsync(ipTails, DeviceType.BMC);
            }else if (cb_ipmiCmd.Text == g_queryBIOSFirmwareKey)
            {
                var batchManager = new RedfishManager(tb_upgradeLog_AppendText);
                _ = batchManager.GetFirmwaretBatchAsync(ipTails, DeviceType.BIOS);
            }
            else
            {
                IpmiResultParse ipmiResultParse = new IpmiResultParse(tb_upgradeLog_AppendText, tb_loginUserName.Text, tb_loginPwd.Text);

                string cmd = cb_ipmiCmd.Text; //  "power status";
                _ = ipmiResultParse.SendIPMICmdBatchAsync(ipTails, cmd);
            }

        }
        private bool hook_ConvertUTCTime(string unixTimestamp)
        {
            try
            {
                long unixTimestampLong = long.Parse(unixTimestamp);
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimestampLong);
                DateTime localTime = dateTimeOffset.LocalDateTime;

                tb_upgradeLog_AppendText($"当地时间: {localTime:yyyy-MM-dd HH:mm:ss}\r\n");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private async void tb_ipmiCmdList_Click(object sender, EventArgs e)
        {
            int cmdCount = 0;
            dg_upgradeProcessBar.Rows.Clear();
            ipRowMap.Clear();
            string[] ipTails = GetRange.GetIPRange(cb_upgradeIP.Text.Trim()).ToArray();
            if (ipTails.Length == 0)
            {
                tb_upgradeLog_AppendText("指定 的IP 地址，格式错误");
                return;
            }

            string cmdListStr = tb_ipmiCmds.Text;
            string[] cmdList = cmdListStr.Split('\n');
            foreach (var cmd in cmdList)
            {
                if (cmd.Trim().Length == 0)
                {
                    continue;
                }
                if (hook_ConvertUTCTime(cmd.Trim()))
                {
                    continue;
                }
                tb_upgradeLog_AppendText($"***************************************************************    cmdCount = {cmdCount++}\r\n");
                Thread.Sleep(500);
                IpmiResultParse ipmiResultParse = new IpmiResultParse(tb_upgradeLog_AppendText, tb_loginUserName.Text, tb_loginPwd.Text);
                string cmdStr = cmd.Trim();
                await ipmiResultParse.SendIPMICmdBatchAsync(ipTails, cmdStr);
            }
        }
    }
}
