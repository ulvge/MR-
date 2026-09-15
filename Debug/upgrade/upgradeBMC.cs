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
using static Debug.upgrade.FruParser;
using static System.Windows.Forms.AxHost;

namespace Debug {

    public partial class UpgradeBMC : Form, InterfaceINI{
        
        private string g_queryBMCFirmwareKey = "BMC版本";
        private string g_queryBIOSFirmwareKey = "BIOS版本";
        private string g_powerReset = "power reset";

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
            bru_init();
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

            // 绑定拖拽进入事件
            this.tb_fileHpm.DragEnter += tb_fileHpm_DragEnter;
            this.tb_fileTelnet.DragEnter += tb_fileHpm_DragEnter;
            // 绑定文件放下事件
            this.tb_fileHpm.DragDrop += tb_fileHpm_DragDrop;
            this.tb_fileTelnet.DragDrop += tb_fileHpm_DragDrop;
            _isFormLoaded = true;
        }
        private void tb_fileHpm_DragEnter(object sender, DragEventArgs e)
        {
            // 检查拖拽的数据中是否包含文件
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; // 设置光标为“复制”图标（带个加号）
            }
            else
            {
                e.Effect = DragDropEffects.None; // 如果不是文件，显示禁止图标
            }
        }
        private void tb_fileHpm_DragDrop(object sender, DragEventArgs e)
        {// 安全地获取文件路径（返回的是字符串数组，因为可能拖入多个文件）
            if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
            {
                // 获取第一个文件的路径，并显示在 TextBox 中
                string filePath = files[0];
                TextBox targetBox = (TextBox)sender;
                targetBox.Text = filePath;

                // 将光标移到文本末尾，方便查看长路径
                targetBox.SelectionStart = targetBox.Text.Length;
            }
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
        // 删除以前的临时文件
        private void DeleteTempFiles(string downloadTempPath)
        {
            // 1. 安全检查：确保目录存在
            if (!Directory.Exists(downloadTempPath))
            {
                Console.WriteLine("目录不存在，无需清理。");
                return;
            }

            // 2. 计算时间阈值：1个月前的时间点
            DateTime cutoffDate = DateTime.Now.AddMonths(-1);

            // 3. 获取当前目录下的所有文件（默认不递归子目录）
            string[] files = Directory.GetFiles(downloadTempPath);

            foreach (string file in files)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(file);

                    // 4. 判断文件的最后写入时间是否早于1个月前
                    if (fileInfo.LastWriteTime < cutoffDate)
                    {
                        // 5. 处理只读文件：如果文件被设为只读，直接删除会报错
                        if (fileInfo.IsReadOnly)
                        {
                            fileInfo.IsReadOnly = false;
                        }

                        // 6. 执行删除
                        fileInfo.Delete();
                        Console.WriteLine($"已删除过期文件: {file}");
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // 捕获权限不足异常，避免因为一个文件删不掉导致整个程序崩溃
                    Console.WriteLine($"权限不足，跳过文件: {file}");
                }
                catch (Exception ex)
                {
                    // 捕获其他异常（如文件正被其他进程占用）
                    Console.WriteLine($"删除文件失败: {file}，原因: {ex.Message}");
                }
            }

            Console.WriteLine("清理完成！");
        }
        // 文件大小 <= 50M，且没有以 “signed.hpm” 结尾，就上传签名
        private static int HPM_MAX_FILE_SIZE_MB = 50;
        private static string HPM_SIGNED_SUFFIX = "signed.hpm";
        private async Task<(bool isSingedSuccess, string singedFileFullName)> TrySignHpm(string filePath)
        {
            string singedFilePath = filePath;
            try
            {
                // 是否需要签名hpm文件
                if (filePath.EndsWith("signed.hpm") == true)
                {
                    return (true, singedFilePath);
                }
                if (new FileInfo(filePath).Length > HPM_MAX_FILE_SIZE_MB * 1024 * 1024)
                {
                    tb_upgradeLog_AppendText($"❌ 文件大小超过 {HPM_MAX_FILE_SIZE_MB}MB，是BMC固件，不需要签名\r\n");
                    return (true, singedFilePath);
                }
                tb_upgradeLog_AppendText("❌ 该hpm文件未签名，尝试上传签名后再升级\r\n");
                SigneHPM signeHPM = new SigneHPM(tb_upgradeLog_AppendText);
                bool isSignSuccess = await signeHPM.UpdateAndSign(filePath);
                if (!isSignSuccess)
                {
                    tb_upgradeLog_AppendText("❌ 签名失败\r\n");
                    return (false, singedFilePath);
                }
                tb_upgradeLog_AppendText("✅ 签名成功\r\n");
                
                string downloadTempPath = $"{Environment.CurrentDirectory}\\downloads";
                DeleteTempFiles(downloadTempPath);
                bool isDownloadSuccess = await signeHPM.DownloadSignedHpm(downloadTempPath);
                if (isDownloadSuccess)
                {
                    singedFilePath = $"{downloadTempPath}\\{signeHPM.signedFileName}";
                }
                return (isDownloadSuccess, singedFilePath);
            }
            finally
            {
                
            }
            return (false, singedFilePath);
        }
        private async Task powerReset(string[] ipTails, int delaySec = 10)
        {
            IpmiResultParse ipmiResultParse = new IpmiResultParse(tb_upgradeLog_AppendText, tb_loginUserName.Text, tb_loginPwd.Text);

            _ = ipmiResultParse.SendIPMICmdBatchAsync(ipTails, "power off");
            await Task.Delay(delaySec * 1000);
            _ = ipmiResultParse.SendIPMICmdBatchAsync(ipTails, "power on");
        }
        private bool _isUpgradeHpmRunning = false;  // 状态标志
        private bool _isFormLoaded;

        private DateTime _lastHPMClickTime = DateTime.Now;
        private async void bt_hpm_Click(object sender, EventArgs e)
        {
            string filePath = tb_fileHpm.Text;
            if (!File.Exists(filePath))
            {
                tb_upgradeLog_AppendText("❌ 文件不存在或路径无效\r\n");
                return;
            }
            if (filePath.EndsWith(".hpm") == false)
            {
                tb_upgradeLog_AppendText("❌ 文件扩展名错误，仅允许 .hpm 文件\r\n");
                return;
            }
            var now = DateTime.Now;
            if(((now - _lastHPMClickTime).TotalMilliseconds > 5000) && (_isUpgradeHpmRunning)) {
                // 两次点击间隔小于1秒，做特别的事情
                _isUpgradeHpmRunning = false;
                tb_upgradeLog_AppendText("强行取消上一次的操作\r\n");
            }
            if (_isUpgradeHpmRunning)
            {
                tb_upgradeLog_AppendText("❌ 上一次操作正在进行中，等待结束\r\n");
                return;
            }
            _lastHPMClickTime = DateTime.Now;
                // 设置标志
            _isUpgradeHpmRunning = true;
            try
            {
                // 是否需要签名hpm文件
                var (isSingedSuccess, singedFileFullName) = await TrySignHpm(filePath);
                if (isSingedSuccess == false)
                {
                    return;
                }

                dg_upgradeProcessBar.Rows.Clear();
                ipRowMap.Clear();
                // // 解析用户输入的多个IP尾数
                string[] ipTails = GetRange.GetIPRange(cb_upgradeIP.Text.Trim()).ToArray();
                if (ipTails.Length == 0)
                {
                    Console.WriteLine("指定 的IP 地址，格式错误");
                    return;
                }

                var batchManager = new RedfishManager(tb_upgradeLog_AppendText);

                // 调用批量升级方法，并传入一个匿名函数来更新UI日志
                await batchManager.UpgradeBatchAsync(ipTails, singedFileFullName);

                // 有签名，说明需要重启系统
                if (filePath != singedFileFullName) 
                {
                    await powerReset(ipTails, 55);
                }
            }
            finally
            {
                // 确保标志一定会被清除
                _isUpgradeHpmRunning = false;
            }   
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tb_upgradeLog_TextChanged(object sender, EventArgs e)
        {

        }

        private async void bt_ipmiCmd_Click(object sender, EventArgs e)
        {
            dg_upgradeProcessBar.Rows.Clear();
            ipRowMap.Clear();
            string[] ipTails = GetRange.GetIPRange(cb_upgradeIP.Text.Trim()).ToArray();
            if (ipTails.Length == 0)
            {
                tb_upgradeLog_AppendText("指定 的IP 地址，格式错误");
                return;
            }
            string cmdOnly = cb_ipmiCmd.Text.Split('|')[0];
            if (cmdOnly == g_queryBMCFirmwareKey)
            {
                var batchManager = new RedfishManager(tb_upgradeLog_AppendText);
                _ = batchManager.GetFirmwaretBatchAsync(ipTails, DeviceType.BMC);
            }else if (cmdOnly == g_queryBIOSFirmwareKey)
            {
                var batchManager = new RedfishManager(tb_upgradeLog_AppendText);
                _ = batchManager.GetFirmwaretBatchAsync(ipTails, DeviceType.BIOS);
            }
            else if (cmdOnly == g_powerReset)
            {
                await powerReset(ipTails);
            }
            else
            {
                IpmiResultParse ipmiResultParse = new IpmiResultParse(tb_upgradeLog_AppendText, tb_loginUserName.Text, tb_loginPwd.Text);
                _ = ipmiResultParse.SendIPMICmdBatchAsync(ipTails, cmdOnly);
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
        // ********* fru
        private void bru_init()
        {
            var dataSource = FruUpdate.FruTable.Select(kvp => new
            {
                Key = kvp.Key,           // 实际的英文 Key (如 "product_name")
                DisplayName = kvp.Key.PadRight(18) + "  " + kvp.Value.DisplayName // 显示的中文 (如 "产品名称")
            }).ToList();

            cb_fruCmd.DataSource = dataSource;
            cb_fruCmd.DisplayMember = "DisplayName"; // 绑定显示字段
            cb_fruCmd.ValueMember = "Key";           // 绑定实际值字段
        }
        private async void bt_fruUpdate_Click(object sender, EventArgs e)
        {
            dg_upgradeProcessBar.Rows.Clear();
            ipRowMap.Clear();
            string[] ipTails = GetRange.GetIPRange(cb_upgradeIP.Text.Trim()).ToArray();
            if (ipTails.Length == 0)
            {
                tb_upgradeLog_AppendText("指定 的IP 地址，格式错误\r\n");
                return;
            }
            try
            {
                // 1 更新
                List<string> cmdList = FruUpdate.GenerateCommands(cb_fruCmd.SelectedValue.ToString(), tb_fruContext.Text);
                IpmiResultParse ipmiResultParse = new IpmiResultParse(tb_upgradeLog_AppendText, tb_loginUserName.Text, tb_loginPwd.Text);
                foreach (var cmd in cmdList)
                {
                    if (cmd.Trim().Length == 0)
                    {
                        continue;
                    }
                    string cmdStr = cmd.Trim();
                    await ipmiResultParse.SendIPMICmdBatchAsync(ipTails, cmdStr);
                }
                Thread.Sleep(200);
                // 2. 重新读取查询
                string reReadFruCmd = "fru list 0";
                var (writeSuccess, output, error) = await ipmiResultParse.SendIPMICmdAsync(ipTails[0], reReadFruCmd);
                // 3. 一行代码完成解析
                if (writeSuccess)
                {
                    string selectedKey = cb_fruCmd.SelectedValue?.ToString();
                    var (readSuccess, readFruItemVaule) = await ReadFruItemValueFromIPMI(selectedKey);
                    if (readSuccess && (tb_fruContext.Text.Trim() == readFruItemVaule))
                    {
                        tb_upgradeLog_AppendText($"Fru写，校验成功, 新值{readFruItemVaule}\r\n");
                    }
                }
                else
                {
                    tb_upgradeLog_AppendText("fru写，失败\r\n");
                }

            }
            catch (Exception ex)
            {
                tb_upgradeLog_AppendText(ex.Message);
            }
            
        }
        private async Task<(bool Success, string actualValue)> ReadFruItemValueFromIPMI(string selectedKey)
        {
            IpmiResultParse ipmiResultParse = new IpmiResultParse(tb_upgradeLog_AppendText, tb_loginUserName.Text, tb_loginPwd.Text);
            string[] ipTails = GetRange.GetIPRange(cb_upgradeIP.Text.Trim()).ToArray();
            string reReadFruCmd = "fru list 0";
            string readFruItemVaule = string.Empty;
            var (success, output, error) = await ipmiResultParse.SendIPMICmdAsync(ipTails[0], reReadFruCmd);
            // 3. 一行代码完成解析
            if (success)
            {
                Dictionary<string, string> fru = FruParser.Parse(output);
                if (string.IsNullOrEmpty(selectedKey) || !fru.TryGetValue(selectedKey, out readFruItemVaule))
                {
                    // 成功获取到值，显示在文本框中
                    tb_upgradeLog_AppendText($"Fru读取，查询结果异常{selectedKey}\r\n");
                }
            }
            else
            {
                tb_upgradeLog_AppendText("Fru读取，查询失败\r\n");
            }

            return (success, readFruItemVaule);
        }

        private async void cb_fruCmd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isFormLoaded) return;
            string selectedKey = cb_fruCmd.SelectedValue?.ToString();
            var (readSuccess, readFruItemVaule) = await ReadFruItemValueFromIPMI(selectedKey);
            if (readSuccess)
            {
                tb_upgradeLog_AppendText($"Fru 读, {selectedKey} = {readFruItemVaule}\r\n");
            }
        }
    }
}
