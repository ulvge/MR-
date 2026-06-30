using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Debug.upgrade
{
    public class SigneHPM
    {
        // 定义目标接口地址 (对应 JS 中的 UploadRul)
        private static readonly string ServiceUrl = "http://192.168.1.101:5000";
        private static readonly string UploadRul = ServiceUrl + "/upload-hpm";
        private static readonly string DownloadRul = ServiceUrl + "/downloads/";
        // 定义允许的文件扩展名 (对应 JS 中的 acceptExt)
        private static readonly string AcceptExt = ".hpm";

        private readonly Action<object> Log;

        public string signedFileName = string.Empty;
        private bool isSignedSuccess = false;
        private bool isDownloadSuccess = false;
        public SigneHPM(Action<object> log)
        {
            Log = log;
        }

        public async Task<bool> UpdateAndSign(string filePath)
        {
            // 模拟 JS: if (files.length === 0) return;
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {

                Log("❌ 文件不存在或路径无效\r\n");
                return false;
            }

            // 2. 校验扩展名 (模拟 JS: if (!file.name.endsWith(acceptExt)))
            string fileExtension = Path.GetExtension(filePath).ToLower();
            if (fileExtension != AcceptExt)
            {
                Log($"❌ 文件扩展名错误，仅允许 {AcceptExt} 文件\r\n");
                return false;
            }

            // 3. 模拟 JS: showStatus(statusDiv, '📤 上传中...', 'info');
            Log("📤 上传文件到签名服务器...\r\n");
            // Log(new BMCProgressInfo(currentIp, "0", "上传文件到签名服务器..."));

            try
            {
                // 4. 准备 FormData 并发送请求 (模拟 JS: fetch(UploadRul, { method: 'POST', body: formData }))
                HttpClient httpClient = new HttpClient();
                MultipartFormDataContent formData = new MultipartFormDataContent();

                // 读取文件并添加到表单中，键名为 "file" (对应 JS: formData.append('file', file))
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var streamContent = new StreamContent(fileStream);
                // 设置文件名，确保后端能获取到原始文件名
                formData.Add(streamContent, "file", Path.GetFileName(filePath));

                // 发送 POST 请求
                HttpResponseMessage response = await httpClient.PostAsync(UploadRul, formData);

                // 5. 解析 JSON 响应 (模拟 JS: response.json())
                string responseBody = await response.Content.ReadAsStringAsync();

                // 使用 System.Text.Json 解析返回的 JSON 对象
                JsonDocument doc = JsonDocument.Parse(responseBody);
                JsonElement root = doc.RootElement;

                // 6. 根据返回结果处理逻辑 (模拟 JS: if (data.error) { ... } else { ... })
                if (root.TryGetProperty("error", out JsonElement errorElement))
                {
                    Log($"❌ {errorElement.GetString()}\r\n");
                }
                else if (root.TryGetProperty("message", out JsonElement messageElement))
                {
                    if (root.TryGetProperty("output_filename", out var outElem))
                    {
                        string outputFilename = outElem.GetString();
                        if (!string.IsNullOrEmpty(outputFilename))
                        {
                            signedFileName = outputFilename;
                            isSignedSuccess = true;
                            Log($"✅ {messageElement.GetString()}\n输出文件: {outputFilename}, success\r\n");
                            await Task.Delay(2000);
                        }
                    }
                }
                if (!isSignedSuccess)
                {
                    Log("❌ 服务器签名失败\r\n");
                }
                httpClient.Dispose();
                fileStream.Dispose();
                streamContent.Dispose();
                doc.Dispose();
                response?.Dispose();
                return isSignedSuccess;
            }
            catch (Exception ex)
            {
                // 7. 捕获网络或解析异常 (模拟 JS: .catch(err => {...}))
                Log($"❌ 上传失败: {ex.Message}\r\n");
            }
            return false;
        }
        public async Task<bool> DownloadSignedHpm(string downloadTempPath)
        {
            if (!isSignedSuccess)
            {
                Log("❌ 签名未成功，无法下载\r\n");
                return false;
            }
            try
            {
                if (!Directory.Exists(downloadTempPath))
                {
                    Directory.CreateDirectory(downloadTempPath);
                }
                string downloadUrl = $"{DownloadRul}{signedFileName}";
                Log($"📥 正在下载签名文件: {downloadUrl}\r\n");
                HttpClient httpClient = new HttpClient();
                // Assuming the server provides a download URL for the signed file
                HttpResponseMessage response = await httpClient.GetAsync(downloadUrl);
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var fileStream = new FileStream($"{downloadTempPath}\\{signedFileName}", FileMode.Create, FileAccess.Write))
                    {
                        await stream.CopyToAsync(fileStream);
                        // 保存文件
                        fileStream.Flush();
                    }
                    Log($"✅ 下载成功: {downloadTempPath}\r\n");
                    isDownloadSuccess = true;
                }

                response.Dispose();
                httpClient.Dispose();
            }catch (Exception ex)
            {
                Log($"❌ 下载失败: {ex.Message}\r\n");
            }
            return isDownloadSuccess;
        }
    }
}