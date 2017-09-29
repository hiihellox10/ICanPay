using ICanPay.Providers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        static string[] yeepayGatewayVerifyParmaNames = { "r0_Cmd", "r1_Code", "r2_TrxId", "r3_Amt", "r4_Cur", "r5_Pid", "r6_Order" };
        static string[] tenpayGatewayVerifyParmaNames = { "trade_mode", "trade_state", "transaction_id", "notify_id", "total_fee", "fee_type" };
        static string[] alipayGatewayVerifyParmaNames = { "notify_type", "notify_id", "notify_time", "sign", "sign_type" };
        static string[] weixinpayGatewayVerifyParmaNames = { "return_code", "appid", "mch_id", "nonce_str", "result_code" };

        #endregion


        #region 方法

        /// <summary>
        /// 验证网关的类型
        /// </summary>
        public static GatewayBase GetGateway()
        {
            List<GatewayParameter> gatewayParameterData = ReadNotifyData();
            if (IsAlipayGateway(gatewayParameterData))
            {
                return new AlipayGateway(gatewayParameterData);
            }

            if (IsWeixinpayGateway(gatewayParameterData))
            {
                return new WeChatPayGataway(gatewayParameterData);
            }

            if (IsTenpayGateway(gatewayParameterData))
            {
                return new TenpayGateway(gatewayParameterData);
            }

            if (IsYeepayGateway(gatewayParameterData))
            {
                return new YeepayGateway(gatewayParameterData);
            }

            return new NullGateway(gatewayParameterData);
        }


        /// <summary>
        /// 验证是否是易宝网关
        /// </summary>
        private static bool IsYeepayGateway(List<GatewayParameter> gatewayParameterData)
        {
            return ExistParameter(yeepayGatewayVerifyParmaNames, gatewayParameterData);
        }


        /// <summary>
        /// 是否是财付通网关
        /// </summary>
        private static bool IsTenpayGateway(List<GatewayParameter> gatewayParameterData)
        {
            return ExistParameter(tenpayGatewayVerifyParmaNames, gatewayParameterData);
        }


        /// <summary>
        /// 是否是支付宝网关
        /// </summary>
        private static bool IsAlipayGateway(List<GatewayParameter> gatewayParameterData)
        {
            return ExistParameter(alipayGatewayVerifyParmaNames, gatewayParameterData);
        }


        /// <summary>
        /// 是否是微信支付网关
        /// </summary>
        private static bool IsWeixinpayGateway(List<GatewayParameter> gatewayParameterData)
        {
            return ExistParameter(weixinpayGatewayVerifyParmaNames, gatewayParameterData);
        }


        /// <summary>
        /// 网关参数数据项中是否存在指定的所有参数名
        /// </summary>
        /// <param name="parmaName">参数名数组</param>
        /// <param name="gatewayParameterData">数据项</param>
        public static bool ExistParameter(string[] parmaName, List<GatewayParameter> gatewayParameterData)
        {
            int compareCount = 0;
            foreach (string item in parmaName)
            {
                if (gatewayParameterData.Exists(p => string.Compare(item, p.Name) == 0))
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
        /// 读取网关发回的数据。Get方式传入QueryString的值均为未解码
        /// </summary>
        /// <returns></returns>
        public static List<GatewayParameter> ReadNotifyData()
        {
            List<GatewayParameter> gatewayParameters = new List<GatewayParameter>();
            ReadQueryString(gatewayParameters);
            ReadForm(gatewayParameters);
            ReadWeixinpayXml(gatewayParameters);

            return gatewayParameters;
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterList">保存网关参数的集合</param>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        /// <param name="gatewayParameterRequestMethod">网关的参数的请求方式的类型</param>
        private static void SetGatewayParameterValue(List<GatewayParameter> gatewayParameterList, string gatewayParameterName,
            string gatewayParameterValue, GatewayParameterRequestMethod gatewayParameterRequestMethod)
        {
            GatewayParameter existsParam = gatewayParameterList.SingleOrDefault(p => string.Compare(p.Name, gatewayParameterName) == 0);
            if (existsParam == null)
            {
                GatewayParameter param = new GatewayParameter(gatewayParameterName, gatewayParameterValue, gatewayParameterRequestMethod);
                gatewayParameterList.Add(param);
            }
            else
            {
                if (string.Compare(existsParam.Value, gatewayParameterValue) == 0)
                {
                    existsParam.RequestMethod = existsParam.RequestMethod | gatewayParameterRequestMethod;
                }
                else
                {
                    existsParam.RequestMethod = gatewayParameterRequestMethod;
                    existsParam.Value = gatewayParameterValue;
                }
            }
        }


        /// <summary>
        /// 读取GET提交的查询字符串中的数据
        /// </summary>
        /// <param name="gatewayParameterList">网关通知的参数列表</param>
        private static void ReadQueryString(List<GatewayParameter> gatewayParameterList)
        {
#if NET35
            var queryString = HttpContext.Current.Request.QueryString;
            var allKeys = queryString.AllKeys;
#elif NETSTANDARD2_0
            var queryString = HttpContext.Current.Request.Query;
            var allKeys = queryString.Keys;
#endif

            foreach(var item in allKeys)
            {
                SetGatewayParameterValue(gatewayParameterList, item, queryString[item], GatewayParameterRequestMethod.Get);
            }
        }


        /// <summary>
        /// 读取POST提交的Form表单的数据
        /// </summary>
        /// <param name="gatewayParameterList">网关通知的参数列表</param>
        private static void ReadForm(List<GatewayParameter> gatewayParameterList)
        {
            var form = HttpContext.Current.Request.Form;
#if NET35
            var allKeys = form.AllKeys;
#elif NETSTANDARD2_0
            var allKeys = form.Keys;
#endif

            foreach (var item in allKeys)
            {
                SetGatewayParameterValue(gatewayParameterList, item, form[item], GatewayParameterRequestMethod.Get);
            }
        }


        /// <summary>
        /// 读取微信支付的通知
        /// </summary>
        /// <param name="gatewayParameterList">网关通知的参数列表</param>
        private static void ReadWeixinpayXml(List<GatewayParameter> gatewayParameterList)
        {
            if (IsWeixinpayNotify())
            {
                XmlDocument xmlDocument = new XmlDocument();
                try
                {
#if NET35
                    StreamReader reader = new StreamReader(HttpContext.Current.Request.InputStream);
#elif NETSTANDARD2_0
                    StreamReader reader = new StreamReader(HttpContext.Current.Request.Body);
#endif
                    xmlDocument.LoadXml(reader.ReadToEnd());
                }
                catch (XmlException) { }

                if (xmlDocument.FirstChild != null && xmlDocument.FirstChild.ChildNodes != null)
                {
                    foreach (XmlNode item in xmlDocument.FirstChild.ChildNodes)
                    {
                        SetGatewayParameterValue(gatewayParameterList, item.Name, item.InnerText, GatewayParameterRequestMethod.Post);
                    }
                }
            }
        }


        /// <summary>
        /// 是否是微信支付的通知
        /// </summary>
        /// <returns></returns>
        private static bool IsWeixinpayNotify()
        {
#if NET35
            string requestType = HttpContext.Current.Request.RequestType;
            string userAgent = HttpContext.Current.Request.UserAgent;
#elif NETSTANDARD2_0
            string requestType = HttpContext.Current.Request.Headers["RequestType"];
            string userAgent = HttpContext.Current.Request.Headers["UserAgent"];
#endif

            string contentType = HttpContext.Current.Request.ContentType;

            if (string.Compare(requestType, "POST") == 0 &&
                string.Compare(contentType, "text/xml") == 0 &&
                string.Compare(userAgent, "Mozilla/4.0") == 0)
            {
                return true;
            }

            return false;
        }

#endregion

    }
}
