using System;

namespace ICanPay
{
    /// <summary>
    /// 向网关发送或接收到的网关的数据的方式类型
    /// </summary>
    [Flags]
    public enum GatewayParameterType
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
        Both = GatewayParameterType.Get | GatewayParameterType.Post

    }
}
