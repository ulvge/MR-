using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug
{
    class EF2L
    {
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

        private static BankClass[] g_BankClass =  {
                new BankClass("bank0", BankVolotLevel.BankVolotLevel_3P3, Range_Bank0),
                new BankClass("bank1", BankVolotLevel.BankVolotLevel_3P3, Range_Bank1),
                new BankClass("bank2", BankVolotLevel.BankVolotLevel_1P8, Range_Bank2),
                new BankClass("bank3", BankVolotLevel.BankVolotLevel_3P3, Range_Bank3),
                new BankClass("bank4", BankVolotLevel.BankVolotLevel_3P3, Range_Bank4),
                new BankClass("bank5", BankVolotLevel.BankVolotLevel_3P3, Range_Bank5),
            };
        public static BankClass[] GetEL2LConfig()
        {
            return g_BankClass;
        }
    }
}
