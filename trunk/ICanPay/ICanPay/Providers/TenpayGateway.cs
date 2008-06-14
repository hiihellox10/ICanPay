using System;
using System.Collections.Generic;
using System.Text;
using ICanPay;
using System.Text.RegularExpressions;

namespace ICanPay.Providers
{
    /// <summary>
    /// �Ƹ�ͨ
    /// </summary>
    sealed public class TenpayGateway : PayGateway, IPaymentUrl, IQueryUrl
    {
        const string payGatewayUrl = @"https://www.tenpay.com/cgi-bin/v1.0/pay_gate.cgi";
        const string queryGatewayUrl = @"http://portal.tenpay.com/cfbiportal/cgi-bin/cfbiqueryorder.cgi";


        /// <summary>
        /// ��������
        /// </summary>
        public override GatewayType GatewayName
        {
            get
            {
                return GatewayType.TenPay;
            }
        }


        /// <summary>
        /// ֧���������ݵ�Url
        /// </summary>
        public string BuildPaymentUrl()
        {
            if(Order.OrderId.Length > 10)
            {
                throw new ArgumentException("������ű�������10λ��", "OrderId");
            }

            if(!PayUtility.IsNumeric(Order.OrderId))
            {
                throw new ArgumentException("�������ֻ��������", "OrderId");
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
        /// ֧��������ǩ��
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
        /// ������ѯUrl
        /// </summary>
        public string BuildQueryUrl()
        {
            // �Ƿ��и��ӵ�c_ymd����
            if (!OtherData.ContainsKey("date"))
            {
                throw new ArgumentException("�Ƹ�ͨ��ѯ������Ҫ�������ڣ�dateΪ�����Ľ������ڡ�ͨ��OtherData[" + "date" + "]���ö����������ڣ���ʽΪyyyyMMdd",
                                            "OtherData[" + "date" + "]");
            }
            else if (!PayUtility.IsNumeric(OtherData["date"]) && OtherData["date"].Length != 8)
            {
                throw new ArgumentException("�������ڱ���Ϊ���֣���ʽΪyyyyMMdd", "OtherData[" + "date" + "]");
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
        /// ��ѯ����Url��ǩ��
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
        /// ��֤�����Ƿ�֧���ɹ�
        /// </summary>
        /// <remarks>���ﴦ���ѯ����������֪ͨ��֧������������֪ͨ</remarks>
        protected override bool CheckNotifyData()
        {
            // ֪ͨ�����б��������Key�����û�б�ʾ���ݿ��ܷǷ�
            string[] checkParma = { "cmdno", "pay_result", "pay_info", "date", "transaction_id", "sp_billno", "total_fee", "fee_type", "attach" };

            if (!PayUtility.ContainsKey(checkParma, OtherData))
            {
                return false;
            }

            // ��鶩���Ƿ�֧���ɹ�������ǩ���Ƿ���ȷ�����������Ƿ�ΪRMB
            if (OtherData["sign"] == NotifySign() && OtherData["fee_type"] == "1" && OtherData["pay_result"] == "0" && OtherData["pay_info"] == "OK")
            {
                Order.Amount = Convert.ToDouble(OtherData["total_fee"]) * 0.01;
                Order.OrderId = OtherData["sp_billno"];

                return true;
            }

            return false;
        }


        /// <summary>
        /// ������֪ͨǩ��
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
 * ���յ���֪ͨ�����ݸ�ʽ
 * /Pay/notify.aspx?attach=&bargainor_id=1202550401&cmdno=1&date=20080413&fee_type=1&pay_info=OK&pay_result=0&pay_time=1208059299&sign=45EB8FFC7363F8ECB6BA0610F4B7F0BA&sp_billno=2&total_fee=1&transaction_id=1202550401200804130000000002
*/