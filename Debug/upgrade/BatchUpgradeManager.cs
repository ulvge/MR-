using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BmcUpgradeTool
{
    public class BatchUpgradeManager
    {
        // 批量升级核心方法
        // ipTails: IP地址的尾数数组，例如 new[] { "82", "83", "85" }
        // filePath: 固件文件的本地绝对路径
        // progressCallback: 进度回调，用于在UI上实时打印日志
        public async Task StartBatchUpgradeAsync(string[] ipTails, string filePath, Action<string> progressCallback)
        {
            if (!File.Exists(filePath))
            {
                progressCallback($"❌ 错误：找不到文件 {filePath}");
                return;
            }

            progressCallback($"🚀 开始批量升级，共 {ipTails.Length} 台设备...");

            // 为每个IP创建一个独立的升级任务
            var upgradeTasks = ipTails.Select(ipTail => UpgradeSingleDeviceAsync(ipTail, filePath, progressCallback));

            // Task.WhenAll 会并发执行所有任务，并等待它们全部完成
            await Task.WhenAll(upgradeTasks);

            progressCallback("🎉 所有设备的升级任务已全部执行完毕！");
        }

        // 单个设备的完整升级流程
        private async Task UpgradeSingleDeviceAsync(string ipTail, string filePath, Action<string> log)
        {
            string currentIp = $"[IP尾数:{ipTail}]";
            try
            {
                var client = new BmcRedfishClient(); // 使用你之前封装好的核心类

                // 1. 获取 Token
                log($"{currentIp} 正在获取认证令牌...");
                bool authSuccess = await client.GetAuthTokenAsync(ipTail);
                if (!authSuccess) { log($"{currentIp} ❌ 获取Token失败，终止升级。"); return; }

                // 2. 上传文件
                log($"{currentIp} 正在上传固件文件...");
                bool uploadSuccess = await client.UploadFileAsync(filePath, ipTail);
                if (!uploadSuccess) { log($"{currentIp} ❌ 文件上传失败，终止升级。"); return; }
                log($"{currentIp} ✅ 文件上传成功！");

                // 3. 启动更新
                string fileName = Path.GetFileName(filePath);
                string remotePath = $"/tmp/web/{fileName}";
                log($"{currentIp} 正在请求启动更新任务...");
                var (startSuccess, taskId) = await client.StartUpdateAsync(remotePath, ipTail);
                if (!startSuccess || string.IsNullOrEmpty(taskId)) { log($"{currentIp} ❌ 启动更新任务失败。"); return; }
                log($"{currentIp} ✅ 升级任务已启动，TaskID: {taskId}");

                // 4. 轮询检查状态 (注意：升级通常耗时较长，这里将超时时间设为600秒)
                log($"{currentIp} 开始监控升级状态（请耐心等待）...");
                var (statusSuccess, finalMsg) = await client.CheckUpdateStatusAsync(ipTail, taskId, timeoutSeconds: 600);

                if (statusSuccess)
                {
                    log($"{currentIp} 🎉 升级成功！消息: {finalMsg}");
                }
                else
                {
                    log($"{currentIp} ❌ 升级失败或超时: {finalMsg}");
                }
            }
            catch (Exception ex)
            {
                log($"{currentIp} 💥 发生未处理的异常: {ex.Message}");
            }
        }
    }
}