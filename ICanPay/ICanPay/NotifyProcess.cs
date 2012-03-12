using System.Collections.Generic;
using ICanPay.Providers;

namespace ICanPay
{
    /// <summary>
    /// 网关通知的处理类，通过对返回数据的分析识别网关类型
    /// </summary>
    internal static class NotifyProcess
    {

        #region 私有字段

        static ICollection<GatewayParameter> gatewayParameterData;

        // 需要验证的参数名称数组，用于识别不同的网关类型。
        // 检查是否在发回的数据中，需要保证参数名称跟其他各个网关验证的参数名称不重复。
        // 建议使用网关中返回的不为空的参数名，并使用尽可能多的参数名。
        static string[] yeepayGatewayVerifyParmaNames = { "r0_Cmd", "r1_Code", "r2_TrxId", "r3_Amt", "r4_Cur", "r5_Pid", "r6_Order" };
        static string[] tenpayGatewayVerifyParmaNames = { "cmdno", "transaction_id", "sp_billno", "total_fee", "fee_type" };
        static string[] chinabankGatewayVerifyParmaNames = { "v_oid", "v_pstatus", "v_amount", "v_moneytype" };
        static string[] alipayGatewayVerifyParmaNames = { "notify_type", "notify_id", "notify_time", "sign", "sign_type" };

        #endregion


        #region 方法

        /// <summary>
        /// 验证网关的类型
        /// </summary>
        public static PayGateway GetGateway()
        {
            gatewayParameterData = Utility.ReadNotifyData();

            if (IsYeepayGateway)
            {
                return new YeepayGateway(gatewayParameterData);
            }

            if (IsTenpayGateway)
            {
                return new TenpayGateway(gatewayParameterData);
            }

            if (IsChinabankGateway)
            {
                return new ChinabankGateway(gatewayParameterData);
            }

            if (IsAlipayGateway)
            {
                return new AlipayGateway(gatewayParameterData);
            }

            return new NullGateway(gatewayParameterData);
        }

        #endregion


        #region 属性

        /// <summary>
        /// 验证是否是Yeepay网关
        /// </summary>
        private static bool IsYeepayGateway
        {
            get
            {
                return Utility.ExistParameter(yeepayGatewayVerifyParmaNames, gatewayParameterData);
            }
        }


        /// <summary>
        /// 是否是Tenpay网关
        /// </summary>
        private static bool IsTenpayGateway
        {
            get
            {
                return Utility.ExistParameter(tenpayGatewayVerifyParmaNames, gatewayParameterData);
            }
        }


        /// <summary>
        /// 是否是Chinabank网关
        /// </summary>
        private static bool IsChinabankGateway
        {
            get
            {
                return Utility.ExistParameter(chinabankGatewayVerifyParmaNames, gatewayParameterData);
            }
        }


        /// <summary>
        /// 是否是支付宝网关
        /// </summary>
        private static bool IsAlipayGateway
        {
            get
            {
                return Utility.ExistParameter(alipayGatewayVerifyParmaNames, gatewayParameterData);
            }
        }

        #endregion

    }
}
