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
            try {
                byte sum = 0;
                string inputStr = tb_chkSumInput.Text.ToString();
                string[] inputArrary = inputStr.Split(' ');
                foreach(var hex in inputArrary) {
                    if(string.IsNullOrWhiteSpace(hex)) {
                        continue;
                    }
                    if(hex.Length > 2) {
                        tb_chkSumOutput.Text = "parse err";
                        return;
                    }
                    byte valDec = (byte)System.Convert.ToInt32(hex, 16);
                    sum += valDec;
                }
                sum = (byte)(256 - sum);
                tb_chkSumOutput.Text = sum.ToString("X");
                Clipboard.SetText(tb_chkSumOutput.Text);
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}