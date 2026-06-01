using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BmcUpgradeTool
{
    public class BmcRedfishClient
    {
        // BMC 默认配置参数
        private const string LoginName = "Administrator";
        private const string LoginPassword = "ttytty`12";

        private readonly HttpClient _httpClient;
        private string _authToken;
        private readonly Action<string> Log;

        public BmcRedfishClient(Action<string> log)
        {
            this.Log = log;
            // 强制使用 TLS 1.2（关键！）
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // 忽略 SSL 证书验证（对应 Python 的 verify=False）
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            _httpClient = new HttpClient(handler);
            _httpClient.Timeout = TimeSpan.FromSeconds(30); // 默认超时
        }

        /// <summary>
        /// 步骤1：获取认证令牌 (X-Auth-Token)
        /// </summary>
        public async Task<bool> GetAuthTokenAsync(string ip)
        {
            string bmcIp = ip;
            string sessionUrl = $"https://{bmcIp}/redfish/v1/SessionService/Sessions";

            var payload = new JObject
            {
                ["UserName"] = LoginName,
                ["Password"] = LoginPassword
            };

            try
            {
                var content = new StringContent(payload.ToString(), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(sessionUrl, content);

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    // 优先从响应头获取 Token
                    if (response.Headers.TryGetValues("X-Auth-Token", out var values))
                    {
                        _authToken = string.Join("", values);
                        Console.WriteLine($"✅ 获取到 Token: {_authToken}");
                        return true;
                    }

                    // 尝试从响应体获取 Token
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(responseBody);
                    if (json["AuthToken"] != null)
                    {
                        _authToken = json["AuthToken"].ToString();
                        Console.WriteLine($"✅ 从响应体获取到 Token: {_authToken}");
                        return true;
                    }

                    Console.WriteLine("⚠️ 会话创建成功，但未找到 Token");
                }
                else
                {
                    Console.WriteLine($"❌ 获取 Token 失败: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 请求异常: {ex.Message}");
            }
            return false;
        }
        public async Task<bool> UploadFileAsync(string filePath, string ip)
        {
            if (_authToken == null) return false;
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"❌ 文件不存在: {filePath}");
                return false;
            }

            string bmcIp = ip;
            string url = $"https://{bmcIp}/redfish/v1/UpdateService/FirmwareInventory";

            Console.WriteLine($"📤 开始上传文件到 {url}");

            MultipartFormDataContent content = null;
            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                Console.WriteLine($"文件大小: {fileBytes.Length} 字节");

                content = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");

                // 关键修正：手动设置 Content-Disposition，确保 name 带双引号
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "\"imgfile\"",
                    FileName = $"\"{Path.GetFileName(filePath)}\""
                };

                content.Add(fileContent);

                // 关键修正：移除 boundary 的双引号
                var boundaryValue = content.Headers.ContentType.Parameters
                    .FirstOrDefault(p => p.Name == "boundary");
                if (boundaryValue != null)
                {
                    boundaryValue.Value = boundaryValue.Value.Replace("\"", string.Empty);
                }

                // 使用 HttpRequestMessage
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Headers.Add("X-Auth-Token", _authToken);
                    request.Content = content;

                    // 关键：使用 ResponseHeadersRead，避免等待完整响应体
                    var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                    if (response.StatusCode == HttpStatusCode.OK ||
                        response.StatusCode == HttpStatusCode.Created ||
                        response.StatusCode == HttpStatusCode.Accepted)
                    {
                        return true;
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"❌ 文件上传失败: {response.StatusCode}, 信息: {error}");
                    }
                }
            }
            catch (TaskCanceledException taskEx)
            {
                Console.WriteLine($"❌ 任务被取消: {taskEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 异常: {ex.GetType().Name}: {ex.Message}");
            }
            finally
            {
                // 确保 content 被释放
                content?.Dispose();
            }
            return false;
        }

        public async Task<bool> UploadFileAsync_NG(string filePath, string ip)
        {
            if (_authToken == null) return false;
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"❌ 文件不存在: {filePath}");
                return false;
            }

            // 注意：这里需要动态获取当前请求的 BaseAddress 里的 IP，或者你在外部传入完整的 URL
            // 为了简单起见，这里假设我们依然使用默认的 IP Head + Tail 构造 URL
            // 在实际 UI 程序中，建议把 IP 地址作为类的属性保存下来
            string bmcIp = ip;
            string url = $"https://{bmcIp}/redfish/v1/UpdateService/FirmwareInventory";

            try
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    var content = new MultipartFormDataContent();
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                    content.Add(fileContent, "imgfile", Path.GetFileName(filePath));

                    // 添加 Token 到请求头
                    _httpClient.DefaultRequestHeaders.Remove("X-Auth-Token");
                    _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _authToken);

                    var response = await _httpClient.PostAsync(url, content);

                    if (response.StatusCode == HttpStatusCode.OK ||
                        response.StatusCode == HttpStatusCode.Created ||
                        response.StatusCode == HttpStatusCode.Accepted)
                    {
                        return true;
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"❌ 文件上传失败: {response.StatusCode}, 信息: {error}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 上传请求异常: {ex.Message}");
            }
            return false;
        }
        /// <summary>
        /// 步骤3：启动更新任务 (SimpleUpdate)
        /// </summary>
        public async Task<(bool success, string taskId)> StartUpdateAsync(string remoteFilePath, string ip)
        {
            if (_authToken == null) return (false, null);
    
            string bmcIp = ip;
            string url = $"https://{bmcIp}/redfish/v1/UpdateService/Actions/UpdateService.SimpleUpdate";

            var payload = new JObject
            {
                ["ImageURI"] = remoteFilePath,
                ["ActiveMode"] = "Immediately"
            };

            try
            {
                var content = new StringContent(payload.ToString(), System.Text.Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Remove("X-Auth-Token");
                _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _authToken);

                var response = await _httpClient.PostAsync(url, content);

                if (response.StatusCode == HttpStatusCode.OK ||
                    response.StatusCode == HttpStatusCode.Created ||
                    response.StatusCode == HttpStatusCode.Accepted)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(responseBody);

                    // 检查 Messages 数组
                    if (json["Messages"] == null || !json["Messages"].HasValues)
                    {
                        string taskId = json["Id"]?.ToString();
                        return (true, taskId);
                    }
                    else
                    {
                        Console.WriteLine($"❌ 启动更新返回了错误信息: {json["Messages"]}");
                    }
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"❌ 启动更新失败: {response.StatusCode}, 信息: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 启动更新请求异常: {ex.Message}");
            }
            return (false, null);
        }

        /// <summary>
        /// 步骤4：轮询检查升级状态
        /// </summary>
        public async Task<(bool success, string message)> CheckUpdateStatusAsync(string ip, string taskId, int timeoutSeconds = 30)
        {
            if (_authToken == null) return (false, "未授权");

            string bmcIp = ip;
            string url = $"https://{bmcIp}/redfish/v1/TaskService/Tasks/{taskId}";

            _httpClient.DefaultRequestHeaders.Remove("X-Auth-Token");
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _authToken);

            int count = 0;
            bool isFinished = false;
            bool isFirstLine = true;
            string msgText = string.Empty;
            while (count < timeoutSeconds)
            {
                if (isFinished)
                {
                    return (true, msgText);
                }
                await Task.Delay(300); // 等待2秒
                count += 2;

                try
                {
                    var response = await _httpClient.GetAsync(url);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var json = JObject.Parse(responseBody);

                        var messages = json["Messages"];
                        if (messages == null || !messages.HasValues) continue;

                        // 获取第一条消息的 Message 字段
                        msgText = messages?["Message"]?.ToString();
                        if (string.IsNullOrEmpty(msgText)) continue;


                        string percent = json["PercentComplete"]?.ToString();

                        // 判断关键状态 (完全照搬你的 Python 逻辑)
                        if (msgText.Contains("Upgrading the WhiteBranding is complete") ||
                            msgText.Contains("Upgrading the BMC is complete") ||
                            msgText.Contains("Upgrading the Bios is complete"))
                        {
                            isFinished = true;
                            percent = "100";    // 这里先不返回，打印一次100%后，再退出
                        }

                        if (msgText.Contains("Upgrading the WhiteBranding"))
                        {
                            Log($"✅ {bmcIp} 破解中......{percent}% {(isFirstLine ? "\r\n" : "\r")}");
                        }
                        if (msgText.Contains("Upgrading the BMC"))
                        {
                            Log($"✅ {bmcIp} BMC 升级中......{percent}%{(isFirstLine ? "\r\n" : "\r")}");
                            await Task.Delay(3000); // Python 里这里额外睡了3秒
                        }
                        if (msgText.Contains("Upgrading the Bios"))
                        {
                            Log($"✅ {bmcIp} BIOS 升级中......{percent}%{(isFirstLine ? "\r\n" : "\r")}");
                        }
                        isFirstLine = false;
                    }
                }
                catch (Exception ex)
                {
                    // 网络波动等异常，继续重试
                    Log($"查询状态异常: {ex.Message}");
                }
            }
            return (false, "检查升级状态超时");
        }
    }
}