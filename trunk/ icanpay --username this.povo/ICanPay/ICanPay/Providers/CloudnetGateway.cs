using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using ICanPay;

namespace ICanPay.Providers
{
    /// <summary>
    /// 云网
    /// </summary>
    sealed public class CloudnetGateway : PayGateway, IPaymentForm, IQueryForm
    {
        const string payGatewayUrl = @"https://www.cncard.net/purchase/getorder.asp";
        const string queryGatewayUrl = @"https://www.cncard.net/purchase/queryorder.asp";
        Dictionary<string, string> parma;


        /// <summary>
        /// 网关名称
        /// </summary>
        public override GatewayType GatewayName
        {
            get 
            {
                return GatewayType.Cloudnet;
            }
        }


        /// <summary>
        /// 验证订单是否支付成功
        /// </summary>
        /// <remarks>这里处理查询订单的网关通知跟支付订单的网关通知</remarks>
        protected override bool CheckNotifyData()
        {
            // 通知数据中必须包含的Key，如果没有表示数据可能非法
            string[] checkParma = { "c_signstr", "c_moneytype", "c_succmark", "c_order", "c_orderamount" };

            if (!PayUtility.ContainsKey(checkParma, OtherData))
            {
                return false;
            }

            // 检查订单是否支付成功，订单签名是否正确，货币类型是否为RMB
            if (OtherData["c_signstr"] == NotifySign() && OtherData["c_moneytype"] == "0")
            {
                if (OtherData["c_succmark"] == "Y" || OtherData["c_succmark"] == "001")
                {
                    Order.Amount = Convert.ToDouble(OtherData["c_orderamount"]);
                    Order.OrderId = OtherData["c_order"];

                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 网关通知的数据签名
        /// </summary>
        private string NotifySign()
        {
            // 生成签名参数的顺序
            string[] notifyParma = {"c_mid", "c_order", "c_orderamount", "c_ymd", "c_transnum", 
                                    "c_succmark", "c_moneytype", "c_memo1", "c_memo2"};
            string sign = PayUtility.GetOtherDataValue(notifyParma, OtherData) + Merchant.Key;

            return PayUtility.MD5(sign).ToLower();
        }


        /// <summary>
        /// 创建Form支付订单HTML代码
        /// </summary>
        public string BuildPaymentForm()
        {
            if (!PayUtility.IsNumeric(Order.OrderId))
            {
                throw new ArgumentException("订单编号只能是数字", "OrderId");
            }

            parma = new Dictionary<string, string>();
            parma.Add("c_mid", Merchant.UserName);
            parma.Add("c_order", Order.OrderId);
            parma.Add("c_name", Customer.Name);
            parma.Add("c_address", Customer.Address);
            parma.Add("c_tel", Customer.Telephone);
            parma.Add("c_post", Customer.Post);
            parma.Add("c_email", Customer.Email);
            parma.Add("c_orderamount", Order.Amount.ToString());
            parma.Add("c_ymd", DateTime.Now.ToString("yyyyMMdd"));
            parma.Add("c_moneytype", "0");
            parma.Add("c_retflag", "1");
            parma.Add("c_returl", Merchant.NotifyUrl);
            parma.Add("notifytype", "1");
            parma.Add("c_signstr", PaySign());

            return GetForm(parma, payGatewayUrl);
        }


        /// <summary>
        /// 支付签名
        /// </summary>
        private string PaySign()
        {
            StringBuilder sign = new StringBuilder();

            sign.Append(Merchant.UserName);
            sign.Append(Order.OrderId);
            sign.Append(Order.Amount);
            sign.Append(DateTime.Now.ToString("yyyyMMdd"));
            sign.Append("0"); // c_moneytype
            sign.Append("1"); // c_retflag
            sign.Append(Merchant.NotifyUrl);
            sign.Append("1"); // notifytype
            sign.Append(Merchant.Key);

            return PayUtility.MD5(sign.ToString()).ToLower();
        }


        /// <summary>
        /// 查询签名
        /// </summary>
        private string QuerySign()
        {
            StringBuilder sign = new StringBuilder();

            sign.Append(Merchant.UserName);
            sign.Append(Order.OrderId);
            sign.Append(OtherData["c_ymd"]);
            sign.Append(Merchant.NotifyUrl);
            sign.Append(Merchant.Key);

            return PayUtility.MD5(sign.ToString()).ToLower();
        }


        /// <summary>
        /// 创建Form查询订单HTML代码。云网查询订单需要c_ymd参数，c_ymd为订单的交易日期。
        /// 通过OtherData["c_ymd"]设置订单交易日期。
        /// </summary>
        public string BuildQueryForm()
        {
            if (!PayUtility.IsNumeric(Order.OrderId))
            {
                throw new ArgumentException("订单编号只能是数字", "OrderId");
            }

            // 是否有附加的c_ymd参数
            if (!OtherData.ContainsKey("c_ymd"))
            {
                throw new ArgumentException("云网查询订单需要c_ymd参数，c_ymd为订单的交易日期。通过OtherData[" + "c_ymd" + "]设置订单交易日期。",
                                            "OtherData[" + "c_ymd" + "]");
            }
            else if (!PayUtility.IsNumeric(OtherData["c_ymd"]) && OtherData["c_ymd"].Length != 8)
            {
                throw new ArgumentException("交易日期必须为数字，格式为yyyyMMdd", "OtherData[" + "c_ymd" + "]");
            }

            parma = new Dictionary<string, string>();
            parma.Add("c_mid", Merchant.UserName);
            parma.Add("c_order", Order.OrderId);
            parma.Add("c_ymd", OtherData["c_ymd"]);
            parma.Add("c_returl", Merchant.NotifyUrl);
            parma.Add("c_signstr", QuerySign());

            return GetForm(parma, queryGatewayUrl);
        }

    }
}



/*
 * 
 * 
 * 支付时网关返回数据格式参考：
 * /Pay/notify.aspx?c_mid=1020440&c_order=1&c_orderamount=.01&c_ymd=20080412&c_transnum=9052704&c_succmark=Y&c_cause=succ&c_moneytype=0&c_memo1=&c_memo2=&c_signstr=a21230bfc92bd73d70e6a8e2bd8d7867&c_paygate=2000
 * 
 * 查询时网关的返回数据格式参考：
 * /Pay/notify.aspx?c_mid=1020440&c_order=1&c_orderamount=.01&c_ymd=20080412&c_moneytype=0&c_succmark=001&c_cause=&c_memo1=&c_memo2=&c_signstr=88256b63dd5d41b8094b37f8a0428c1d&c_name=%B9%FE&c_address=&c_email=&c_tel=&c_post=
 * 
*/