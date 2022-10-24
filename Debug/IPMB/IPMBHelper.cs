using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug.IPMB {
    static class IPMBHelper {
        /// <summary>
        /// 进制转换
        /// </summary>
        /// <param name="hexStr">ini 中的原始 hex 数据</param>
        /// <param name="shiftCount">根据标准协议，进行移位的个数</param>
        /// <returns></returns>
        public static string Radix(string hexStr, int shiftCount, string LUMStr) {
            try {
                byte valDec = (byte)Convert.ToInt32(hexStr, 16);
                byte lum = (byte)Convert.ToInt32(LUMStr, 16);
                valDec = (byte)(valDec << shiftCount);
                valDec += lum;
                string resHexStr = valDec.ToString("X");
                return resHexStr;
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
                return "0";
            }
        }
    }
}
