using Debug.upgrade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static Debug.UpgradeBMC;

namespace BmcUpgradeTool
{
    public class RedfishManager
    {
        private readonly Action<object> Log;
        public RedfishManager(Action<object> log)
        {
            this.Log = log;
        }
        private const string DefaultIpHead = "192.168.60.";
        // 批量升级核心方法
        // ipTails: IP地址的尾数数组，例如 new[] { "82", "83", "85" }
        // filePath: 固件文件的本地绝对路径
        // progressCallback: 进度回调，用于在UI上实时打印日志
        public async Task UpgradeBatchAsync(string[] ipTails, string filePath)
        {
            if (!File.Exists(filePath))
            {
                Log($"❌ 错误：找不到文件 {filePath}\r\n");
                return;
            }

            Log($"🚀 开始批量升级，共 {ipTails.Length} 台设备...\r\n");

            // 为每个IP创建一个独立的升级任务
            var upgradeTasks = ipTails.Select(ipTail => UpgradeSingleDeviceAsync(DefaultIpHead+ipTail, filePath));

            // Task.WhenAll 会并发执行所有任务，并等待它们全部完成
            await Task.WhenAll(upgradeTasks);

            Log("🎉 所有设备的升级任务已全部执行完毕！\r\n");
        }

        // 单个设备的完整升级流程
        private async Task UpgradeSingleDeviceAsync(string ip, string filePath)
        {
            string currentIp = ip;
            try
            {
                var client = new RedfishCore(Log); // 使用你之前封装好的核心类

                // 1. 获取 Token
                Log($"{currentIp} 正在获取认证令牌...\r\n");
                Log(new BMCProgressInfo(currentIp, "0", "开始升级"));
                bool authSuccess = await client.GetAuthTokenAsync(currentIp);
                if (!authSuccess) {
                    Log(new BMCProgressInfo(currentIp, "0", "获取Token失败，终止升级"));
                    return; 
                }

                // 2. 上传文件
                Log($"{currentIp} 正在上传固件文件 {filePath}\r\n");
                bool uploadSuccess = await client.UploadFileAsync(filePath, currentIp);
                if (!uploadSuccess) {
                    Log(new BMCProgressInfo(currentIp, "0", "文件上传失败，终止升级"));
                    return; 
                }
                Log($"{currentIp} ✅ 文件上传成功！\r\n");

                // 3. 启动更新
                string fileName = Path.GetFileName(filePath);
                string remotePath = $"/tmp/web/{fileName}";
                Log($"{currentIp} 正在请求启动更新任务...\r\n");
                var (startSuccess, taskId) = await client.StartUpdateAsync(remotePath, currentIp);
                if (!startSuccess || string.IsNullOrEmpty(taskId)) {
                    Log(new BMCProgressInfo(currentIp, "0", taskId));
                    return; 
                }
                Log($"{currentIp} ✅ 升级任务已启动，TaskID: {taskId}\r\n");

                // 4. 轮询检查状态 (注意：升级通常耗时较长，这里将超时时间设为300秒)
                Log($"{currentIp} 开始监控升级状态（请耐心等待）...\r\n");
                var (statusSuccess, finalMsg) = await client.CheckUpdateStatusAsync(currentIp, taskId, timeoutSeconds: 300);

                if (statusSuccess)
                {
                    Log($"{currentIp} 🎉 升级成功！消息: {finalMsg}\r\n");
                }
                else
                {
                    Log(new BMCProgressInfo(currentIp, "0", finalMsg));
                }

                Log($"{currentIp} 准备退出...\r\n");
                bool exitSuccess = await client.DeleteSessionAsync(currentIp);
                if (!exitSuccess) {
                    Log(new BMCProgressInfo(currentIp, "100", "退出失败"));
                    return; 
                }
            }
            catch (Exception ex)
            {
                Log($"{currentIp} 💥 发生未处理的异常: {ex.Message}\r\n");
            }
        }

        public async Task GetFirmwaretBatchAsync(string[] ipTails, DeviceType deviceType)
        {
            Log($"🚀 开始批量查询 BMC 版本，共 {ipTails.Length} 台设备...\r\n");

            // 为每个IP创建一个独立的升级任务
            var upgradeTasks = ipTails.Select(ipTail => GetBMCFirmWareAsync(DefaultIpHead + ipTail, deviceType));

            // Task.WhenAll 会并发执行所有任务，并等待它们全部完成
            await Task.WhenAll(upgradeTasks);

            Log("🎉 所有设备的查询任务已全部执行完毕！\r\n");
        }
        // 单个设备的完整升级流程
        private async Task GetBMCFirmWareAsync(string ip, DeviceType deviceType)
        {
            string currentIp = ip;
            try
            {
                var client = new RedfishCore(Log); // 使用你之前封装好的核心类

                // 1. 获取 Token
                Log($"{currentIp} 正在获取认证令牌...\r\n");
                Log(new BMCProgressInfo(currentIp, "0", "开始获取版本信息"));
                bool authSuccess = await client.GetAuthTokenAsync(currentIp);
                if (!authSuccess)
                {
                    Log(new BMCProgressInfo(currentIp, "0", "获取Token失败"));
                    return;
                }

                // 2 获取 BMC 版本信息
                (bool success, string message) result = (false, "未执行查询");
                switch (deviceType)
                {
                    case DeviceType.BMC:
                        result = await client.GetBMCInfoAsync(currentIp);
                        if (result.success)
                        {
                            Log(new BMCProgressInfo(currentIp, "100", result.message));
                            Log($"{currentIp} 获取BMC版本成功，{result.message}\r\n");
                        }
                        else
                        {
                            Log($"{currentIp} 获取BMC版本失败\r\n");
                        }
                        break;
                    case DeviceType.BIOS:
                        result = await client.GetBIOSInfoAsync(currentIp);
                        if (result.success)
                        {
                            Log(new BMCProgressInfo(currentIp, "100", result.message));
                            Log($"{currentIp} 获取BIOS版本成功，{result.message}\r\n");
                        }
                        else
                        {
                            Log($"{currentIp} 获取BIOS版本失败\r\n");
                        }
                        break;
                    case DeviceType.CPLD:
                        break;
                }

                //GetBIOSInfoAsync


                Log($"{currentIp} 准备退出...\r\n");
                bool exitSuccess = await client.DeleteSessionAsync(currentIp);
                if (!exitSuccess)
                {
                    Log(new BMCProgressInfo(currentIp, "99", result.message));
                    return;
                }
            }
            catch (Exception ex)
            {
                Log($"{currentIp} 💥 发生未处理的异常: {ex.Message}\r\n");
            }
        }
    }
}