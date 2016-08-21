
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
        /// 浏览器自动返回
        /// </summary>
        AutoReturn,

        /// <summary>
        /// 服务器异步通知
        /// </summary>
        ServerNotify
    }
}
