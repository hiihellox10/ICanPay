using System;
using System.Collections.Generic;
using System.Text;
using ICanPay.Providers;

namespace ICanPay
{
    /// <summary>
    /// ��֤���ص����ͣ�ͨ���Է������ݵ�ʶ���ж���������
    /// </summary>
    internal class ProcessNotify
    {
        Dictionary<string, string> notifyData;

        public ProcessNotify(Dictionary<string, string> notifyData)
        {
            this.notifyData = notifyData;
        }


        /// <summary>
        /// ��֤���ص����ͣ����û��ͨ����֤����null
        /// </summary>
        public PayGateway GetGateway()
        {
            if (IsYeepayGateway)
            {
                return new YeepayGateway();
            }

            if (IsCloudnetGateway)
            {
                return new CloudnetGateway();
            }

            if (IsTenpayGateway)
            {
                return new TenpayGateway();
            }

            if (IsChinabankGateway)
            {
                return new ChinabankGateway();
            }

            return null;
        }


        /// <summary>
        /// ��֤�Ƿ���Yeepay����
        /// </summary>
        private bool IsYeepayGateway
        {
            get
            {
                // ��֤��Key������Ƿ��ڷ��ص������У���Ҫ��֤Key�ڸ������صķ��������еĵ�Ψһ�ԣ�����һ�����ڡ�
                string[] verifyKeys = { "r0_Cmd", "r1_Code", "r2_TrxId", "r3_Amt" };
                return PayUtility.ContainsKey(verifyKeys, notifyData);
            }
        }


        /// <summary>
        /// �Ƿ���Cloudnet����
        /// </summary>
        private bool IsCloudnetGateway
        {
            get
            {
                // ��֤��Key������Ƿ��ڷ��ص������У���Ҫ��֤Key�ڸ������صķ��������еĵ�Ψһ�ԣ�����һ�����ڡ�
                string[] verifyKeys = { "c_mid", "c_order", "c_orderamount", "c_ymd", "c_succmark" };
                return PayUtility.ContainsKey(verifyKeys, notifyData);
            }
        }


        /// <summary>
        /// �Ƿ���Tenpay����
        /// </summary>
        private bool IsTenpayGateway
        {
            get
            {
                string[] verifyKeys = { "cmdno", "transaction_id", "sp_billno", "total_fee", "fee_type" };
                return PayUtility.ContainsKey(verifyKeys, notifyData);
            }
        }


        /// <summary>
        /// �Ƿ���Chinabank����
        /// </summary>
        private bool IsChinabankGateway
        {
            get
            {
                string[] verifyKeys = { "v_oid", "v_pstatus", "v_amount", "v_moneytype" };
                return PayUtility.ContainsKey(verifyKeys, notifyData);
            }
        }
    }
}
