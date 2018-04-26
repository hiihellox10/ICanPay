using System;
using System.Collections.Generic;

namespace ICanPay
{
    /// <summary>
    /// 支付事件数据的基类
    /// </summary>
    public abstract class PaymentEventArgs : EventArgs
    {

        #region 私有字段

        protected GatewayBase gateway;
        string notifyServerHostAddress;

        #endregion


        #region 构造函数

        /// <summary>
        /// 初始化支付事件数据的基类
        /// </summary>
        /// <param name="gateway">支付网关</param>
        public PaymentEventArgs(GatewayBase gateway)
        {
            this.gateway = gateway;
            notifyServerHostAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
        }


        #endregion


        #region 属性

        /// <summary>
        /// 发送支付通知的网关IP地址
        /// </summary>
        public string NotifyServerHostAddress
        {
            get
            {
                return notifyServerHostAddress;
            }
        }


        /// <summary>
        /// 支付网关的Get、Post数据的集合
        /// </summary>
        public ICollection<GatewayParameter> GatewayParameterData
        {
            get
            {
                return gateway.GatewayParameterData;
            }
        }

        #endregion


        #region 方法

        /// <summary>
        /// 获得网关的参数值。没有参数值时返回空字符串，Get方式的值均为未解码。
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        public string GetGatewayParameterValue(string gatewayParameterName)
        {
            return gateway.GetGatewayParameterValue(gatewayParameterName);
        }


        /// <summary>
        /// 获得网关的参数值。没有参数值时返回空字符串，Get方式的值均为未解码。
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        /// <param name="httpMethod">网关的参数的请求方法的类型</param>
        public string GetGatewayParameterValue(string gatewayParameterName, HttpMethod httpMethod)
        {
            return gateway.GetGatewayParameterValue(gatewayParameterName, httpMethod);
        }

        #endregion

    }
}