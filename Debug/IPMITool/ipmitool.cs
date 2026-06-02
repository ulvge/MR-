using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug.IPMITool
{
    public class ipmitool
    {
        private string _ipmitoolPath = "..\\..\\IPMITool\\IPMIToolWin1.8.18\\ipmitool.exe";
        /// <summary>
        /// 直接执行完整命令（不替换 IP）
        /// </summary>
        public async Task<(bool Success, string Output, string Error)> ExecuteFullCommandAsync(string fullCommand)
        {
            if (!File.Exists(_ipmitoolPath))
            {
                Console.WriteLine("IPMI.exe 路径不存在");
                return (false, "",  "IPMI.exe 路径不存在");
            }
            var process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = _ipmitoolPath,
                Arguments = fullCommand,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8
            };

            try
            {
                process.Start();

                // 异步读取输出
                var outputTask = process.StandardOutput.ReadToEndAsync();
                var errorTask = process.StandardError.ReadToEndAsync();

                // 等待进程退出（使用 Task.Run 包装 WaitForExit）
                await Task.Run(() => process.WaitForExit());

                string output = await outputTask;
                string error = await errorTask;

                return (process.ExitCode == 0, output, error);
            }
            catch (Exception ex)
            {
                return (false, "", ex.Message);
            }
        }
    }
}
