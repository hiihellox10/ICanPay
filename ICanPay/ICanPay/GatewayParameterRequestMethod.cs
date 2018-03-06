using System;

namespace ICanPay
{
    /// <summary>
    /// 向网关发送或接收到的网关的数据的请求方式类型
    /// </summary>
    public enum GatewayParameterRequestMethod
    {
        None,

        Get,

        Post
    }
}
