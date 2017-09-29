using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace ICanPay.Core
{
    /// <summary>
    /// 支付的相关操作
    /// </summary>
    public static class Utility
    {

        #region 方法

        /// <summary>
        /// 获得字符串的MD5值，MD5值为大写
        /// </summary>
        /// <param name="text">字符串</param>
        public static string GetMD5(string text)
        {
            return GetMD5(text, Encoding.UTF8);
        }


        /// <summary>
        /// 获得字符串的MD5值，MD5值为大写
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="textEncoding">字符串编码</param>
        /// <returns></returns>
        public static string GetMD5(string text, Encoding textEncoding)
        {
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(textEncoding.GetBytes(text));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("X2"));
            }

            return stringBuilder.ToString();
        }


        /// <summary>
        /// 读取网页，返回网页内容
        /// </summary>
        /// <param name="pageUrl">网页URL</param>
        /// <returns></returns>
        public static string ReadPage(string pageUrl)
        {
            return ReadPage(pageUrl, Encoding.UTF8);
        }


        /// <summary>
        /// 读取网页，返回网页内容
        /// </summary>
        /// <param name="pageUrl">网页URL</param>
        /// <param name="pageEncoding">网页编码</param>
        /// <returns></returns>
        public static string ReadPage(string pageUrl, Encoding pageEncoding)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pageUrl);
            request.Method = "GET";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), pageEncoding))
                    {
                        if (reader != null)
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
                request.Abort();
            }

            return string.Empty;
        }

        #endregion

    }
}
