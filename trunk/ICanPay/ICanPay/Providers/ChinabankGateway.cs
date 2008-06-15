using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ICanPay.Providers
{
    /// <summary>
    /// �й�����
    /// </summary>
    public class ChinabankGateway : PayGateway, IPaymentForm, IQueryForm
    {
        const string payGatewayUrl = @"https://pay3.chinabank.com.cn/PayGate";
        const string queryGatewayUrl = @"https://pay3.chinabank.com.cn/receiveorder.jsp";
        Dictionary<string, string> parma;

        /// <summary>
        /// ������
        /// </summary>
        public override GatewayType GatewayName
        {
            get
            {
                return GatewayType.ChinaBank;
            }
        }


        /// <summary>
        /// �������֧��֪ͨ���Ƿ�֧���ɹ�
        /// </summary>
        protected override bool CheckNotifyData()
        {
            // ֪ͨ�����б��������Key�����û�б�ʾ���ݿ��ܷǷ�
            string[] checkParma = { "v_oid", "v_pstatus", "v_pstring", "v_pmode", "v_md5str", "v_amount", "v_moneytype" };

            if (!PayUtility.ContainsKey(checkParma, OtherData))
            {
                return false;
            }

            // ��鶩���Ƿ�֧���ɹ�������ǩ���Ƿ���ȷ�����������Ƿ�ΪRMB
            if (OtherData["v_md5str"] == NotifySign() && OtherData["v_moneytype"] == "CNY" && OtherData["v_pstatus"] == "20" || OtherData["v_pstring"] == "֧�����")
            {
                Order.Amount = Convert.ToDouble(OtherData["v_amount"]);
                Order.OrderId = OtherData["v_oid"];

                return true;
            }

            return false;
        }


        /// <summary>
        /// ֪ͨǩ��
        /// </summary>
        private string NotifySign()
        {
            string[] notifyParma = { "v_oid", "v_pstatus", "v_amount", "v_moneytype" };
            string sign = PayUtility.GetOtherDataValue(notifyParma, OtherData) + Merchant.Key;

            return PayUtility.MD5(sign).ToUpper();
        }

        /// <summary>
        /// ����֧��HTML����
        /// </summary>
        public string BuildPaymentForm()
        {
            if (!IsRightOrderId(Order.OrderId))
            {
                throw new ArgumentException("����ֻ����Ӣ�����ָ�Ӣ�ķ�����ɡ�", "OrderId");
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
        /// ֧��ǩ��
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
        /// �Ƿ�����ȷ�Ķ�����Ÿ�ʽ������ֻ����Ӣ�����ָ�_-#$():;,.������ɡ�
        /// </summary>
        /// <param name="orderId">�������</param>
        private bool IsRightOrderId(string orderId)
        {
            return Regex.IsMatch(orderId, @"^[a-zA-Z_\-0-9#$():;,.]+$");
        }


        /// <summary>
        /// ������ѯHTML����
        /// </summary>
        public string BuildQueryForm()
        {
            if (!IsRightOrderId(Order.OrderId))
            {
                throw new ArgumentException("����ֻ����Ӣ�����ָ�_-#$():;,.������ɡ�", "OrderId");
            }

            parma = new Dictionary<string, string>();
            parma.Add("v_mid", Merchant.UserName);
            parma.Add("v_oid", Order.OrderId);
            parma.Add("v_url", Merchant.NotifyUrl);
            parma.Add("billNo_md5", QuerySign());

            return GetForm(parma, queryGatewayUrl);
        }


        /// <summary>
        /// ��ѯǩ��
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
���������ص�֪ͨ��Form�е����ݸ�ʽ
v_oid, 1
v_pstatus, 20
v_pstring, ֧���ɹ�
v_pmode, ��������
v_md5str, D4502468C877AA4F801AAD128CBC6134
v_md5info, 73d390268ab382ca8a693901cc69dba1
v_md5money, 6606290f1faf96e3c5df2cfd36c96f15
v_md5, D4502468C877AA4F801AAD128CBC6134
v_amount, 0.01
v_moneytype, CNY
remark1, 
remark2,
*/