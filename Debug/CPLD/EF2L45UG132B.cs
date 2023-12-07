using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug
{
    class EF2L45UG132B
    {
        public static LVCMOS[] Range_Bank0 = {
                    new LVCMOS("A", 2, 13),
                    new LVCMOS("B", 3, 13),
                    new LVCMOS("C", 4, 12),
                };
        public static LVCMOS[] Range_Bank1 = {
                    new LVCMOS("B", 14, 14),
                    new LVCMOS("C", 13, 14),
                    new LVCMOS("D", 12, 12),
                    new LVCMOS("E", 12, 14),
                    new LVCMOS("F", 12, 14),

                    new LVCMOS("G", 12, 14),
                    new LVCMOS("H", 12, 14),
                    new LVCMOS("J", 12, 14),
                    new LVCMOS("K", 12, 14),

                    new LVCMOS("L", 12, 14),
                    new LVCMOS("M", 12, 14),
                    new LVCMOS("N", 13, 14),
                };
        public static LVCMOS[] Range_Bank2 = {
                    new LVCMOS("M", 3, 11),
                    new LVCMOS("N", 2, 12),
                    new LVCMOS("P", 1, 13),
                };
        public static LVCMOS[] Range_Bank3 = {
                    new LVCMOS("J", 3, 3),
                    new LVCMOS("K", 1, 3),
                    new LVCMOS("L", 1, 3),
                    new LVCMOS("M", 1, 2),
                };
        public static LVCMOS[] Range_Bank4 = {
                    new LVCMOS("F", 1, 1),
                    new LVCMOS("F", 3, 3),
                    new LVCMOS("G", 1, 1),
                    new LVCMOS("G", 3, 3),
                    new LVCMOS("H", 1, 3),
                    new LVCMOS("J", 1, 2),
                };
        public static LVCMOS[] Range_Bank5 = {
                    new LVCMOS("B", 1, 2),
                    new LVCMOS("C", 1, 3),
                    new LVCMOS("D", 1, 3),
                    new LVCMOS("E", 1, 3),
                    new LVCMOS("F", 2, 2),
                };

        private static BankClass[] g_BankClass =  {
                new BankClass("bank0", BankVolotLevel.BankVolotLevel_3P3, Range_Bank0),
                new BankClass("bank1", BankVolotLevel.BankVolotLevel_3P3, Range_Bank1),
                new BankClass("bank2", BankVolotLevel.BankVolotLevel_3P3, Range_Bank2),
                new BankClass("bank3", BankVolotLevel.BankVolotLevel_3P3, Range_Bank3),
                new BankClass("bank4", BankVolotLevel.BankVolotLevel_3P3, Range_Bank4),
                new BankClass("bank5", BankVolotLevel.BankVolotLevel_3P3, Range_Bank5),
            };
        public static BankClass[] GetConfig()
        {
            return g_BankClass;
        }
    }
}
