using System;

using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Com.Ctrip.Framework.Apollo.Util
{
  public  class AthenaEncryptHelper
    {
        /// <summary>
        /// 获取AES密钥
        /// </summary>
        private static string getkey1()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(typeof(UInt16).Name.First())
                .Append(typeof(NetTcpStyleUriParser).Name.First())
                .Append(typeof(IFormattable).Name.First())
                .Append(typeof(TimeSpan).Name.First())
                .Append(typeof(object).Name.First())
                .Append(typeof(Core.Utils.Properties).Name.First())
                .Append(".")
                .Append(typeof(Char).Name.First())
                .Append(typeof(object).Name.First())
                .Append(typeof(Math).Name.First())
                .Append("!")
                .Append("@")
                .Append("#")
                .Append("$")
                .Append("%").Append("^"); ;
            return builder.ToString().ToLower();
        }
        /// <summary>
        /// 十六进制字符串转化为字节数值
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns></returns>
        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        /// <summary>
        /// AES 加密
        /// </summary>
        /// <param name="content">需要加密的内容</param>
        /// <returns></returns>
        public static string EncryptAES(string content)
        {
            try
            {
                Encoding encoding = Encoding.UTF8;

                byte[] plainBytes = encoding.GetBytes(content);
                byte[] keyBytes = encoding.GetBytes(getkey1());
                Aes kgen = Aes.Create("AES");
                kgen.Mode = CipherMode.ECB;
                kgen.Key = keyBytes;
                ICryptoTransform cTransform = kgen.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                //  return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                return ByteToHex(resultArray);
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 二进制转换为16进制
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHex(byte[] bytes)
        {
            // 第四步：把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                string hex = bytes[i].ToString("X");
                if (hex.Length == 1)
                {
                    result.Append("0");
                }
                result.Append(hex);
            }
            return result.ToString();
        }
        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="content">需要加密的内容</param>
        /// <returns></returns>
        public static string DecryptAES(string content)
        {
            try
            {
                Encoding encoding = Encoding.UTF8;

                byte[] plainBytes = strToToHexByte(content); //Convert.FromBase64String(content);
                byte[] keyBytes = encoding.GetBytes(getkey1());
                Aes kgen = Aes.Create("AES");
                kgen.Mode = CipherMode.ECB;
                kgen.Key = keyBytes;
                ICryptoTransform cTransform = kgen.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                return encoding.GetString(resultArray);
            }
            catch
            {
                return "";
            }
        }

    }
}
