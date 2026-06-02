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
        public string updateTime { get; set; }
        public string msg { get; set; }


        public BMCProgressInfo()
        {

        }
        public BMCProgressInfo(string ip, string percent, string msg)
        {
            this.ip = ip;
            this.percent = percent;
            this.msg = msg;
            this.updateTime = DateTime.Now.ToString("HH:mm:ss");
        }

    }
}
