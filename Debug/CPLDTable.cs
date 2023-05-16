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
        public CPLDTable()
        {
            InitializeComponent();
		}

		private void PortPinCovert_Load(object sender, EventArgs e) {
			loadINI();
			
			AutoCreateAllItems();
		}

		private class OriDesc {
			public string humanName { get; set; }// eg: nr cr
			public string pin { get; set; } // eg: -10, 20
			public string IOSTANDARD { get; set; }
			public OriDesc(string humanName, string pin) {
				this.humanName = humanName.Replace(".", "_");
				this.pin = pin;
				this.IOSTANDARD = GetBankVolLevel(pin);
			}
			private class LVCMOS {
				public string rowName;
				public int min;
				public int max;

				public LVCMOS(string rowName, int min, int max) {
					this.rowName = rowName;
					this.min = min;
					this.max = max;
				}
			}
			private class BankVolotLevel {
				public static string BankVolotLevel_1P8 = "LVCMOS18";
				public static string BankVolotLevel_3P3 = "LVCMOS33";
			}
			private class BankClass {
				public string bankName;
				public string bankVolotLevel;
				public LVCMOS[] range;
                    
                public BankClass(string bankName, string bankVolotLevel, LVCMOS[] range_Bank) {
                    this.bankName = bankName;
                    this.bankVolotLevel = bankVolotLevel;
                    this.range = range_Bank;
				}
				public static LVCMOS[] Range_Bank0 = {
					new LVCMOS("A", 3, 15),
					new LVCMOS("B", 3, 14),
					new LVCMOS("C", 4, 13),
					new LVCMOS("D", 5, 12),
					new LVCMOS("E", 6, 11),
					new LVCMOS("F", 7, 10),
				};
				public static LVCMOS[] Range_Bank1 = {
					new LVCMOS("B", 16, 16),
					new LVCMOS("C", 15, 16),
					new LVCMOS("D", 14, 16),
					new LVCMOS("E", 13, 16),
					new LVCMOS("F", 12, 16),

					new LVCMOS("G", 11, 16),
					new LVCMOS("H", 10, 16),
					new LVCMOS("J", 10, 16),
					new LVCMOS("K", 11, 16),

					new LVCMOS("L", 12, 16),
					new LVCMOS("M", 13, 13),
					new LVCMOS("M", 16, 16),
				};
				public static LVCMOS[] Range_Bank2 = {
					new LVCMOS("L", 7, 10),
					new LVCMOS("M", 6, 11),
					new LVCMOS("M", 14, 15),
					new LVCMOS("N", 6, 16),
					new LVCMOS("P", 4, 16),
					new LVCMOS("R", 3, 16),
					new LVCMOS("T", 2, 16),
				};
				public static LVCMOS[] Range_Bank3 = {
					new LVCMOS("K", 4, 5),
					new LVCMOS("L", 1, 5),
					new LVCMOS("M", 1, 4),
					new LVCMOS("N", 1, 3),
					new LVCMOS("P", 1, 2),
					new LVCMOS("R", 1, 1),
				};
				public static LVCMOS[] Range_Bank4 = {
					new LVCMOS("G", 1, 1),
					new LVCMOS("H", 1, 5),
					new LVCMOS("H", 7, 7),
					new LVCMOS("J", 1, 7),
					new LVCMOS("K", 1, 3),
					new LVCMOS("K", 6, 6),
				};
				public static LVCMOS[] Range_Bank5 = {
					new LVCMOS("B", 1, 1),
					new LVCMOS("C", 1, 2),
					new LVCMOS("D", 1, 3),
					new LVCMOS("E", 1, 4),
					new LVCMOS("F", 1, 5),
					new LVCMOS("G", 2, 6),
					new LVCMOS("H", 6, 6),
				};
			};
			private BankClass[] g_BankClass =  {
				new BankClass("bank0", BankVolotLevel.BankVolotLevel_3P3, BankClass.Range_Bank0),
				new BankClass("bank1", BankVolotLevel.BankVolotLevel_3P3, BankClass.Range_Bank1),
				new BankClass("bank2", BankVolotLevel.BankVolotLevel_3P3, BankClass.Range_Bank2),
				new BankClass("bank3", BankVolotLevel.BankVolotLevel_1P8, BankClass.Range_Bank3),
				new BankClass("bank4", BankVolotLevel.BankVolotLevel_1P8, BankClass.Range_Bank4),
				new BankClass("bank5", BankVolotLevel.BankVolotLevel_1P8, BankClass.Range_Bank5),
			};
			private string GetBankVolLevel(string pin) {
				if(pin.Equals("T6")) {
					Console.WriteLine(pin);
                }
				string row = pin.Substring(0,1);
				int column = int.Parse(pin.Substring(1));

                foreach(var bank in g_BankClass) {
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
		string oriFile = "../../res/ori.txt";
		string destFile = "../../res/dest.txt";
		//1 read ori table
		//2 parsing split ori table
		//3 add column
		//4 format output
		private void AutoCreateAllItems() {
			List<OriDesc> destDesc = new List<OriDesc>();	
			// 1 read ori table
			string[] oriItems = File.ReadAllLines(oriFile);
			//2 parsing split ori table
			foreach(var item in oriItems) {
				string removeSpace = item;
				int lastLen;
				while(true) {
					lastLen = removeSpace.Length;
					removeSpace = removeSpace.Replace("  ", " ");
					removeSpace = removeSpace.Replace("\t", " ");
					removeSpace = removeSpace.Replace("#", "");
					if (lastLen == removeSpace.Length) {
						break;
					}
				}
				string[] lines = removeSpace.Split(' ');
				if (lines.Length != 2) {
					continue;
                }

				//3 add column
				destDesc.Add(new OriDesc(lines[0], lines[1]));
			}

			//4 format output
			StringBuilder sb = new StringBuilder();
            foreach(var item in destDesc) {
				//set_pin_assignment { CPU_CKOBV_SEL0 } {  LOCATION = T6   ;IOSTANDARD = LVCMOS18  ;PULLTYPE = NONE; }
				string assembly = ("set_pin_assignment { " + item.humanName + " } { LOCATION = " + item.pin + "; IOSTANDARD = " + item.IOSTANDARD + "; PULLTYPE = NONE; }\r\n");
				if(IsPinNameNeedCommit(item.humanName)) {
					sb.Insert(0, "#" + assembly);
                } else {
					sb.Append(assembly);
				}
			}
			File.WriteAllText(destFile, sb.ToString());
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
    }
}