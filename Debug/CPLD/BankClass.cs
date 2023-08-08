using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug
{
    class BankClass
    {
        public string bankName;
        public string bankVolotLevel;
        public LVCMOS[] range;

        public BankClass(string bankName, string bankVolotLevel, LVCMOS[] range_Bank)
        {
            this.bankName = bankName;
            this.bankVolotLevel = bankVolotLevel;
            this.range = range_Bank;
        }
    }
}
