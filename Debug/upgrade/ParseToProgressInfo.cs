using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Debug.upgrade
{
    public class ParseToProgressInfo
    {
        public BMCProgressInfo ParseProgressInfo(string message)
        {
            // 解析格式: "✅ 192.168.1.1 破解中......50%"
            // 或: "✅ 192.168.1.1 BMC 升级中......80%"

            // 移除开头的 ✅ 和空格
            string msg = message.TrimStart('✅').TrimStart();

            // 查找 IP 地址（简单正则或字符串解析）
            string ip = ExtractIP(msg);
            if (string.IsNullOrEmpty(ip)) return null;

            // 提取百分比
            int percent = ExtractPercent(msg);
            if (percent == -1) return null;

            // 提取阶段（破解中 或 BMC升级中）
            string stage = ExtractStage(msg);

            return new BMCProgressInfo(ip, percent, stage);
            // 更新 DataGridView
        }

        private string ExtractIP(string message)
        {
            // IP 地址正则匹配
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(
                @"\b(?:[0-9]{1,3}\.){3}[0-9]{1,3}\b");

            Match match = regex.Match(message);
            return match.Success ? match.Value : null;
        }

        private int ExtractPercent(string message)
        {
            // 提取百分比数字
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(
                @"(\d+)%");

            Match match = regex.Match(message);
            if (match.Success && int.TryParse(match.Groups[1].Value, out int percent))
            {
                return percent;
            }
            return -1;
        }

        private string ExtractStage(string message)
        {
            if (message.Contains("破解中"))
                return "破解中";
            else if (message.Contains("BMC升级中"))
                return "BMC升级中";
            return "处理中";
        }
    }
}
