using Debug.IPMB;
using Debug.tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Debug
{
    public partial class PortPinCovert : Form, InterfaceINI {
        public PortPinCovert()
        {
            InitializeComponent();
        }

        private void hwPortPin_TextChanged(object sender, EventArgs e)
        {
            string raw = this.hwPortPin.Text;
            if(raw.Length != 2) {
                return;
            }
            char port = raw[0];
            char pin = raw[1];
            if(!char.IsLetter(port) || !char.IsDigit(pin)) {
                return;
            }
            if(pin > '7') {
                MessageBox.Show("The num of pin  should't more than 7");
                Console.WriteLine();
                return;
            }
            char portUpper = char.ToUpper(port);
            int softPortPin = (portUpper - 'A') * 8 + (pin-'0');

            this.softPortPin.Text = softPortPin.ToString();
        }

        private void softPortPin_TextChanged(object sender, EventArgs e)
        {
            string raw = this.softPortPin.Text;
            try {
                int idex = int.Parse(raw);
                int port = idex / 8;
                int pin = idex % 8;

                if(port > 26) {
                    MessageBox.Show("The num of port  is too large ,not support!!");
                    return;
                }
                char portChar = (char)('A' + port);

                string portStr = portChar.ToString();
                string res = portStr + pin.ToString();

                this.hwPortPin.Text = res;

            } catch(Exception) {
                return;
            }

        }

        private void PortPinCovert_Load(object sender,EventArgs e) {
            string[] lists = NetFun.NetFunc_getAllName();
            this.cb_netFun.Items.AddRange(lists);
            loadINI();
        }

        private void PortPinCovert_FormClosing(object sender,FormClosingEventArgs e) {
            //e.Cancel = true;                  //不执行操作
            updateINI();
        }

        public void loadINI() {
            IniHelper iniHelper = new IniHelper();
            iniHelper.IniLoader2Form(this);
        }

        public void updateINI() {
            IniHelper iniHelper = new IniHelper();
            iniHelper.IniUpdate2File(this);
        }

        private void tb_chkSumInput_TextChanged(object sender, EventArgs e) {
            string inputStr = tb_chkSumInput.Text.ToString();
            tb_chkSumOutput.Text = ChkSum(inputStr);
            Clipboard.SetText(tb_chkSumOutput.Text);
        }

        private string ChkSum(string str) {
            try {
                byte sum = 0;
                string[] inputArrary = str.Split(' ');
                foreach(var hex in inputArrary) {
                    if(string.IsNullOrWhiteSpace(hex)) {
                        continue;
                    }
                    if(hex.Length > 2) {
                        return "parse err";
                    }
                    byte valDec = (byte)System.Convert.ToInt32(hex, 16);
                    sum += valDec;
                }
                sum = (byte)(256 - sum);
                return sum.ToString("X");
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }

        private void cb_netFun_SelectedIndexChanged(object sender, EventArgs e) {
            string[] subCmdName = NetFun.SumCmd_getAllName(cb_netFun.Text);
            if (subCmdName != null) {
                cb_cmd.Items.Clear();
                cb_cmd.Items.AddRange(subCmdName);
                if (cb_cmd.SelectedIndex == -1) { 
                    cb_cmd.SelectedIndex = 0;
                }
            }
            UpdateCmd();
        }

        private static string FRAME_HEAD = "A0";
        private static string FRAME_TAIL = "A5";
        private void UpdateCmd() {
            string cmd = CreateCmd();
            tb_IPMB.Text = cmd;
            Clipboard.SetText(tb_IPMB.Text);
        }

        private string checkFormat(string arrary) {
            string[] cmdBytes = arrary.Split(' ');
            StringBuilder cmds = new StringBuilder();
            foreach(string str in cmdBytes) {
                if(string.IsNullOrEmpty(str)) {
                    continue;
                }
                if(str.Length == 1) {
                    cmds.Append("0" + str + " ");
                } else if(str.ToLower().StartsWith("0x")) {
                    if(str.Length > 2) {
                        cmds.Append(str.Substring(2) + " ");
                    }
                } else {
                    cmds.Append(str + " ");
                }
            }
            return cmds.ToString();
        }
        private string CreateCmd() {
            string formatStr1, formatStr2;
            StringBuilder head = new StringBuilder();
            head.Append(tb_target.Text+" ");
            string netFunVal = NetFun.NetFunc_convertVal(cb_netFun.Text);
            head.Append(IPMBHelper.Radix(netFunVal, 2, tb_LUN.Text) + " ");
            formatStr1 = checkFormat(head.ToString());
            head.Clear();
            head.Append(formatStr1);
            head.Append(ChkSum(formatStr1) + " ");

            StringBuilder body = new StringBuilder();
            body.Append(tb_selfAddr.Text + " ");
            body.Append(IPMBHelper.Radix(tb_serialNumber.Text, 2, tb_LUN.Text) + " ");

            string subCmdVal = NetFun.SumCmd_convertVal(cb_netFun.Text, cb_cmd.Text);
            body.Append(subCmdVal + " ");
            //body.Append(tb_cmd.Text + " ");
            if (!string.IsNullOrEmpty(tb_data.Text)) { 
                body.Append(tb_data.Text + " ");
            }
            formatStr2 = checkFormat(body.ToString());
            body.Clear();
            body.Append(formatStr2);
            body.Append(ChkSum(formatStr2) + " ");

            string cmd = head.ToString() + body.ToString();

            if(cb_frameTag.Checked) {
                return FRAME_HEAD + " " + cmd + FRAME_TAIL + " ";
            }
            return cmd;
        }

        private void ipmb_TextChanged(object sender, EventArgs e) {
            UpdateCmd();
        }

        private void cb_cmd_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateCmd();
            string desc = NetFun.SumCmd_getDesc(cb_netFun.Text, cb_cmd.Text);
            tb_data.Text = string.Empty;
            toolTip1.SetToolTip(tb_data, desc);
        }
    }
}