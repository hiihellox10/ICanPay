using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// 支付Url的接口
    /// </summary>
    internal interface IPaymentUrl
    {
        /// <summary>
        /// 支付订单数据的Url键接
        /// </summary>
        string BuildPaymentUrl();
    }
}
