using Debug.upgrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug.IPMITool
{
    public class IpmiResultParse
    {
        private const string DefaultIpHead = "192.168.60.";
        private readonly Action<object> Log;
        private string username;
        private string password;
        public IpmiResultParse(Action<object> log, string username, string password)
        {
            this.Log = log;
            this.username = username;
            this.password = password;
        }
        private string GetCommandHead(string ip, string ipmiCmd)
        {
            string fullCmd = $"-I lanplus -H {DefaultIpHead+ip} -U {username} -P {password} -C 17 {ipmiCmd}";
            return fullCmd;
        }

        // 批量发送ipmi命令
        // ipTails: IP地址的尾数数组，例如 new[] { "82", "83", "85" }
        // filePath: 固件文件的本地绝对路径
        // progressCallback: 进度回调，用于在UI上实时打印日志
        public async Task SendIPMICmdBatchAsync(string[] ipTails, string ipmiCmd)
        {
            Log($"🚀 开始批量发送ipmi命令 {ipmiCmd}，共 {ipTails.Length} 台设备...\r\n");

            // 为每个IP创建一个独立的任务
            var upgradeTasks = ipTails.Select(ipTail => SendIPMICmdSingleAsync(DefaultIpHead+ipTail, GetCommandHead(ipTail, ipmiCmd)));

            // Task.WhenAll 会并发执行所有任务，并等待它们全部完成
            await Task.WhenAll(upgradeTasks);

            Log("🎉 所有设备的ipmi命令已全部发送完毕！\r\n");
        }
        public async Task<(bool Success, string Output, string Error)> SendIPMICmdAsync(string ipTail, string ipmiCmd)
        {
            Log($"🚀 开始批量发送ipmi命令 {ipmiCmd}，\r\n");

            // 为每个IP创建一个独立的任务
            var (success, output, error) = await SendIPMICmdSingleAsync(DefaultIpHead + ipTail, GetCommandHead(ipTail, ipmiCmd), false);

            Log("🎉 所有设备的ipmi命令已全部发送完毕！\r\n");

            return (success, output, error);
        }

        // 单个设备的完整升级流程
        private async Task<(bool Success, string Output, string Error)> SendIPMICmdSingleAsync(string currentIp, string ipmiCmd, bool isPrintfResult = true)
        {
            try
            {
                var ipmi = new ipmitool();

                // 异步调用
                var (success, output, error) = await ipmi.ExecuteFullCommandAsync(ipmiCmd, Log);

                if (isPrintfResult)
                {
                    Log($"{currentIp} {output}");
                }
                if (success)
                {
                    Log(new BMCProgressInfo(currentIp, "100", output));
                }
                else
                {
                    Log(new BMCProgressInfo(currentIp, "0", error));
                }
                return (success, output, error);
            }
            catch (Exception ex)
            {
                Log(new BMCProgressInfo(currentIp, "0", ex.Message));
                Log($"{currentIp} 💥 发生未处理的异常: {ex.Message}\r\n");
            }

            return (false, "", "");

        }
    }
}
