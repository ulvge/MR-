using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug.misc
{
    static class DataCheck
    {
        /// <summary>
        /// 累加
        /// </summary>
        /// <param name="bts"></param>
        /// <returns></returns>
        public static byte CheckAdd(byte[] bts)
        {
            byte sum = 0;
            for (int i = 0; i < bts.Length; i++)
            {
                sum += bts[i];
            }
            return sum;
        }
        public static byte CheckOR(byte startVal, byte[] bts)
        {
            byte sum = startVal;
            for (int i = 0; i < bts.Length; i++)
            {
                sum ^= bts[i];
            }
            return sum;
        }
        /// <summary>
        /// CRC maxim
        /// https://crccalc.com/
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte CheckCRC8(byte startVal, byte[] buffer)
        {
            byte crc = startVal;
            for (int j = 0; j < buffer.Length; j++)
            {
                crc ^= buffer[j];
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x01) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0x8c;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }
        public static void DataCheckMain()
        {
            //6D
            byte[] ori1 = new byte[] { 0x01, 0x09, 0x00, 0x02, 0xFF, 0xFF, 0x01, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

            //80
            byte[] ori2 = new byte[] { 0x02, 0x0A, 0x00, 0x02, 0xFF, 0xFF, 0x01, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

            //2C
            byte[] ori3 = new byte[] { 0x03, 0x0B, 0x00, 0x02, 0xFF, 0xFF, 0x01, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

            byte res;

            res = CheckCRC8(0, ori1);
            Console.WriteLine("CRC 6D  " + res.ToString("X2"));

            res = CheckCRC8(0, ori2);
            Console.WriteLine("CRC 80  " + res.ToString("X2"));

            res = CheckCRC8(0, ori3);
            Console.WriteLine("CRC 2C  " + res.ToString("X2"));
        }
    }
}
