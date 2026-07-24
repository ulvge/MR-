using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug.upgrade
{
    public static class FruUpdate
    {
        // 内部结构：定义每一个 FRU 字段对应的协议参数
        public class FruField
        {
            public string DisplayName { get; set; } // 下拉列表显示的名称
            public string IpmiFieldName { get; set; }    // 【新增】ipmitool fru print 返回的英文字段名
            public byte Area { get; set; }          // 区域 (0x1=Chassis, 0x2=Board, 0x3=Product)
            public byte Offset { get; set; }        // 偏移量
            public bool IsString { get; set; }      // 是否为字符串类型 (true: 需转换ASCII; false: 特殊格式如时间/类型)
            public byte ExtraByte { get; set; }     // 针对特殊字段的额外字节 (如 CHASSISTYPE 的 0x01, BOARDMFGDATE 的 0x03)

            // 用于 Product Serial 这种需要写两次的特殊字段
            public byte? SecondaryArea { get; set; }
            public byte? SecondaryOffset { get; set; }
        }


        public static readonly Dictionary<string, FruField> FruTable = new Dictionary<string, FruField>
        {
            // --- Chassis Area (0x1) ---
            { "chassis_type",   new FruField { DisplayName = "机箱类型",     IpmiFieldName = "Chassis Type",          Area = 0x1, Offset = 0x00, IsString = false, ExtraByte = 0x01 } },
            { "chassis_pn",     new FruField { DisplayName = "机箱料号",     IpmiFieldName = "Chassis Part Number",   Area = 0x1, Offset = 0x01, IsString = true } },
            { "chassis_sn",     new FruField { DisplayName = "机箱序列号",   IpmiFieldName = "Chassis Serial",        Area = 0x1, Offset = 0x02, IsString = true } },
            { "chassis_extra",  new FruField { DisplayName = "机箱自定义",   IpmiFieldName = "Chassis Extra",         Area = 0x1, Offset = 0x03, IsString = true } },

            // --- Board Area (0x2) ---
            { "board_mfg_date", new FruField { DisplayName = "主板制造日期", IpmiFieldName = "Board Mfg Date",        Area = 0x2, Offset = 0x00, IsString = false, ExtraByte = 0x03 } },
            { "board_mfg",      new FruField { DisplayName = "主板制造商",   IpmiFieldName = "Board Mfg",             Area = 0x2, Offset = 0x01, IsString = true } },
            { "board_product",  new FruField { DisplayName = "主板产品名",   IpmiFieldName = "Board Product",         Area = 0x2, Offset = 0x02, IsString = true } },
            { "board_sn",       new FruField { DisplayName = "主板序列号",   IpmiFieldName = "Board Serial",          Area = 0x2, Offset = 0x03, IsString = true } },
            { "board_pn",       new FruField { DisplayName = "主板料号",     IpmiFieldName = "Board Part Number",     Area = 0x2, Offset = 0x04, IsString = true } },
            { "board_extra",    new FruField { DisplayName = "主板自定义",   IpmiFieldName = "Board Extra",           Area = 0x2, Offset = 0x06, IsString = true } },

            // --- Product Area (0x3) ---
            { "product_mfg",    new FruField { DisplayName = "产品制造商",   IpmiFieldName = "Product Manufacturer",  Area = 0x3, Offset = 0x00, IsString = true } },
            { "product_name",   new FruField { DisplayName = "产品名称",     IpmiFieldName = "Product Name",          Area = 0x3, Offset = 0x01, IsString = true } },
            { "product_pn",     new FruField { DisplayName = "产品料号",     IpmiFieldName = "Product Part Number",   Area = 0x3, Offset = 0x02, IsString = true } },
            { "product_ver",    new FruField { DisplayName = "产品版本",     IpmiFieldName = "Product Version",       Area = 0x3, Offset = 0x03, IsString = true } },
            { "product_sn",     new FruField { DisplayName = "产品序列号",   IpmiFieldName = "Product Serial",        Area = 0x3, Offset = 0x04, IsString = true, SecondaryArea = 0x6, SecondaryOffset = 0x03 } },
            { "product_asset",  new FruField { DisplayName = "产品资产标签", IpmiFieldName = "Product Asset Tag",     Area = 0x3, Offset = 0x05, IsString = true } },
            { "product_extra",  new FruField { DisplayName = "产品自定义",   IpmiFieldName = "Product Extra",         Area = 0x3, Offset = 0x07, IsString = true } },
        };

        // 1. 将字符串转换为 IPMI 要求的 Hex 字符串格式 (长度Hex + 内容Hex)
        // 例如输入 "MyServer" -> "0x08 0x4D 0x79 0x53 0x65 0x72 0x76 0x65 0x72"
        private static string ConvertStringToHex(string input)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(input);
            List<string> hexList = new List<string> { $"0x{asciiBytes.Length:X2}" }; // 长度字节

            foreach (byte b in asciiBytes)
            {
                hexList.Add($"0x{b:X2}");
            }
            return string.Join(" ", hexList);
        }

        // 2. 将时间转换为 IPMI 要求的 3 字节 Hex 字符串 (小端序，基于 1996-01-01 08:00:00)
        // 例如输入 "2024-05-20 10:30:00" -> "0x48 0x18 0x0E"
        private static string ConvertTimeToHex(string inputTime)
        {
            var baseTime = new DateTime(1996, 1, 1, 8, 0, 0, DateTimeKind.Local);
            var targetTime = DateTime.ParseExact(inputTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            long offsetMinutes = (long)(targetTime - baseTime).TotalMinutes;

            // 直接拆分成 3 个字节的 Hex 字符串
            string byte1 = $"0x{offsetMinutes & 0xFF:X2}";
            string byte2 = $"0x{(offsetMinutes >> 8) & 0xFF:X2}";
            string byte3 = $"0x{(offsetMinutes >> 16) & 0xFF:X2}";

            return $"{byte1} {byte2} {byte3}";
        }

        public static string fruWriteHead = "raw 0x30 0x90 0x04 0x00";
        // 3. 核心：根据下拉列表的选择，动态生成 IPMI Raw 命令字符串
        public static List<string> GenerateCommands(string selectedKey, string userInput)
        {
            var commands = new List<string>();

            if (!FruTable.TryGetValue(selectedKey, out var field))
            {
                throw new ArgumentException($"未知的 FRU 字段: {selectedKey}");
            }

            // 基础协议头字符串
            string baseCmd = $"{fruWriteHead} 0x{field.Area:X2} 0x{field.Offset:X2} 0x00";

            if (field.IsString)
            {
                // 字符串类型: 拼接长度 + 内容 Hex
                baseCmd += $" {ConvertStringToHex(userInput)}";
            }
            else
            {
                // 非字符串类型 (特殊处理)
                if (field.Offset == 0x00 && field.Area == 0x2)
                {
                    // 主板制造日期 (Board Mfg Date)
                    baseCmd += $" {ConvertTimeToHex(userInput)}";
                }
                else
                {
                    // 机箱类型 (Chassis Type) -> 固定 1 字节
                    baseCmd += $" 0x{field.ExtraByte:X2}";
                }
            }

            commands.Add($"{baseCmd}");

            // 处理 Product Serial 需要写两次的特殊情况 (0x3 0x4 和 0x6 0x3)
            if (field.SecondaryArea.HasValue && field.SecondaryOffset.HasValue)
            {
                string secCmd = $"{fruWriteHead} 0x{field.SecondaryArea.Value:X2} 0x{field.SecondaryOffset.Value:X2} 0x00";
                secCmd += $" {ConvertStringToHex(userInput)}";
                commands.Add($"{secCmd}");
            }

            return commands;
        }

    }    
}