using System;
using System.Collections.Generic;

namespace ICanPay
{
    /// <summary>
    /// 支付事件数据的基类
    /// </summary>
    public abstract class PaymentEventArgs : EventArgs
    {

        #region 字段

        private string _notifyServerHostAddress;
        protected GatewayBase _gateway;

        #endregion


        #region 构造函数

        /// <summary>
        /// 初始化支付事件数据的基类
        /// </summary>
        /// <param name="gateway">支付网关</param>
        public PaymentEventArgs(GatewayBase gateway)
        {
            this._gateway = gateway;
            _notifyServerHostAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
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
                return _notifyServerHostAddress;
            }
        }


        /// <summary>
        /// 支付网关的Get、Post数据的集合
        /// </summary>
        public ICollection<GatewayParameter> GatewayParameterData
        {
            get
            {
                return _gateway.GatewayParameterData;
            }
        }

        #endregion


        #region 方法

        /// <summary>
        /// 获得网关的参数值，参数不存在时返回空字符串。
        /// </summary>
        /// <param name="gatewayParameterName">网关参数的名称</param>
        public string GetGatewayParameterValue(string gatewayParameterName)
        {
            return _gateway.GetGatewayParameterValue(gatewayParameterName);
        }


        /// <summary>
        /// 获得网关的参数值，参数不存在时返回空字符串。
        /// </summary>
        /// <param name="gatewayParameterName">网关参数的名称</param>
        /// <param name="httpMethod">网关的参数的请求方法的类型</param>
        public string GetGatewayParameterValue(string gatewayParameterName, HttpMethod httpMethod)
        {
            return _gateway.GetGatewayParameterValue(gatewayParameterName, httpMethod);
        }

        /// <summary>
        /// 获得网关的参数值，参数不存在时返回空字符串。
        /// </summary>
        /// <typeparam name="T">返回的数据类型</typeparam>
        /// <param name="gatewayParameterName">网关参数的名称</param>
        public T GetGatewayParameterValue<T>(string gatewayParameterName)
        {
            return _gateway.GetGatewayParameterValue<T>(gatewayParameterName);
        }


        /// <summary>
        /// 获得网关的参数值，参数不存在时返回空字符串。
        /// </summary>
        /// <typeparam name="T">返回的数据类型</typeparam>
        /// <param name="gatewayParameterName">网关参数的名称</param>
        /// <param name="httpMethod">网关数据请求方法的类型</param>
        public T GetGatewayParameterValue<T>(string gatewayParameterName, HttpMethod httpMethod)
        {
            return _gateway.GetGatewayParameterValue<T>(gatewayParameterName, httpMethod);
        }


        #endregion

    }
}