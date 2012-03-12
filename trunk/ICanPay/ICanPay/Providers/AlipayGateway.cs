using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ICanPay.Providers
{
    /// <summary>
    /// 支付宝网关
    /// </summary>
    /// <remarks>
    /// 当前支付宝的实现只提供了即时到帐功能
    /// </remarks>
    public sealed class AlipayGateway : PayGateway, IPaymentForm, IPaymentUrl
    {

        #region 私有字段

        const string payGatewayUrl = @"https://www.alipay.com/cooperate/gateway.do";
        static Encoding pageEncoding = Encoding.GetEncoding("gb2312");

        #endregion


        #region 构造函数

        /// <summary>
        /// 初始化支付宝网关
        /// </summary>
        public AlipayGateway()
        {
        }


        /// <summary>
        /// 初始化支付宝网关
        /// </summary>
        /// <param name="gatewayParameterData">网关通知的数据集合</param>
        public AlipayGateway(ICollection<GatewayParameter> gatewayParameterData)
            : base(gatewayParameterData)
        {
        }

        #endregion


        #region 属性

        public override GatewayType GatewayType
        {
            get
            {
                return GatewayType.Alipay;
            }
        }


        public override PaymentNotifyMethod PaymentNotifyMethod
        {
            get
            {
                // 通过RequestType、UserAgent来判断是否为服务器通知
                if (string.Compare(HttpContext.Current.Request.RequestType, "POST") == 0 &&
                    string.Compare(HttpContext.Current.Request.UserAgent, "Mozilla/4.0") == 0)
                {
                    return PaymentNotifyMethod.ServerNotify;
                }

                return PaymentNotifyMethod.AutoReturn;
            }
        }


        #endregion


        #region IPaymentForm 成员

        public string BuildPaymentForm()
        {
            if (string.IsNullOrEmpty(GetGatewayParameterValue("seller_email")))
            {
                throw new ArgumentNullException("seller_email", "订单缺少seller_email参数，seller_email是卖家支付宝账号的邮箱。" +
                                                "你需要使用PaymentSetting<T>.SetGatewayParameterValue(\"seller_email\", \"youname@email.com\")方法设置卖家支付宝账号的邮箱。");
            }

            if (!Utility.IsEmail(GetGatewayParameterValue("seller_email")))
            {
                throw new ArgumentException("Email格式不正确", "seller_email");
            }

            Dictionary<string, string> parma = new Dictionary<string, string>();
            parma.Add("service", "create_direct_pay_by_user");
            parma.Add("partner", Merchant.UserName);
            parma.Add("notify_url", Merchant.NotifyUrl.ToString());
            parma.Add("return_url", Merchant.NotifyUrl.ToString());
            parma.Add("sign", PaySign());
            parma.Add("sign_type", "MD5");
            parma.Add("subject", Order.OrderId);
            parma.Add("out_trade_no", Order.OrderId);
            parma.Add("total_fee", Order.Amount.ToString());
            parma.Add("payment_type", "1");
            parma.Add("seller_email", GetGatewayParameterValue("seller_email"));
            parma.Add("_input_charset", "gb2312");

            return GetForm(parma, payGatewayUrl);
        }

        #endregion


        #region IPaymentUrl 成员

        public string BuildPaymentUrl()
        {
            if (string.IsNullOrEmpty(GetGatewayParameterValue("seller_email")))
            {
                throw new ArgumentNullException("seller_email", "订单缺少seller_email参数，seller_email是卖家支付宝账号的邮箱。" +
                                                "你需要使用PaymentSetting<T>.SetGatewayParameterValue(\"seller_email\", \"youname@email.com\")方法设置卖家支付宝账号的邮箱。");
            }

            if (!Utility.IsEmail(GetGatewayParameterValue("seller_email")))
            {
                throw new ArgumentException("Email格式不正确", "seller_email");
            }

            string sign = GetSignPrama();
            return string.Format("{0}?{1}&sign={2}&sign_type=MD5", payGatewayUrl, sign, PaySign(sign));
        }

        #endregion


        #region 方法

        protected override bool CheckNotifyData()
        {
            if (ValidateAlipayNotify() && ValidateAlipayNotifySign())
            {
                // 支付状态是否为成功
                if (string.Compare(GetGatewayParameterValue("trade_status"), "TRADE_FINISHED") == 0)
                {
                    Order.Amount = double.Parse(GetGatewayParameterValue("total_fee"));
                    Order.OrderId = GetGatewayParameterValue("out_trade_no");

                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 验证支付宝通知的签名
        /// </summary>
        private bool ValidateAlipayNotifySign()
        {
            // 获得签名字符串，首先需要将参数排序。
            // 然后组合参数。将值非空，参数名不是sign、sign_type的其他参数组合起来验证签名。
            List<string> paramList = new List<string>();
            foreach (KeyValuePair<string, string> item in GatewayParameterDataSort(GatewayParameterData))
            {
                if (string.Compare(item.Key, "sign") != 0 && string.Compare(item.Key, "sign_type") != 0 &&
                    !string.IsNullOrEmpty(item.Value))
                {
                    paramList.Add(string.Format("{0}={1}", item.Key, item.Value));
                }
            }

            StringBuilder sign = new StringBuilder();
            foreach (string parma in paramList)
            {
                sign.Append(parma + "&");
            }
            sign.Remove(sign.Length - 1, 1);

            // 验证通知的签名
            if (string.Compare(GetGatewayParameterValue("sign"), PaySign(sign.ToString())) == 0)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 将网关参数的集合排序
        /// </summary>
        /// <param name="coll">原网关参数的集合</param>
        private SortedList<string, string> GatewayParameterDataSort(ICollection<GatewayParameter> coll)
        {
            SortedList<string, string> list = new SortedList<string, string>();
            foreach (GatewayParameter item in coll)
            {
                list.Add(item.ParameterName, item.ParameterValue);
            }

            return list;
        }


        /// <summary>
        /// 获得提交的支付订单数据的签名。
        /// </summary>
        private string PaySign()
        {
            return PaySign(GetSignPrama());
        }


        /// <summary>
        /// 获得提交的支付订单数据的签名。
        /// </summary>
        /// <param name="SignPrama">用于签名的字符串(不包括Key)</param>
        private string PaySign(string signPrama)
        {
            return AlipayMD5(signPrama + Merchant.Key);
        }


        /// <summary>
        /// 获得用于签名数据的支付宝MD5散列值
        /// </summary>
        /// <param name="signPrama">用于签名的字符串</param>
        private string AlipayMD5(string signPrama)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(pageEncoding.GetBytes(signPrama));

            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }

            return sb.ToString();
        }


        /// <summary>
        /// 获得用于签名的数据字符串
        /// </summary>
        private string GetSignPrama()
        {
            StringBuilder sign = new StringBuilder();

            // 签名参数需要按字母顺序添加
            sign.Append("_input_charset=gb2312&");
            sign.AppendFormat("notify_url={0}&", Merchant.NotifyUrl.ToString());
            sign.AppendFormat("out_trade_no={0}&", Order.OrderId);
            sign.AppendFormat("partner={0}&", Merchant.UserName);
            sign.Append("payment_type=1&");
            sign.AppendFormat("return_url={0}&", Merchant.NotifyUrl.ToString());
            sign.AppendFormat("seller_email={0}&", GetGatewayParameterValue("seller_email"));
            sign.Append("service=create_direct_pay_by_user&");
            sign.AppendFormat("subject={0}&", Order.OrderId);
            sign.AppendFormat("total_fee={0}", Order.Amount.ToString());

            return sign.ToString();
        }


        protected override void WriteSucceedFlag()
        {
            if (PaymentNotifyMethod == PaymentNotifyMethod.ServerNotify)
            {
                System.Web.HttpContext.Current.Response.Write("success");
            }
        }


        /// <summary>
        /// 验证网关的通知是否正确
        /// </summary>
        private bool ValidateAlipayNotify()
        {
            // 因为网关通知的有效性验证只有1分钟时间，超过时间后将无法验证通知是否正确。
            // 可能会因为支付宝服务器或者其它问题造成没有在1分钟内完成通知的验证，
            // 超时以后将无法正确验证，这会导致整个支付的验证失败。有必要的话可以考虑不验证通知的真实性直接返回true。
            // 因为测试的时候支付宝的验证服务器有时差，收到支付宝通知的时候的时候验证服务器不能正确通过验证，
            // 需要延迟几秒，所以屏蔽了验证代码。
            return true;

            //try
            //{
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetValidateAlipayNotifyUrl());
            //    using (WebResponse response = request.GetResponse())
            //    {
            //        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            //        {
            //            if (reader == null)
            //            {
            //                return false;
            //            }

            //            if (string.Compare(reader.ReadToEnd(), "true") == 0)
            //            {
            //                return true;
            //            }
            //        }
            //    }
            //}
            //catch { }

            //return false;
        }


        /// <summary>
        /// 获得验证支付宝通知的Url
        /// </summary>
        private string GetValidateAlipayNotifyUrl()
        {
            return string.Format("{0}?service=notify_verify&partner={1}&notify_id={2}", payGatewayUrl, Merchant.UserName,
                                 GetGatewayParameterValue("notify_id"));
        }


        #endregion

    }
}