using System;
namespace ICanPay
{
    interface ICheckPayment
    {
        /// <summary>
        /// 查询订单是否支付成功。服务器通过HTTP输出返回数据。
        /// </summary>
        bool CheckPayment();
    }
}
