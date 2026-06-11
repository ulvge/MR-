using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Debug.IPMITool
{
    public class ipmitool
    {
        private string _ipmitoolPath = "..\\..\\IPMITool\\IPMIToolWin1.8.19\\ipmitool.exe";
        /// <summary>
        /// 直接执行完整命令（不替换 IP）
        /// </summary>
        public async Task<(bool Success, string Output, string Error)> ExecuteFullCommandAsync(string fullCommand)
        {
            string absolutePath = Path.GetFullPath(_ipmitoolPath);
            if (!File.Exists(absolutePath))
            {
                Console.WriteLine("IPMI.exe 路径不存在");
                return (false, "",  "IPMI.exe 路径不存在");
            }
            var process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = absolutePath,
                Arguments = fullCommand,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
                WorkingDirectory = Path.GetDirectoryName(absolutePath)
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

                return (process.ExitCode == 0, output + ". " + error, error); // error信息，可能是一些辅助的调试信息
            }
            catch (Exception ex)
            {
                return (false, "", ex.Message);
            }
        }
    }
}
