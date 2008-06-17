using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ICanPay.Providers
{
    /// <summary>
    /// 中国网银
    /// </summary>
    public class ChinabankGateway : PayGateway, IPaymentForm, IQueryForm
    {
        const string payGatewayUrl = @"https://pay3.chinabank.com.cn/PayGate";
        const string queryGatewayUrl = @"https://pay3.chinabank.com.cn/receiveorder.jsp";
        Dictionary<string, string> parma;

        /// <summary>
        /// 网关名
        /// </summary>
        public override GatewayType GatewayName
        {
            get
            {
                return GatewayType.ChinaBank;
            }
        }


        /// <summary>
        /// 检查网关支付通知，是否支付成功
        /// </summary>
        protected override bool CheckNotifyData()
        {
            // 通知数据中必须包含的Key，如果没有表示数据可能非法
            string[] checkParma = { "v_oid", "v_pstatus", "v_pstring", "v_pmode", "v_md5str", "v_amount", "v_moneytype" };

            if (!PayUtility.ContainsKey(checkParma, OtherData))
            {
                return false;
            }

            // 检查订单是否支付成功，订单签名是否正确，货币类型是否为RMB
            if (OtherData["v_md5str"] == NotifySign() && OtherData["v_moneytype"] == "CNY" && OtherData["v_pstatus"] == "20" || OtherData["v_pstring"] == "支付完成")
            {
                Order.Amount = Convert.ToDouble(OtherData["v_amount"]);
                Order.OrderId = OtherData["v_oid"];

                return true;
            }

            return false;
        }


        /// <summary>
        /// 通知签名
        /// </summary>
        private string NotifySign()
        {
            string[] notifyParma = { "v_oid", "v_pstatus", "v_amount", "v_moneytype" };
            string sign = PayUtility.GetOtherDataValue(notifyParma, OtherData) + Merchant.Key;

            return PayUtility.MD5(sign).ToUpper();
        }

        /// <summary>
        /// 创建支付HTML代码
        /// </summary>
        public string BuildPaymentForm()
        {
            if (!IsRightOrderId(Order.OrderId))
            {
                throw new ArgumentException("订单只能由英文数字跟英文符号组成。", "OrderId");
            }

            parma = new Dictionary<string, string>();
            parma.Add("v_mid", Merchant.UserName);
            parma.Add("v_oid", Order.OrderId);
            parma.Add("v_amount", Order.Amount.ToString());
            parma.Add("v_moneytype", "CNY");
            parma.Add("v_url", Merchant.NotifyUrl);
            parma.Add("v_md5info", PaySign());
            parma.Add("v_rcvname", Customer.Name);
            parma.Add("v_rcvaddr", Customer.Address);
            parma.Add("v_rcvtel", Customer.Telephone);
            parma.Add("v_rcvpost", Customer.Post);
            parma.Add("v_rcvemail", Customer.Email);

            return GetForm(parma, payGatewayUrl);
        }



        /// <summary>
        /// 支付签名
        /// </summary>
        private string PaySign()
        {
            StringBuilder sign = new StringBuilder();
            sign.Append(Order.Amount);
            sign.Append("CNY");
            sign.Append(Order.OrderId);
            sign.Append(Merchant.UserName);
            sign.Append(Merchant.NotifyUrl);
            sign.Append(Merchant.Key);

            return PayUtility.MD5(sign.ToString()).ToUpper();
        }


        /// <summary>
        /// 是否是正确的订单编号格式。订单只能由英文数字跟_-#$():;,.符号组成。
        /// </summary>
        /// <param name="orderId">订单编号</param>
        private static bool IsRightOrderId(string orderId)
        {
            return Regex.IsMatch(orderId, @"^[a-zA-Z_\-0-9#$():;,.]+$");
        }


        /// <summary>
        /// 创建查询HTML代码
        /// </summary>
        public string BuildQueryForm()
        {
            if (!IsRightOrderId(Order.OrderId))
            {
                throw new ArgumentException("订单只能由英文数字跟_-#$():;,.符号组成。", "OrderId");
            }

            parma = new Dictionary<string, string>();
            parma.Add("v_mid", Merchant.UserName);
            parma.Add("v_oid", Order.OrderId);
            parma.Add("v_url", Merchant.NotifyUrl);
            parma.Add("billNo_md5", QuerySign());

            return GetForm(parma, queryGatewayUrl);
        }


        /// <summary>
        /// 查询签名
        /// </summary>
        private string QuerySign()
        {
            StringBuilder sign = new StringBuilder();
            sign.Append(Order.OrderId);
            sign.Append(Merchant.Key);

            return PayUtility.MD5(sign.ToString()).ToUpper();
        }

    }
}



/*
服务器返回的通知中Form中的数据格式
v_oid, 1
v_pstatus, 20
v_pstring, 支付成功
v_pmode, 建设银行
v_md5str, D4502468C877AA4F801AAD128CBC6134
v_md5info, 73d390268ab382ca8a693901cc69dba1
v_md5money, 6606290f1faf96e3c5df2cfd36c96f15
v_md5, D4502468C877AA4F801AAD128CBC6134
v_amount, 0.01
v_moneytype, CNY
remark1, 
remark2,
*/