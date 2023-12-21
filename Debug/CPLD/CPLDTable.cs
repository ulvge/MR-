using Debug.misc;
using Debug.tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Debug
{
    public partial class CPLDTable : Form, InterfaceINI {
		private static BankClass[] g_BankClass;

		public CPLDTable()
        {
            InitializeComponent();
		}

		private void PortPinCovert_Load(object sender, EventArgs e) {
			loadINI();
			AddUsage();
		}

        private void AddUsage()
        { 
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("1、可支持根据CPLD简述生成adc" + Environment.NewLine);
			stringBuilder.Append("\t格式：名称 pin" + Environment.NewLine);
			stringBuilder.Append("\t示例：" + Environment.NewLine);
			stringBuilder.Append("\tCPLD_DEBUG1	P8" + Environment.NewLine);
			stringBuilder.Append("\tCPLD_DEBUG2 T8" + Environment.NewLine);

			stringBuilder.Append("2、可支持根据CPLD简述定义变量" + Environment.NewLine);
			stringBuilder.Append("\t格式：名称 in/out 注释 " + Environment.NewLine);
			stringBuilder.Append("\t示例：" + Environment.NewLine);
			stringBuilder.Append("\tCPLD_DEBUG1	IN for CPLD debug" + Environment.NewLine);
			stringBuilder.Append("\tCPU_CRU_RST_OK OUT for CPU复位完成信号" + Environment.NewLine);

			tb_input.Text = stringBuilder.ToString();
		}

        private BankClass[] GetDeviceConfig()
        {
			if (cb_devs.Text.Contains("EF3"))
            {
				return EF3L.GetConfig();
			}
			else if(cb_devs.Text.Contains("EF2L45UG132B"))
			{
				return EF2L45UG132B.GetConfig();
            }
			else if (cb_devs.Text.Contains("EF2L45BG256B"))
			{
				return EF2L45BG256B.GetConfig();
			}
			return null;
		}
		private class CreateADC {
			public string humanName { get; set; }// eg: nr cr
			public string pin { get; set; } // eg: -10, 20
			public string IOSTANDARD { get; set; }
			public CreateADC(string humanName, string pin) {
				this.humanName = humanName.Replace(".", "_");
				this.pin = pin;
				this.IOSTANDARD = GetBankVolLevel(pin);
			}

			private string GetBankVolLevel(string pin) {
				if(pin.Equals("T6")) {
					//Console.WriteLine(pin);
                }
				string row = pin.Substring(0,1);
				int column = int.Parse(pin.Substring(1));

                foreach(var bank in g_BankClass) {
					if (bank.bankName.Equals("bank1") && pin.Equals("R17"))
					{
						//Console.WriteLine("bank.bankName :" + bank.bankName);
					}
					LVCMOS[] range = bank.range;
                    foreach(var item in range) {
						if(item.rowName == row) {
							if((column >= item.min) && (column <= item.max)) {
								return bank.bankVolotLevel;
							}
						}
					}
                }
				return "LVCMOSERROR";
			}
		}

		private string[] StringCommit = {
				"JTAG_",
				"PROGRAMN",
			};
		private bool IsPinNameNeedCommit(string pinName) {
			if(string.IsNullOrEmpty(pinName)) {
				return true;
			}
			foreach(var item in StringCommit) {
				if(pinName.Contains(item)) {
					return true;
				}
			}
			return false;
		}

		private class DefineVariable
		{
			public string humanName { get; set; }
			public string inout { get; set; }
			public string comm { get; set; }

			public DefineVariable(string[] str)
			{
				if (str.Length >= 2)
                {
					humanName = str[0];
					inout = str[1].ToLower() + "put";
				}
                for (int i = 2; i < str.Length; i++)
                {
					comm += str[i] + " ";
				}
				if (!inout.Equals("input") && !inout.Equals("output"))
                {
					inout += "error__";
				}
				if (comm.Contains(humanName))
                {
					comm = comm.Replace(humanName, "");
				}
				comm = comm.Trim();
			}
		}

		private void OutputDefineVariable(List<DefineVariable> defineVariable)
		{
			StringBuilder sb = new StringBuilder();
			foreach (var item in defineVariable)
			{
				string assembly = "    " + item.inout + " " + item.humanName + "; // " + item.comm + "\r\n";
				if (IsPinNameNeedCommit(item.humanName))
				{
					sb.Insert(0, "#" + assembly);
				}
				else
				{
					sb.Append(assembly);
				}
			}
			tb_res.Text = sb.ToString();
		}
		private void OutputADC(List<CreateADC> destDesc)
        {
			//4 format output
			StringBuilder sb = new StringBuilder();
			foreach (var item in destDesc)
			{
				//set_pin_assignment { CPU_CKOBV_SEL0 } {  LOCATION = T6   ;IOSTANDARD = LVCMOS18  ;PULLTYPE = NONE; }
				string assembly = ("set_pin_assignment { " + item.humanName + " } { LOCATION = " + item.pin + "; IOSTANDARD = " + item.IOSTANDARD + "; PULLTYPE = NONE; }\r\n");
				if (IsPinNameNeedCommit(item.humanName))
				{
					sb.Insert(0, "#" + assembly);
				}
				else
				{
					sb.Append(assembly);
				}
			}
			tb_res.Text = sb.ToString();
		}
		//1 read ori table
		//2 parsing split ori table
		//3 add column
		//4 format output
		private void AutoCreateAllItems() {
			g_BankClass = GetDeviceConfig();
			if (g_BankClass == null)
            {
				return;
			}
			List<DefineVariable> defineVariable = new List<DefineVariable>();
			List<CreateADC> destDesc = new List<CreateADC>();
			// 1 read ori table
			string[] oriItems = tb_input.Text.Split('\n');
			//2 parsing split ori table
			foreach (var item in oriItems) {
				string removeSpace = item;
				int lastLen;
				while(true) {
					lastLen = removeSpace.Length;
					removeSpace = removeSpace.Replace("  ", " ");
					removeSpace = removeSpace.Replace("\t", " ");
					removeSpace = removeSpace.Replace("#", "");
					removeSpace = removeSpace.Replace("<", "");
					removeSpace = removeSpace.Replace(">", "");
					if (lastLen == removeSpace.Length) {
						break;
					}
				}
				string[] lines = removeSpace.Split(' ');
				if (lines.Length == 1)
                {
					continue;
                }
				if (lines.Length == 2)
				{
					destDesc.Add(new CreateADC(lines[0], lines[1]));
                }

				defineVariable.Add(new DefineVariable(lines));
			}
			if (defineVariable.Count > destDesc.Count) {
				OutputDefineVariable(defineVariable);
            }
            else
            {
				OutputADC(destDesc);
			}
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

        private void bt_createAgain_Click(object sender, EventArgs e)
		{
			//DataCheck.DataCheckMain();
			//SelectPdf.Handler();
			//return;
			AutoCreateAllItems();
		}
    }
}