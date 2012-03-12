﻿
namespace ICanPay
{
    /// <summary>
    /// 支付订单通过url提交
    /// </summary>
    internal interface IPaymentUrl
    {
        /// <summary>
        /// 创建包含支付订单数据的url地址
        /// </summary>
        string BuildPaymentUrl();
    }
}
