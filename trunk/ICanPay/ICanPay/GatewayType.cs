using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// 网关类型
    /// </summary>
    public enum GatewayType
    {
        /// <summary>
        /// 财付通
        /// </summary>
        TenPay = 1,


        /// <summary>
        /// 中国网银
        /// </summary>
        ChinaBank = 2,


        /// <summary>
        /// 易宝
        /// </summary>
        YeePay = 3,


        /// <summary>
        /// 云网
        /// </summary>
        Cloudnet = 4
    }
}
