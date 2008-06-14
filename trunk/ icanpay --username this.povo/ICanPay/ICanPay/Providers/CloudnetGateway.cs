using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using ICanPay;

namespace ICanPay.Providers
{
    /// <summary>
    /// ����
    /// </summary>
    sealed public class CloudnetGateway : PayGateway, IPaymentForm, IQueryForm
    {
        const string payGatewayUrl = @"https://www.cncard.net/purchase/getorder.asp";
        const string queryGatewayUrl = @"https://www.cncard.net/purchase/queryorder.asp";
        Dictionary<string, string> parma;


        /// <summary>
        /// ��������
        /// </summary>
        public override GatewayType GatewayName
        {
            get 
            {
                return GatewayType.Cloudnet;
            }
        }


        /// <summary>
        /// ��֤�����Ƿ�֧���ɹ�
        /// </summary>
        /// <remarks>���ﴦ���ѯ����������֪ͨ��֧������������֪ͨ</remarks>
        protected override bool CheckNotifyData()
        {
            // ֪ͨ�����б��������Key�����û�б�ʾ���ݿ��ܷǷ�
            string[] checkParma = { "c_signstr", "c_moneytype", "c_succmark", "c_order", "c_orderamount" };

            if (!PayUtility.ContainsKey(checkParma, OtherData))
            {
                return false;
            }

            // ��鶩���Ƿ�֧���ɹ�������ǩ���Ƿ���ȷ�����������Ƿ�ΪRMB
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
        /// ����֪ͨ������ǩ��
        /// </summary>
        private string NotifySign()
        {
            // ����ǩ��������˳��
            string[] notifyParma = {"c_mid", "c_order", "c_orderamount", "c_ymd", "c_transnum", 
                                    "c_succmark", "c_moneytype", "c_memo1", "c_memo2"};
            string sign = PayUtility.GetOtherDataValue(notifyParma, OtherData) + Merchant.Key;

            return PayUtility.MD5(sign).ToLower();
        }


        /// <summary>
        /// ����Form֧������HTML����
        /// </summary>
        public string BuildPaymentForm()
        {
            if (!PayUtility.IsNumeric(Order.OrderId))
            {
                throw new ArgumentException("�������ֻ��������", "OrderId");
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
        /// ֧��ǩ��
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
        /// ��ѯǩ��
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
        /// ����Form��ѯ����HTML���롣������ѯ������Ҫc_ymd������c_ymdΪ�����Ľ������ڡ�
        /// ͨ��OtherData["c_ymd"]���ö����������ڡ�
        /// </summary>
        public string BuildQueryForm()
        {
            if (!PayUtility.IsNumeric(Order.OrderId))
            {
                throw new ArgumentException("�������ֻ��������", "OrderId");
            }

            // �Ƿ��и��ӵ�c_ymd����
            if (!OtherData.ContainsKey("c_ymd"))
            {
                throw new ArgumentException("������ѯ������Ҫc_ymd������c_ymdΪ�����Ľ������ڡ�ͨ��OtherData[" + "c_ymd" + "]���ö����������ڡ�",
                                            "OtherData[" + "c_ymd" + "]");
            }
            else if (!PayUtility.IsNumeric(OtherData["c_ymd"]) && OtherData["c_ymd"].Length != 8)
            {
                throw new ArgumentException("�������ڱ���Ϊ���֣���ʽΪyyyyMMdd", "OtherData[" + "c_ymd" + "]");
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
 * ֧��ʱ���ط������ݸ�ʽ�ο���
 * /Pay/notify.aspx?c_mid=1020440&c_order=1&c_orderamount=.01&c_ymd=20080412&c_transnum=9052704&c_succmark=Y&c_cause=succ&c_moneytype=0&c_memo1=&c_memo2=&c_signstr=a21230bfc92bd73d70e6a8e2bd8d7867&c_paygate=2000
 * 
 * ��ѯʱ���صķ������ݸ�ʽ�ο���
 * /Pay/notify.aspx?c_mid=1020440&c_order=1&c_orderamount=.01&c_ymd=20080412&c_moneytype=0&c_succmark=001&c_cause=&c_memo1=&c_memo2=&c_signstr=88256b63dd5d41b8094b37f8a0428c1d&c_name=%B9%FE&c_address=&c_email=&c_tel=&c_post=
 * 
*/