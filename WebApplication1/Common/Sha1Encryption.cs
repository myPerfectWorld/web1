using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class Sha1Encryption
    {
        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <returns>返回40位UTF8 大写</returns>
        public static string SHA1(string content)
        {
            return SHA1(content, Encoding.UTF8).ToLower();
        }
        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <param name="encode">指定加密编码</param>
        /// <returns>返回40位大写字符串</returns>
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashText"></param>
        /// <param name="EncryptKey"></param>
        /// <returns></returns>
        public static string hmacsha1hasher(string hashText, string EncryptKey)
        {
            byte[] HmacKey = System.Text.Encoding.UTF8.GetBytes(EncryptKey);
            byte[] HmacData = System.Text.Encoding.UTF8.GetBytes(hashText);
            HMACSHA1 Hmac = new HMACSHA1(HmacKey);
            CryptoStream cs = new CryptoStream(Stream.Null, Hmac, CryptoStreamMode.Write);
            cs.Write(HmacData, 0, HmacData.Length);
            cs.Close();
            byte[] Result = Hmac.Hash;
            StringBuilder sb = new StringBuilder();
            foreach (byte b in Result)
            {
                // 以十六进制格式格式化
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EncryptText"></param>
        /// <param name="EncryptKey"></param>
        /// <returns></returns>
        public static string hmacsha1(string EncryptText, string EncryptKey)
        {
            HMACSHA1 myHMACSHA1 = new HMACSHA1(Encoding.Default.GetBytes(EncryptKey));
            byte[] RstRes = myHMACSHA1.ComputeHash(Encoding.Default.GetBytes(EncryptText));

            StringBuilder EnText = new StringBuilder();
            foreach (byte Byte in RstRes)
            {
                EnText.AppendFormat("{0:x2}", Byte);
            }
            return EnText.ToString();
        }
    }
}
