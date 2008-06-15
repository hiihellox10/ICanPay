using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// 设置需要支付的订单的数据，创建支付订单HTML代码或URL地址
    /// </summary>
    /// <remarks>
    /// 你需要保证输出HTML代码的页面为GB2312编码，否则网关接收到的数据将会产生乱码。
    /// 在Web.config中的configuration/system.web节点设置<globalization requestEncoding="gb2312" responseEncoding="gb2312" />
    /// 目前只能使用RMB支付，其他货币支付请阅读相关网关接口文档修改。
    /// </remarks>
    public class PaymentSetting<T> where T : PayGateway, new()
    {
        private T t;


        /// <summary>
        /// 传入网关接口的实现类
        /// </summary>
        public PaymentSetting()
        {
            this.t = new T();
        }

        /// <summary>
        /// 网关数据
        /// </summary>
        public T PayGateway
        {
            get
            {
                return this.t;
            }
        }
    }
}
