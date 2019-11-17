using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace ICanPay.Providers
{
    /// <summary>
    /// 微信支付网关
    /// </summary>
    /// <remarks>
    /// 使用模式二实现微信支付
    /// </remarks>
    public sealed class WeChatPayGataway : GatewayBase, IPaymentQRCode, IQueryNow
    {

        #region 私有字段

        private const string PayGatewayUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        private const string QueryGatewayUrl = "https://api.mch.weixin.qq.com/pay/orderquery";

        #endregion


        #region 构造函数

        /// <summary>
        /// 初始化微信支付网关
        /// </summary>
        public WeChatPayGataway()
        {
        }


        /// <summary>
        /// 初始化微信支付网关
        /// </summary>
        /// <param name="gatewayParameterList">网关通知的数据集合</param>
        public WeChatPayGataway(Dictionary<string, GatewayParameter> gatewayParameterList)
            : base(gatewayParameterList)
        {
        }

        #endregion


        public override GatewayType GatewayType
        {
            get { return GatewayType.WeChatPay; }
        }

        public override PaymentNotifyMethod PaymentNotifyMethod
        {
            get { return PaymentNotifyMethod.ServerNotify; }
        }

        private WeChatPayMerchant WeChatPayMerchant
        {
            get
            {
                WeChatPayMerchant weChatPayMerchant = Merchant as WeChatPayMerchant;
                if (weChatPayMerchant == null)
                {
                    throw new ArgumentException("商户数据类型不是微信支付商户，请将 Merchant 属性设置为 WeChatPayMerchant 类型", "Merchant");
                }

                return weChatPayMerchant;
            }
        }
            

        public override bool ValidateNotify()
        {
            if (IsSuccessResult())
            {
                ReadNotifyOrder();
                return true;
            }

            return false;
        }

        public string GetPaymentQRCodeContent()
        {
            string createOrderXml = CreateOrder();
            return GetWeChatPayUrl(createOrderXml);
        }

        private string CreateOrder()
        {
            InitPaymentOrderParameter();
            string paymentOrderXml = ConvertGatewayParameterDataToXml();

            return PostRequest(paymentOrderXml, PayGatewayUrl);
        }

        public bool QueryNow()
        {
            string queryOrderResultXml = QueryOrder();
            return CheckQueryResult(queryOrderResultXml);
        }

        private string QueryOrder()
        {
            InitQueryOrderParameter();
            string queryOrderXml = ConvertGatewayParameterDataToXml();

            return PostRequest(queryOrderXml, QueryGatewayUrl);
        }


        /// <summary>
        /// 初始化支付订单的参数
        /// </summary>
        private void InitPaymentOrderParameter()
        {
            SetGatewayParameterValue("mch_id", Merchant.UserName);
            SetGatewayParameterValue("appid", WeChatPayMerchant.AppId);
            SetGatewayParameterValue("nonce_str", GenerateNonceString());
            SetGatewayParameterValue("body", Order.Subject);
            SetGatewayParameterValue("out_trade_no", Order.Id);
            SetGatewayParameterValue("total_fee", Order.Amount * 100);
            SetGatewayParameterValue("spbill_create_ip", "127.0.0.1");
            SetGatewayParameterValue("notify_url", Merchant.NotifyUrl);
            SetGatewayParameterValue("trade_type", "NATIVE");
            SetGatewayParameterValue("product_id", Order.Id);
            SetGatewayParameterValue("sign", GetSign());    // 签名需要在最后设置，以免缺少参数。
        }


        /// <summary>
        /// 读取通知中的订单金额、订单编号
        /// </summary>
        private void ReadNotifyOrder()
        {
            Order.Id = GetGatewayParameterValue("out_trade_no");
            Order.Amount = GetGatewayParameterValue<int>("total_fee") * 0.01;
        }


        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <returns></returns>
        private string GenerateNonceString()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }


        /// <summary>
        /// 将网关数据转换成XML
        /// </summary>
        /// <returns></returns>
        private string ConvertGatewayParameterDataToXml()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            StringBuilder xmlBuilder = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(xmlBuilder, settings))
            {
                writer.WriteStartElement("xml");
                foreach (var item in GetSortedGatewayParameter())
                {
                    writer.WriteElementString(item.Key, item.Value);
                }
                writer.WriteEndElement();
                writer.Flush();
            }

            return xmlBuilder.ToString();
        }


        /// <summary>
        /// 获得签名
        /// </summary>
        /// <returns></returns>
        private string GetSign()
        {
            StringBuilder signBuilder = new StringBuilder();
            foreach (var item in GetSortedGatewayParameter())
            {
                // 空值的参数与sign参数不参与签名
                if (!string.IsNullOrEmpty(item.Value) && string.Compare("sign", item.Key) != 0)
                {
                    signBuilder.AppendFormat("{0}={1}&", item.Key, item.Value);
                }
            }

            signBuilder.Append("key=" + Merchant.Key);
            return Utility.GetMD5(signBuilder.ToString());
        }


        /// <summary>
        /// 提交请求
        /// </summary>
        /// <param name="requestXml">请求的XML内容</param>
        /// <param name="gatewayUrl">网关URL</param>
        /// <returns></returns>
        private string PostRequest(string requestXml, string gatewayUrl)
        {
            byte[] dataByte = Encoding.UTF8.GetBytes(requestXml);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(gatewayUrl);
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.ContentLength = dataByte.Length;

            try
            {
                using (Stream outStream = request.GetRequestStream())
                {
                    outStream.Write(dataByte, 0, dataByte.Length);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
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


        /// <summary>
        /// 获得微信支付的URL
        /// </summary>
        /// <param name="resultXml">创建订单返回的数据</param>
        /// <returns></returns>
        private string GetWeChatPayUrl(string resultXml)
        {
            // 需要先清除之前创建订单的参数，否则会对接收到的参数造成干扰。
            ClearAllGatewayParameter();
            ReadResultXml(resultXml);
            if (IsSuccessResult())
            {
                return GetGatewayParameterValue("code_url");
            }

            return string.Empty;
        }

        /// <summary>
        /// 读取结果的XML
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private void ReadResultXml(string xml)
        {
            XmlDocument xmlDocument = Utility.CreateXmlSafeDocument();
            try
            {
                xmlDocument.LoadXml(xml);
            }
            catch (XmlException) { }
            
            if (xmlDocument.FirstChild != null && xmlDocument.FirstChild.ChildNodes != null)
            {
                foreach (XmlNode item in xmlDocument.FirstChild.ChildNodes)
                {
                    SetGatewayParameterValue(item.Name, item.InnerText);
                }
            }
        }


        /// <summary>
        /// 是否是已成功支付的支付通知
        /// </summary>
        /// <returns></returns>
        private bool IsSuccessResult()
        {
            if (CompareGatewayParameterValue("return_code", "SUCCESS")&&
                CompareGatewayParameterValue("result_code", "SUCCESS")&&
                CompareGatewayParameterValue("sign", GetSign()))
            {
                return true;
            }

            return false;
        }
        

        /// <summary>
        /// 检查查询结果
        /// </summary>
        /// <param name="resultXml">查询结果的XML</param>
        /// <returns></returns>
        private bool CheckQueryResult(string resultXml)
        {
            // 需要先清除之前查询订单的参数，否则会对接收到的参数造成干扰。
            ClearAllGatewayParameter();
            ReadResultXml(resultXml);
            if (IsSuccessResult())
            {
               if(CompareGatewayParameterValue("out_trade_no", Order.Id) &&
                  CompareGatewayParameterValue("total_fee", Order.Amount * 100))
               {
                   return true;
               }
            }

            return false;
        }

          /// <summary>
        /// 初始化查询订单参数
        /// </summary>
        private void InitQueryOrderParameter()
        {
            SetGatewayParameterValue("appid", WeChatPayMerchant.AppId);
            SetGatewayParameterValue("mch_id", Merchant.UserName);
            SetGatewayParameterValue("out_trade_no", Order.Id);
            SetGatewayParameterValue("nonce_str", GenerateNonceString());
            SetGatewayParameterValue("sign", GetSign());    // 签名需要在最后设置，以免缺少参数。
        }
        
        
        /// <summary>
        /// 初始化表示已成功接收到支付通知的数据
        /// </summary>
        private void InitProcessSuccessParameter()
        {
            SetGatewayParameterValue("return_code", "SUCCESS");
        }


        public override void WriteSucceedFlag()
        {
            // 需要先清除之前接收到的通知的参数，否则会对生成标志成功接收到通知的XML造成干扰。
            ClearAllGatewayParameter();
            InitProcessSuccessParameter();
            HttpContext.Current.Response.Write(ConvertGatewayParameterDataToXml());
        }
    }
}
