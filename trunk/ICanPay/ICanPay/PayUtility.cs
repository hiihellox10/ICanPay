using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ICanPay
{
    /// <summary>
    /// 支付的相关操作
    /// </summary>
    internal static class PayUtility
    {
        /// <summary>
        /// 获得字符串的MD5值
        /// </summary>
        /// <param name="text">需要获得MD5值的支付串</param>
        public static string MD5(string text)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(text, "md5");
        }


        /// <summary>
        /// 字符串是否全是数字，全由数字组成返回true
        /// </summary>
        /// <param name="text">需要验证的字符串</param>
        public static bool IsNumeric(string text)
        {
            return Regex.IsMatch(text, "^[0-9]*$");
        }


        /// <summary>
        /// 根据参数顺序，获取其他数据项中的值
        /// </summary>
        /// <param name="parma">参数顺序</param>
        /// <param name="otherData">他数据项</param>
        public static string GetOtherDataValue(string[] parma, Dictionary<string, string> otherData)
        {
            StringBuilder value = new StringBuilder();

            foreach (string s in parma)
            {
                if (otherData.ContainsKey(s))
                {
                    value.Append(otherData[s]);
                }
            }

            return value.ToString();
        }

        /// <summary>
        /// 数据项中是否包含参数中的所有键
        /// </summary>
        /// <param name="parma">参数</param>
        /// <param name="otherData">数据项</param>
        public static bool ContainsKey(string[] parma, Dictionary<string, string> otherData)
        {
            foreach (string s in parma)
            {
                if (!otherData.ContainsKey(s))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
