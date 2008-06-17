using System;
using System.Collections.Generic;
using System.Text;
using ICanPay.Providers;

namespace ICanPay
{
    /// <summary>
    /// 验证网关的类型，通过对返回数据的识别判断网关类型
    /// </summary>
    internal class ProcessNotify
    {
        Dictionary<string, string> notifyData;

        public ProcessNotify(Dictionary<string, string> notifyData)
        {
            this.notifyData = notifyData;
        }


        /// <summary>
        /// 验证网关的类型，如果没有通过验证返回null
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
        /// 验证是否是Yeepay网关
        /// </summary>
        private bool IsYeepayGateway
        {
            get
            {
                // 验证的Key，检查是否在发回的数据中，需要保证Key在各个网关的发送数据中的的唯一性，并且一定存在。
                string[] verifyKeys = { "r0_Cmd", "r1_Code", "r2_TrxId", "r3_Amt" };
                return PayUtility.ContainsKey(verifyKeys, notifyData);
            }
        }


        /// <summary>
        /// 是否是Cloudnet网关
        /// </summary>
        private bool IsCloudnetGateway
        {
            get
            {
                // 验证的Key，检查是否在发回的数据中，需要保证Key在各个网关的发送数据中的的唯一性，并且一定存在。
                string[] verifyKeys = { "c_mid", "c_order", "c_orderamount", "c_ymd", "c_succmark" };
                return PayUtility.ContainsKey(verifyKeys, notifyData);
            }
        }


        /// <summary>
        /// 是否是Tenpay网关
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
        /// 是否是Chinabank网关
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
