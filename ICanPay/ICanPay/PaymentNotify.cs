using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Web;

namespace ICanPay
{
    /// <summary>
    /// 网关返回的数据
    /// </summary>
    public class PaymentNotify
    {
        private PayGateway gateway;


        /// <summary>
        /// 网关处理程序
        /// </summary>
        public PaymentNotify()
        {
        }


        /// <summary>
        /// 发送通知的网关的处理类
        /// </summary>
        public PayGateway PayGateway
        {
            get
            {
                return gateway;
            }
            set
            {
                gateway = value;
            }
        }

        /// <summary>
        /// 是否有接收到正确的网关通知
        /// </summary>
        public bool HasNotify
        {
            get
            {
                if (gateway != null && gateway.ValidateNotify())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
