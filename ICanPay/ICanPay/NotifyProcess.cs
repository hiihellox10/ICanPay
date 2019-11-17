using ICanPay.Providers;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Xml;

namespace ICanPay
{
    /// <summary>
    /// 网关通知的处理类，通过对返回数据的分析识别网关类型
    /// </summary>
    internal static class NotifyProcess
    {

        #region 私有字段

        // 需要验证的参数名称数组，用于识别不同的网关类型。
        // 检查是否在发回的数据中，需要保证参数名称跟其他各个网关验证的参数名称不重复。
        // 建议使用网关中返回的不为空的参数名，并使用尽可能多的参数名。
        private static string[] _yeepayGatewayVerifyParmaNames = { "r0_Cmd", "r1_Code", "r2_TrxId", "r3_Amt", "r4_Cur", "r5_Pid", "r6_Order" };
        private static string[] _tenpayGatewayVerifyParmaNames = { "trade_mode", "trade_state", "transaction_id", "notify_id", "total_fee", "fee_type" };
        private static string[] _alipayGatewayVerifyParmaNames = { "notify_type", "notify_id", "notify_time", "sign", "sign_type" };
        private static string[] _weChatPayGatewayVerifyParmaNames = { "return_code", "appid", "mch_id", "nonce_str", "result_code" };

        #endregion


        #region 方法

        /// <summary>
        /// 验证网关的类型
        /// </summary>
        public static GatewayBase GetGateway()
        {
            Dictionary<string, GatewayParameter> gatewayParameterList = ReadNotifyData();
            if (IsAlipayGateway(gatewayParameterList))
            {
                return new AlipayGateway(gatewayParameterList);
            }

            if (IsWeChatPayGateway(gatewayParameterList))
            {
                return new WeChatPayGataway(gatewayParameterList);
            }

            if (IsTenpayGateway(gatewayParameterList))
            {
                return new TenpayGateway(gatewayParameterList);
            }

            if (IsYeepayGateway(gatewayParameterList))
            {
                return new YeepayGateway(gatewayParameterList);
            }

            return new NullGateway(gatewayParameterList);
        }


        /// <summary>
        /// 验证是否是易宝网关
        /// </summary>
        private static bool IsYeepayGateway(Dictionary<string, GatewayParameter> gatewayParameterList)
        {
            return ExistParameter(_yeepayGatewayVerifyParmaNames, gatewayParameterList);
        }


        /// <summary>
        /// 是否是财付通网关
        /// </summary>
        private static bool IsTenpayGateway(Dictionary<string, GatewayParameter> gatewayParameterList)
        {
            return ExistParameter(_tenpayGatewayVerifyParmaNames, gatewayParameterList);
        }


        /// <summary>
        /// 是否是支付宝网关
        /// </summary>
        private static bool IsAlipayGateway(Dictionary<string, GatewayParameter> gatewayParameterList)
        {
            return ExistParameter(_alipayGatewayVerifyParmaNames, gatewayParameterList);
        }


        /// <summary>
        /// 是否是微信支付网关
        /// </summary>
        private static bool IsWeChatPayGateway(Dictionary<string, GatewayParameter> gatewayParameterList)
        {
            return ExistParameter(_weChatPayGatewayVerifyParmaNames, gatewayParameterList);
        }


        /// <summary>
        /// 网关参数数据项中是否存在指定的所有参数名
        /// </summary>
        /// <param name="parmaName">参数名数组</param>
        /// <param name="gatewayParameterList">数据项</param>
        public static bool ExistParameter(string[] parmaName, Dictionary<string, GatewayParameter> gatewayParameterList)
        {
            int compareCount = 0;
            foreach (string item in parmaName)
            {
                if (gatewayParameterList.ContainsKey(item))
                {
                    compareCount++;
                }
            }

            if (compareCount == parmaName.Length)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 读取网关发回的数据。
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, GatewayParameter> ReadNotifyData()
        {
            Dictionary<string, GatewayParameter> gatewayParameterList = new Dictionary<string, GatewayParameter>();
            ReadQueryString(gatewayParameterList);
            ReadForm(gatewayParameterList);
            ReadBody(gatewayParameterList);

            return gatewayParameterList;
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterList">保存网关参数的集合</param>
        /// <param name="gatewayParameterName">网关参数的名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        /// <param name="httpMethod">网关参数的请求方法的类型</param>
        private static void SetGatewayParameterValue(Dictionary<string, GatewayParameter> gatewayParameterList, string gatewayParameterName,
            string gatewayParameterValue, HttpMethod httpMethod)
        {

            if (gatewayParameterList.ContainsKey(gatewayParameterName))
            {
                GatewayParameter gatewayParameter = gatewayParameterList[gatewayParameterName];
                if (string.Compare(gatewayParameter.Value, gatewayParameterValue) != 0 ||
                                   gatewayParameter.HttpMethod != httpMethod)
                {
                    gatewayParameter.HttpMethod = httpMethod;
                    gatewayParameter.Value = gatewayParameterValue;
                }
            }
            else
            {
                GatewayParameter gatewayParameter = new GatewayParameter(gatewayParameterName, gatewayParameterValue, httpMethod);
                gatewayParameterList.Add(gatewayParameterName, gatewayParameter);
            }
        }


        /// <summary>
        /// 读取GET提交的查询字符串中的数据
        /// </summary>
        /// <param name="gatewayParameterList">网关通知的参数列表</param>
        private static void ReadQueryString(Dictionary<string, GatewayParameter> gatewayParameterList)
        {
            NameValueCollection queryString = HttpContext.Current.Request.QueryString;
            foreach (string item in queryString.AllKeys)
            {
                SetGatewayParameterValue(gatewayParameterList, item, queryString[item], HttpMethod.Get);
            }
        }


        /// <summary>
        /// 读取POST提交的Form表单的数据
        /// </summary>
        /// <param name="gatewayParameterList">网关通知的参数列表</param>
        private static void ReadForm(Dictionary<string, GatewayParameter> gatewayParameterList)
        {
            NameValueCollection form = HttpContext.Current.Request.Form;
            foreach (string item in form.AllKeys)
            {
                SetGatewayParameterValue(gatewayParameterList, item, form[item], HttpMethod.Post);
            }
        }


        /// <summary>
        /// 读取Body的内容
        /// </summary>
        /// <param name="gatewayParameterList">网关通知的参数列表</param>
        private static void ReadBody(Dictionary<string, GatewayParameter> gatewayParameterList)
        {
            ReadWeChatPayXml(gatewayParameterList);
        }


        /// <summary>
        /// 读取微信支付的通知
        /// </summary>
        /// <param name="gatewayParameterList">网关通知的参数列表</param>
        private static void ReadWeChatPayXml(Dictionary<string, GatewayParameter> gatewayParameterList)
        {
            if (IsWeChatPayNotify())
            {
                XmlDocument xmlDocument = Utility.CreateXmlSafeDocument();
                try
                {
                    StreamReader reader = new StreamReader(HttpContext.Current.Request.InputStream);
                    xmlDocument.LoadXml(reader.ReadToEnd());
                }
                catch (XmlException) { }

                if (xmlDocument.FirstChild != null && xmlDocument.FirstChild.ChildNodes != null)
                {
                    foreach (XmlNode item in xmlDocument.FirstChild.ChildNodes)
                    {
                        SetGatewayParameterValue(gatewayParameterList, item.Name, item.InnerText, HttpMethod.Post);
                    }
                }
            }
        }


        /// <summary>
        /// 是否是微信支付的通知
        /// </summary>
        /// <returns></returns>
        private static bool IsWeChatPayNotify()
        {
            if (string.Compare(HttpContext.Current.Request.RequestType, "POST") == 0 &&
                string.Compare(HttpContext.Current.Request.ContentType, "text/xml") == 0 &&
                string.Compare(HttpContext.Current.Request.UserAgent, "Mozilla/4.0") == 0)
            {
                return true;
            }

            return false;
        }

        #endregion

    }
}
