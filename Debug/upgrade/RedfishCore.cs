using Debug.upgrade;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BmcUpgradeTool
{
    public class RedfishCore
    {
        // BMC 默认配置参数
        private const string LoginName = "Administrator";
        private const string LoginPassword = "ttytty`12";

        private readonly HttpClient _httpClient;
        private string _authToken;
        private string _seesionID;
        private readonly Action<Object> Log;

        public RedfishCore(Action<Object> log)
        {
            this.Log = log;
            // 强制使用 TLS 1.2（关键！）
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // 忽略 SSL 证书验证（对应 Python 的 verify=False）
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            _httpClient = new HttpClient(handler);
            _httpClient.Timeout = TimeSpan.FromSeconds(60); // 默认超时
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

                        if (response.Headers.TryGetValues("Location", out var seesionID))
                        {
                            _seesionID = string.Join("", seesionID).Split('/').LastOrDefault();
                            Console.WriteLine($"✅ 获取到 Session ID: {_seesionID}");
                        }
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

            DateTime startTime = DateTime.Now;
            bool isFinished = false;
            bool isFirstLine = true;
            string msgText = string.Empty;

            while ((int)(DateTime.Now - startTime).TotalSeconds < timeoutSeconds)
            {
                if (isFinished)
                {
                    Log(new BMCProgressInfo(bmcIp, "100", "成功"));
                    return (true, "成功");
                }
                await Task.Delay(1000); // 等待1000ms

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
                            Log(new BMCProgressInfo(bmcIp, percent, "破解中"));
                        }
                        else if (msgText.Contains("Upgrading the BMC"))
                        {
                            Log(new BMCProgressInfo(bmcIp, percent, "BMC 升级中"));
                            await Task.Delay(1000); // Python 里这里额外睡了3秒
                        }
                        else if (msgText.Contains("Upgrading the Bios"))
                        {
                            Log(new BMCProgressInfo(bmcIp, percent, "BIOS 升级中"));
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
        /// <summary>
        /// 释放认证令牌（登出/删除会话）
        /// </summary>
        public async Task<bool> DeleteSessionAsync(string ip)
        {
            
            if (string.IsNullOrEmpty(_authToken))
            {
                Console.WriteLine("⚠️ 无有效 Token，无需释放");
                return true;
            }
            if (string.IsNullOrEmpty(_seesionID))
            {
                Console.WriteLine("⚠️ 无有效 Session ID，无需释放");
                return true;
            }

            string sessionUrl = $"https://{ip}/redfish/v1/SessionService/Sessions/{_seesionID}";
            
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, sessionUrl);
                request.Headers.Add("X-Auth-Token", _authToken);
                
                var response = await _httpClient.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"✅ 已释放 Token: {_authToken}");
                    _authToken = null;
                    return true;
                }
                else
                {
                    Console.WriteLine($"⚠️ 释放 Token 失败: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ 释放 Token 异常: {ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// 获取 BMC 固件信息（特别是 ReleaseDate）
        /// </summary>
        /// <param name="ip">BMC 的 IP 地址</param>
        /// <returns>包含操作是否成功及 ReleaseDate 信息的元组</returns>
        public async Task<(bool success, string message)> GetBMCInfoAsync(string ip)
        {
            // 1. 检查认证状态
            if (_authToken == null) 
                return (false, "未授权");

            // 2. 构建请求 URL
            string bmcIp = ip;
            string url = $"https://{bmcIp}/redfish/v1/UpdateService/FirmwareInventory/ActiveBMC";

            // 3. 设置认证头
            _httpClient.DefaultRequestHeaders.Remove("X-Auth-Token");
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _authToken);

            try
            {
                // 4. 发送 GET 请求
                var response = await _httpClient.GetAsync(url);
                
                // 5. 检查 HTTP 状态码
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(responseBody);

                    // 6. 提取 ReleaseDate
                    // 根据提供的 JSON 结构，ReleaseDate 是顶级属性
                    var releaseDateToken = json["ReleaseDate"];
                    
                    if (releaseDateToken == null)
                    {
                        return (false, "未找到 ReleaseDate 字段");
                    }
                    // 直接按 DateTime 取值（Newtonsoft 已经自动解析了）
                    DateTime releaseDate = releaseDateToken.Value<DateTime>();

                    // 关键：告诉 .NET 这个时间是 UTC 时间，然后转换到本地时区
                    DateTime utcTime = DateTime.SpecifyKind(releaseDate, DateTimeKind.Utc);
                    DateTime localTime = utcTime.ToLocalTime();

                    string buildDateStringFormatted = localTime.ToString("yyyy-MM-dd HH:mm:ss");

                    string version = json["Version"].ToString();
                    return (true, version + " "+ buildDateStringFormatted); // 返回 "2026-06-01 11:09:30"
                }
                else
                {
                    return (false, $"HTTP 请求失败，状态码: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // 捕获网络错误或 JSON 解析错误
                return (false, $"发生异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取 BIOS 固件信息（特别是 Version）
        /// </summary>
        /// <param name="ip">BMC 的 IP 地址</param>
        /// <returns>包含操作是否成功及 Version 信息的元组</returns>
        public async Task<(bool success, string message)> GetBIOSInfoAsync(string ip)
        {
            // 1. 检查认证状态
            if (_authToken == null) 
                return (false, "未授权");

            // 2. 构建请求 URL
            string bmcIp = ip;
            string url = $"https://{bmcIp}/redfish/v1/UpdateService/FirmwareInventory/Bios";

            // 3. 设置认证头
            _httpClient.DefaultRequestHeaders.Remove("X-Auth-Token");
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _authToken);

            try
            {
                // 4. 发送 GET 请求
                var response = await _httpClient.GetAsync(url);
                
                // 5. 检查 HTTP 状态码
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(responseBody);

                    // 6. 提取 Version
                    // 根据提供的 JSON 结构，Version 是顶级属性
                    string biosVersion = json["Version"].ToString();
                    
                    if (biosVersion == null)
                    {
                        return (false, "未找到 Version 字段");
                    }

                    return (true, biosVersion); // 返回 "2026-06-01 11:09:30"
                }
                else
                {
                    return (false, $"HTTP 请求失败，状态码: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // 捕获网络错误或 JSON 解析错误
                return (false, $"发生异常: {ex.Message}");
            }
        }
    }
}