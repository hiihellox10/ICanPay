using System;

namespace ICanPay
{
    internal interface IPaymentQRCode
    {
        string GetPaymentQRCodeContent();
    }
}