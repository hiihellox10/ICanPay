
namespace ICanPay
{
    /// <summary>
    /// 支付通知的返回方式
    /// </summary>
    public enum PaymentNotifyMethod
    {
        /// <summary>
        /// 未知
        /// </summary>
        None = 0,

        /// <summary>
        /// 自动返回
        /// </summary>
        AutoReturn,

        /// <summary>
        /// 服务器通知
        /// </summary>
        ServerNotify
    }
}
