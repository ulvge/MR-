using BmcUpgradeTool;
using Debug.tools;
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
using System.Threading.Tasks;
using System.Windows.Forms;
using static Debug.MRForm;

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

        private void tb_fileHpm_DoubleClick(object sender, EventArgs e)
        {//弹出打开 hpm升级包 对话框
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "hpm升级包|*.hpm";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                tb_fileHpm.Text = fileDialog.FileName;
            }
        }
        public void tb_upgradeLog_AppendText(string message)
        {
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

            var batchManager = new BatchUpgradeManager(tb_upgradeLog_AppendText);

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

            var batchManager = new BatchUpgradeManager(tb_upgradeLog_AppendText);

            // 调用批量升级方法，并传入一个匿名函数来更新UI日志
            await batchManager.StartBatchUpgradeAsync(ipTails, filePath);

            // 全部完成后恢复按钮
            bt_hpm.Enabled = true;
            //MessageBox.Show("批量升级流程已结束！");

        }

    }
}
