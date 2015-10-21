using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CarConsole.ClassLib
{
    class HexConvert
    {

        /// <summary>
        /// 字节数组显示为16进制字符串 0xae00cf => "AE 00 CF"
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>16进制字符串</returns>
        public static string BytesToHexString(byte[] bytes) 
        {
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                    strB.Append(" ");
                }
                hexString = strB.ToString();
            }
            return hexString;
        }

        /// <summary>
        /// 字符串显示为16进制字符串 “110” => “49 49 48”
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string StringToHexString(string s)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                    strB.Append(" ");
                }
                hexString = strB.ToString();
            }
            return hexString;
        }

        /// <summary>
        /// 16进制格式的string 转成byte[]，例如, "ae00cf"转换成0xae00cf，(长度缩减一半；"3031" 4个 转成new byte[]{ 0x30, 0x31} 2个)
        /// </summary>
        /// <param name="hexString">16进制字符串</param>
        /// <param name="discarded"></param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(string hexString, out int discarded)
        {
            discarded = 0;
            string newString = "";
            char c;
            // 删除非a-f A-F 0-9 的字符
            for (int i = 0; i < hexString.Length; i++)
            {
                c = hexString[i];
                if (IsHexDigit(c))
                    newString += c;
                else
                    discarded++;
            }
            // if odd number of characters, discard last character
            if (newString.Length % 2 != 0)
            {
                discarded++;
                newString = newString.Substring(0, newString.Length - 1);
            }

            int byteLength = newString.Length / 2;
            byte[] bytes = new byte[byteLength];
            string hex;
            int j = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                hex = new String(new Char[] { newString[j], newString[j + 1] });
                bytes[i] = HexToByte(hex);
                j = j + 2;
            }
            return bytes;
        }

        private static bool IsHexDigit(char c)
        {
            return new Regex("[0-9a-fA-F]").IsMatch(c.ToString());
        }

        private static byte HexToByte(string hex)
        {
            return byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }
    }
}
