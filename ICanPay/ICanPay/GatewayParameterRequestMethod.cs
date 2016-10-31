using System;

namespace ICanPay
{
    /// <summary>
    /// 向网关发送或接收到的网关的数据的请求方式类型
    /// </summary>
    [Flags]
    public enum GatewayParameterRequestMethod
    {

        /// <summary>
        /// Get
        /// </summary>
        Get = 1,


        /// <summary>
        /// Post
        /// </summary>
        Post = 2,


        /// <summary>
        /// Get 和 Post
        /// </summary>
        Both = GatewayParameterRequestMethod.Get | GatewayParameterRequestMethod.Post

    }
}
