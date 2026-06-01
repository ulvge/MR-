using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug.upgrade
{
    public class BMCProgressInfo
    {
        public string ip { get; set; }
        public int percent { get; set; }
        public string stage { get; set; } // "破解中" 或 "BMC升级中"
        public DateTime updateTime { get; set; }


        public BMCProgressInfo()
        {

        }
        public BMCProgressInfo(string ip, int percent, string stage)
        {
            this.ip = ip;
            this.percent = percent;
            this.stage = stage;
        }

    }
}
