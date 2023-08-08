using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug
{
    class EF3L
    {
		public static LVCMOS[] Range_Bank0 = {
					new LVCMOS("A", 1, 20),
					new LVCMOS("B", 1, 19),
					new LVCMOS("C", 6, 17),
					new LVCMOS("D", 5, 16),
					new LVCMOS("E", 6, 15),
					new LVCMOS("F", 7, 14),
					new LVCMOS("G", 7, 14),
					new LVCMOS("H", 8, 11),
					new LVCMOS("J", 12, 12),
				};
		public static LVCMOS[] Range_Bank1 = {
					new LVCMOS("B", 20, 20),
					new LVCMOS("C", 18, 20),
					new LVCMOS("D", 17, 20),
					new LVCMOS("E", 17, 20),
					new LVCMOS("F", 15, 20),

					new LVCMOS("G", 15, 20),
					new LVCMOS("H", 14, 20),
					new LVCMOS("J", 13, 20),
					new LVCMOS("K", 13, 20),
					new LVCMOS("L", 13, 20),

					new LVCMOS("M", 13, 20),
					new LVCMOS("N", 13, 20),

					new LVCMOS("P", 15, 20),
					new LVCMOS("R", 16, 20),
					new LVCMOS("T", 17, 20),
					new LVCMOS("U", 17, 20),
					new LVCMOS("V", 17, 20),
				};
		public static LVCMOS[] Range_Bank2 = {
					new LVCMOS("N", 8, 12),
					new LVCMOS("P", 7, 14),
					new LVCMOS("R", 6, 15),
					new LVCMOS("T", 6, 16),
					new LVCMOS("U", 6, 15),

					new LVCMOS("V", 6, 16),
					new LVCMOS("W", 1, 20),
					new LVCMOS("Y", 1, 20),
				};
		public static LVCMOS[] Range_Bank3 = {
					new LVCMOS("M", 6, 9),
					new LVCMOS("N", 3, 6),
					new LVCMOS("P", 3, 6),
					new LVCMOS("R", 1, 5),
					new LVCMOS("T", 1, 5),

					new LVCMOS("U", 1, 5),
					new LVCMOS("V", 1, 4),
				};
		public static LVCMOS[] Range_Bank4 = {
					new LVCMOS("J", 1, 2),
					new LVCMOS("K", 1, 9),
					new LVCMOS("L", 1, 7),
					new LVCMOS("M", 1, 5),
					new LVCMOS("N", 1, 2),

					new LVCMOS("P", 1, 2),
				};
		public static LVCMOS[] Range_Bank5 = {
					new LVCMOS("C", 1, 4),
					new LVCMOS("D", 1, 2),
					new LVCMOS("E", 1, 4),
					new LVCMOS("F", 1, 6),
					new LVCMOS("G", 1, 6),

					new LVCMOS("H", 1, 7),
					new LVCMOS("J", 3, 8),
				};

		private static BankClass[] g_BankClass =  {
				new BankClass("bank0", BankVolotLevel.BankVolotLevel_3P3, Range_Bank0),
				new BankClass("bank1", BankVolotLevel.BankVolotLevel_3P3, Range_Bank1),
				new BankClass("bank2", BankVolotLevel.BankVolotLevel_1P8, Range_Bank2),
				new BankClass("bank3", BankVolotLevel.BankVolotLevel_3P3, Range_Bank3),
				new BankClass("bank4", BankVolotLevel.BankVolotLevel_3P3, Range_Bank4),
				new BankClass("bank5", BankVolotLevel.BankVolotLevel_3P3, Range_Bank5),
			};
		public static BankClass[] GetEL3LConfig()
		{
			return g_BankClass;
		}
    }
}
