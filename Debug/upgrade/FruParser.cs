using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Debug.upgrade
    {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class FruParser
    {
        /// <summary>
        /// 解析 ipmitool fru print 的输出，直接返回一个字典
        /// Key 是内部字段名 (如 "product_name")，Value 是解析出的值
        /// </summary>
        public static Dictionary<string, string> Parse(string ipmitoolOutput)
        {
            var result = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(ipmitoolOutput)) return result;

            // 1. 从 FruTable 中提取出 "IPMI字段名 -> 内部Key" 的映射，用于快速查找
            var ipmiToInternalMap = FruUpdate.FruTable
                .ToDictionary(kvp => kvp.Value.IpmiFieldName, kvp => kvp.Key);

            // 2. 逐行读取并解析
            string[] lines = ipmitoolOutput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (line.Contains("Area Checksum") || !line.Contains(":")) continue;

                int colonIndex = line.IndexOf(':');
                if (colonIndex == -1) continue;

                string rawKey = line.Substring(0, colonIndex).Trim();
                string value = line.Substring(colonIndex + 1).Trim();

                // 3. 如果 ipmitool 返回的字段在我们的表中，就存入结果字典
                if (ipmiToInternalMap.TryGetValue(rawKey, out string internalKey))
                {
                    result[internalKey] = value;
                }
            }

            return result;
        }
    }    
}