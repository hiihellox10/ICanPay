using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// ֧��Url�Ľӿ�
    /// </summary>
    internal interface IPaymentUrl
    {
        /// <summary>
        /// ֧���������ݵ�Url����
        /// </summary>
        string BuildPaymentUrl();
    }
}
