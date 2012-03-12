using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ICanPay
{
    /// <summary>
    /// 支付的相关操作
    /// </summary>
    public static class Utility
    {

        #region 字段

        const string numericRegexString = "^[0-9]*$";
        const string emailRegexString = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        #endregion


        #region 方法

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
            return Regex.IsMatch(text, numericRegexString);
        }


        /// <summary>
        /// 验证是否是日期格式。仅用于验证yyyyMMdd格式日期。
        /// </summary>
        /// <param name="text">需要验证的字符串</param>
        public static bool IsDate(string text)
        {
            if (text.Length != 8)
            {
                return false;
            }

            // 设置日期格式
            text = string.Format("{0}-{1}-{2}", text.Substring(0, 4), text.Substring(4, 2), text.Substring(6, 2));
            DateTime tempDateTime;
            return DateTime.TryParse(text, out tempDateTime);
        }


        /// <summary>
        /// 是否是正确格式的Email地址
        /// </summary>
        /// <param name="emailAddress">Email地址</param>
        public static bool IsEmail(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                return false;
            }

            return Regex.IsMatch(emailAddress, emailRegexString);
        }



        /// <summary>
        /// 网关参数数据项中是否存在指定的所有参数名
        /// </summary>
        /// <param name="parmaName">参数名数组</param>
        /// <param name="gatewayParameterData">数据项</param>
        public static bool ExistParameter(string[] parmaName, ICollection<GatewayParameter> gatewayParameterData)
        {
            int compareCount = 0;
            foreach (string s in parmaName)
            {
                foreach (GatewayParameter item in gatewayParameterData)
                {
                    if (string.Compare(s, item.ParameterName) == 0)
                    {
                        compareCount++;
                        break;
                    }
                }
            }

            if (compareCount == parmaName.Length)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 读取网关发回的数据。Get方式传入QueryString的值均为未解码
        /// </summary>
        public static ICollection<GatewayParameter> ReadNotifyData()
        {
            List<GatewayParameter> gatewayParameterList = new List<GatewayParameter>();
            NameValueCollection coll;
            string[] keys;

            // 读取通过Get传入的值
            coll = HttpContext.Current.Request.QueryString;
            keys = coll.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                GatewayParameter param = new GatewayParameter(keys[i], coll[keys[i]], GatewayParameterType.Get);
                gatewayParameterList.Add(param);
            }

            // 读取通过Post传入的值
            coll = HttpContext.Current.Request.Form;
            keys = coll.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                GatewayParameter param = new GatewayParameter(keys[i], coll[keys[i]], GatewayParameterType.Post);
                if (gatewayParameterList.Exists(p => p.ParameterName == param.ParameterName &&
                                                p.ParameterValue == param.ParameterValue))
                {
                    param.ParameterType = GatewayParameterType.Both;
                }

                gatewayParameterList.Add(param);
            }

            return gatewayParameterList;
        }

        #endregion

    }
}
