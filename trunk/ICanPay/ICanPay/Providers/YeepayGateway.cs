using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using ICanPay;

namespace ICanPay.Providers
{
    /// <summary>
    /// �ױ�
    /// </summary>
    sealed public class YeepayGateway : PayGateway, IPaymentForm, IPaymentUrl, ICheckPayment
    {
        const string payGatewayUrl = @"https://www.yeepay.com/app-merchant-proxy/node";

        /// <summary>
        /// ��������
        /// </summary>
        public override GatewayType GatewayName
        {
            get 
            { 
                return GatewayType.YeePay;
            }
        }


        /// <summary>
        /// ֧��������Form HTML����
        /// </summary>
        public string BuildPaymentForm()
        {
            Dictionary<string, string> parma = new Dictionary<string, string>();
            parma.Add("p0_Cmd", "Buy");
            parma.Add("p1_MerId", Merchant.UserName);
            parma.Add("p2_Order", Order.OrderId);
            parma.Add("p3_Amt", Order.Amount.ToString());
            parma.Add("p4_Cur", "CNY");
            parma.Add("p5_Pid", Order.OrderId);
            parma.Add("p8_Url", Merchant.NotifyUrl);
            parma.Add("p9_SAF", "0");
            parma.Add("pr_NeedResponse", "1");
            parma.Add("hmac", PaySign());

            return GetForm(parma, payGatewayUrl);
        }


        /// <summary>
        /// ֧��ǩ��
        /// </summary>
        private string PaySign()
        {
            StringBuilder sign = new StringBuilder();
            sign.Append("Buy");
            sign.Append(Merchant.UserName);
            sign.Append(Order.OrderId);
            sign.Append(Order.Amount);
            sign.Append("CNY");
            sign.Append(Order.OrderId);
            sign.Append(Merchant.NotifyUrl);
            sign.Append("0");   // p9_SAF ֵ
            sign.Append("1");   // pr_NeedResponse ֵ

            string hmacSign = YeepayHmacMD5.HmacSign(sign.ToString(), Merchant.Key);
            return hmacSign;
        }


        /// <summary>
        ///  ֧���������ݵ�Url
        /// </summary>
        public string BuildPaymentUrl()
        {
            StringBuilder url = new StringBuilder();

            url.Append(payGatewayUrl + "?");
            url.Append("p0_Cmd=" + "Buy");
            url.Append("&p1_MerId=" + Merchant.UserName);
            url.Append("&p2_Order=" + Order.OrderId);
            url.Append("&p3_Amt=" + Order.Amount);
            url.Append("&p4_Cur=" + "CNY");
            url.Append("&p5_Pid=" + Order.OrderId);
            url.Append("&p8_Url=" + Merchant.NotifyUrl);
            url.Append("&p9_SAF=0");
            url.Append("&pr_NeedResponse=1");
            url.Append("&hmac=" + PaySign());

            return url.ToString();
        }


        /// <summary>
        /// ��֤�����Ƿ�֧���ɹ�
        /// </summary>
        protected override bool CheckNotifyData()
        {
            // ֪ͨ�����б��������Key�����û�б�ʾ���ݿ��ܷǷ�
            string[] checkParma = { "hmac", "r1_Code", "r4_Cur", "r3_Amt", "r6_Order" };

            string sign = NotifySign();

            if (!PayUtility.ContainsKey(checkParma, OtherData))
            {
                return false;
            }

            // ��鶩���Ƿ�֧���ɹ�������ǩ���Ƿ���ȷ�����������Ƿ�ΪRMB
            if (OtherData["hmac"] == sign && OtherData["r1_Code"] == "1" && OtherData["r4_Cur"] == "RMB")
            {
                Order.Amount = Convert.ToDouble(OtherData["r3_Amt"]);
                Order.OrderId = OtherData["r6_Order"];

                return true;
            }

            return false;
        }


        /// <summary>
        /// ֪ͨ���ݵ�ǩ��
        /// </summary>
        private string NotifySign()
        {
            // ����ǩ��������˳��
            string[] notifyParma = {"p1_MerId", "r0_Cmd", "r1_Code", "r2_TrxId", "r3_Amt", "r4_Cur", "r5_Pid", "r6_Order", "r7_Uid",
                                    "r8_MP", "r9_BType"};

            string sign = PayUtility.GetOtherDataValue(notifyParma, OtherData);

            return YeepayHmacMD5.HmacSign(sign, Merchant.Key);
        }


        /// <summary>
        /// ��ѯ�����Ƿ�֧���ɹ�
        /// </summary>
        public bool CheckPayment()
        {
            string sendData = GetQueryUrl();
            StreamReader sr = null;
            WebResponse resp = null;
            bool result;

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(sendData);
            myReq.Method = "GET";
            myReq.ContentType = "application/x-www-form-urlencoded";

            try
            {
                resp = myReq.GetResponse();
                sr = new StreamReader(resp.GetResponseStream());
            }
            catch
            {
            }

            if (sr == null)
            {
                return false;
            }

            result = ReadReturnStream(sr);

            sr.Close();
            resp.Close();

            return result;
        }


        /// <summary>
        /// ������ѯ������Url
        /// </summary>
        private string GetQueryUrl()
        {
            string signStr = "QueryOrdDetail" + Merchant.UserName + Order.OrderId;
            string hmac = YeepayHmacMD5.HmacSign(signStr, Merchant.Key);

            return payGatewayUrl + "?p0_Cmd=QueryOrdDetail" + "&p1_MerId=" + Merchant.UserName + "&p2_Order=" + Order.OrderId + "&hmac=" + hmac;
        }


        /// <summary>
        /// ��ȡ���������صĶ���������
        /// </summary>
        private bool ReadReturnStream(StreamReader reader)
        {
            Dictionary<string, string> returnData = new Dictionary<string, string>();
            
            string line = string.Empty;
            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('=');
                returnData.Add(data[0], data[1]);
            }

            // �����Ƿ�֧���ɹ�
            if (returnData["r1_Code"] != "1" && returnData["rb_PayStatus"] != "SUCCESS")
            {
                return false;
            }

            Order.Amount = Convert.ToDouble(returnData["r3_Amt"]);
            Order.OrderId = returnData["r6_Order"];

            return true;
        }
    }
}


/*
 * 
֧���ɹ����ط�������
http://www.sina.com/?p1_MerId=10000432521&r0_Cmd=Buy&r1_Code=1&r2_TrxId=9130402221282816&r3_Amt=0.01&r4_Cur=RMB&r5_Pid=3721002&r6_Order=3721002&r7_Uid=&r8_MP=&r9_BType=1&ru_Trxtime=20080328190019&ro_BankOrderId=23412909&rb_BankId=1000000-NET&rp_PayDate=20080328190018&hmac=98323b53efeb78c3c2f1bbd1e2cc81f1


��ѯ���ݣ������������ǩ��
https://www.yeepay.com/app-merchant-proxy/node?p0_Cmd=QueryOrdDetail&p1_MerId=10000432521&p2_Order=3721002&hmac=c20bcda467cb2f42d382acf42b41421f


�������ݣ������������ǩ��
r0_Cmd=QueryOrdDetail
r1_Code=1
r2_TrxId=9130402221282816
r3_Amt=0.01
r4_Cur=RMB
r5_Pid=3721002
r6_Order=3721002
r8_MP=
ra_FrpTrxId=
rb_PayStatus=SUCCESS
rc_RefundCount=0
rd_RefundAmt=0.0
hmac=16c352949aa45e5c74eb1d5abff0237d
*/