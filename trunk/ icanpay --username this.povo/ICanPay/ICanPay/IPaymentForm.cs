using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// ����֧������Form HTML����Ľӿ�
    /// </summary>
    internal interface IPaymentForm
    {
        /// <summary>
        /// ����Form�ύ֧������HTML����
        /// </summary>
        string BuildPaymentForm();
    }
}
