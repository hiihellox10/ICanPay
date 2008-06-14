using System;
using System.Collections.Generic;
using System.Text;
using ICanPay;
using System.Text.RegularExpressions;

namespace ICanPay.Providers
{
    /// <summary>
    /// 财付通
    /// </summary>
    sealed public class TenpayGateway : PayGateway, IPaymentUrl, IQueryUrl
    {
        const string payGatewayUrl = @"https://www.tenpay.com/cgi-bin/v1.0/pay_gate.cgi";
        const string queryGatewayUrl = @"http://portal.tenpay.com/cfbiportal/cgi-bin/cfbiqueryorder.cgi";


        /// <summary>
        /// 网关名称
        /// </summary>
        public override GatewayType GatewayName
        {
            get
            {
                return GatewayType.TenPay;
            }
        }


        /// <summary>
        /// 支付订单数据的Url
        /// </summary>
        public string BuildPaymentUrl()
        {
            if(Order.OrderId.Length > 10)
            {
                throw new ArgumentException("订单编号必须少于10位数", "OrderId");
            }

            if(!PayUtility.IsNumeric(Order.OrderId))
            {
                throw new ArgumentException("订单编号只能是数字", "OrderId");
            }

            StringBuilder url = new StringBuilder();
            url.Append(payGatewayUrl + "?");
            url.Append("cmdno=1");
            url.Append("&date=" + DateTime.Now.ToString("yyyyMMdd"));
            url.Append("&bank_type=0");
            url.Append("&desc=" + Order.OrderId);
            url.Append("&purchaser_id=");
            url.Append("&bargainor_id=" + Merchant.UserName);
            url.Append("&transaction_id=" + Merchant.UserName + DateTime.Now.ToString("yyyyMMdd") + Order.OrderId.PadLeft(10, '0'));
            url.Append("&sp_billno=" + Order.OrderId);
            url.Append("&total_fee=" + Order.Amount * 100);
            url.Append("&fee_type=1");
            url.Append("&return_url=" + Merchant.NotifyUrl);
            url.Append("&attach=");
            url.Append("&sign=" + PaySign());

            return url.ToString();
        }


        /// <summary>
        /// 支付订单的签名
        /// </summary>
        private string PaySign()
        {
            StringBuilder sign = new StringBuilder();
            sign.Append("cmdno=1");
            sign.Append("&date=" + DateTime.Now.ToString("yyyyMMdd"));
            sign.Append("&bargainor_id=" + Merchant.UserName);
            sign.Append("&transaction_id=" + Merchant.UserName + DateTime.Now.ToString("yyyyMMdd") + Order.OrderId.PadLeft(10, '0'));
            sign.Append("&sp_billno=" + Order.OrderId);
            sign.Append("&total_fee=" + Order.Amount * 100);
            sign.Append("&fee_type=1");
            sign.Append("&return_url=" + Merchant.NotifyUrl);
            sign.Append("&attach=");
            sign.Append("&key=" + Merchant.Key);

            return PayUtility.MD5(sign.ToString()).ToUpper();
        }


        /// <summary>
        /// 订单查询Url
        /// </summary>
        public string BuildQueryUrl()
        {
            // 是否有附加的c_ymd参数
            if (!OtherData.ContainsKey("date"))
            {
                throw new ArgumentException("财付通查询订单需要交易日期，date为订单的交易日期。通过OtherData[" + "date" + "]设置订单交易日期，格式为yyyyMMdd",
                                            "OtherData[" + "date" + "]");
            }
            else if (!PayUtility.IsNumeric(OtherData["date"]) && OtherData["date"].Length != 8)
            {
                throw new ArgumentException("交易日期必须为数字，格式为yyyyMMdd", "OtherData[" + "date" + "]");
            }

            StringBuilder url = new StringBuilder();
            url.Append(queryGatewayUrl + "?");
            url.Append("cmdno=2");
            url.Append("&date=" + OtherData["date"]);
            url.Append("&bargainor_id=" + Merchant.UserName);
            url.Append("&transaction_id=" + Merchant.UserName + OtherData["date"] + Order.OrderId.PadLeft(10, '0'));
            url.Append("&sp_billno=" + Order.OrderId);
            url.Append("&return_url=" + Merchant.NotifyUrl);
            url.Append("&attach=");
            url.Append("&sign=" + QuerySign());

            return url.ToString();
        }


        /// <summary>
        /// 查询订单Url的签名
        /// </summary>
        private string QuerySign()
        {
            StringBuilder sign = new StringBuilder();
            sign.Append("cmdno=2");
            sign.Append("&date=" + OtherData["date"]);
            sign.Append("&bargainor_id=" + Merchant.UserName);
            sign.Append("&transaction_id=" + Merchant.UserName + OtherData["date"] + Order.OrderId.PadLeft(10, '0'));
            sign.Append("&sp_billno=" + Order.OrderId);
            sign.Append("&return_url=" + Merchant.NotifyUrl);
            sign.Append("&attach=");
            sign.Append("&key=" + Merchant.Key);

            return PayUtility.MD5(sign.ToString());
        }


        /// <summary>
        /// 验证订单是否支付成功
        /// </summary>
        /// <remarks>这里处理查询订单的网关通知跟支付订单的网关通知</remarks>
        protected override bool CheckNotifyData()
        {
            // 通知数据中必须包含的Key，如果没有表示数据可能非法
            string[] checkParma = { "cmdno", "pay_result", "pay_info", "date", "transaction_id", "sp_billno", "total_fee", "fee_type", "attach" };

            if (!PayUtility.ContainsKey(checkParma, OtherData))
            {
                return false;
            }

            // 检查订单是否支付成功，订单签名是否正确，货币类型是否为RMB
            if (OtherData["sign"] == NotifySign() && OtherData["fee_type"] == "1" && OtherData["pay_result"] == "0" && OtherData["pay_info"] == "OK")
            {
                Order.Amount = Convert.ToDouble(OtherData["total_fee"]) * 0.01;
                Order.OrderId = OtherData["sp_billno"];

                return true;
            }

            return false;
        }


        /// <summary>
        /// 服务器通知签名
        /// </summary>
        private string NotifySign()
        {
            StringBuilder sign = new StringBuilder();
            sign.Append("cmdno=" + OtherData["cmdno"]);
            sign.Append("&pay_result=" + OtherData["pay_result"]);
            sign.Append("&date=" + OtherData["date"]);
            sign.Append("&transaction_id=" + OtherData["transaction_id"]);
            sign.Append("&sp_billno=" + OtherData["sp_billno"]);
            sign.Append("&total_fee=" + OtherData["total_fee"]);
            sign.Append("&fee_type=" + OtherData["fee_type"]);
            sign.Append("&attach=" + OtherData["attach"]);
            sign.Append("&key=" + Merchant.Key);

            return PayUtility.MD5(sign.ToString());
        }
    }
}


/*
 * 接收到的通知的数据格式
 * /Pay/notify.aspx?attach=&bargainor_id=1202550401&cmdno=1&date=20080413&fee_type=1&pay_info=OK&pay_result=0&pay_time=1208059299&sign=45EB8FFC7363F8ECB6BA0610F4B7F0BA&sp_billno=2&total_fee=1&transaction_id=1202550401200804130000000002
*/