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
        public string percent { get; set; }
        public string stage { get; set; } // "破解中" 或 "BMC升级中"
        public string updateTime { get; set; }


        public BMCProgressInfo()
        {

        }
        public BMCProgressInfo(string ip, string percent, string stage)
        {
            this.ip = ip;
            this.percent = percent;
            this.stage = stage;
            this.updateTime = DateTime.Now.ToString("HH:mm:ss");
        }

    }
}
