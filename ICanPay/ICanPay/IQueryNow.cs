
namespace ICanPay
{
    /// <summary>
    /// 向支付平台的订单查询接口查询的订单的状态，支付平台立即输出返回查询结果。
    /// </summary>
    interface IQueryNow
    {
        /// <summary>
        /// 查询订单是否支付成功。
        /// </summary>
        /// <remarks>
        /// 支付平台的查询接口输出返回查询数据。
        /// </remarks>
        bool QueryNow();
    }
}
