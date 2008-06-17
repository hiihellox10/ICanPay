using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// 建立支付订单Form HTML代码的接口
    /// </summary>
    internal interface IPaymentForm
    {
        /// <summary>
        /// 建立Form提交支付订单HTML代码
        /// </summary>
        string BuildPaymentForm();
    }
}
