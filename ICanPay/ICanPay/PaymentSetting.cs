using System;
using System.Web;

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

        #region 构造函数

        public PaymentSetting()
        {
        }


        public PaymentSetting(Merchant merchant, Order order)
        {
            t.Merchant = merchant;
            t.Order = order;
        }

        #endregion


        #region 字段

        T t = new T();

        #endregion


        #region 属性

        /// <summary>
        /// 网关
        /// </summary>
        public T Gateway
        {
            get
            {
                return t;
            }
        }


        /// <summary>
        /// 商家数据
        /// </summary>
        public Merchant Merchant
        {
            get
            {
                return t.Merchant;
            }

            set
            {
                t.Merchant = value;
            }
        }


        /// <summary>
        /// 订单数据
        /// </summary>
        public Order Order
        {
            get
            {
                return t.Order;
            }

            set
            {
                t.Order = value;
            }
        }


        /// <summary>
        /// 是否已实现支付接口
        /// </summary>
        public bool CanPayment
        {
            get
            {
                IPaymentUrl url = t as IPaymentUrl;
                if (url != null)
                {
                    return true;
                }

                IPaymentForm form = t as IPaymentForm;
                if (form != null)
                {
                    return true;
                }

                return false;
            }
        }


        /// <summary>
        /// 是否已实现查询接口
        /// </summary>
        public bool CanQuery
        {
            get
            {
                IQueryUrl url = t as IQueryUrl;
                if (url != null)
                {
                    return true;
                }

                IQueryForm form = t as IQueryForm;
                if (form != null)
                {
                    return true;
                }

                return false;
            }
        }

        #endregion


        #region 方法

        /// <summary>
        /// 转到支付网站支付
        /// </summary>
        public void Payment()
        {
            IPaymentUrl url = t as IPaymentUrl;
            if (url != null)
            {
                HttpContext.Current.Response.Redirect(url.BuildPaymentUrl());
                return;
            }

            IPaymentForm form = t as IPaymentForm;
            if (form != null)
            {
                HttpContext.Current.Response.Write(form.BuildPaymentForm());
                return;
            }

            throw new NotSupportedException(t.GatewayType + " 没有实现支付接口");
        }


        /// <summary>
        /// 查询订单
        /// </summary>
        /// <remarks>
        /// 订单的查询通知数据通过跟支付通知一样的形式反回。用处理网关通知一样的方法接受查询订单的数据
        /// </remarks>
        public void Query()
        {
            IQueryUrl url = t as IQueryUrl;
            if (url != null)
            {
                HttpContext.Current.Response.Redirect(url.BuildQueryUrl());
                return;
            }

            IQueryForm form = t as IQueryForm;
            if (form != null)
            {

                HttpContext.Current.Response.Write(form.BuildQueryForm());
                return;
            }

            throw new NotSupportedException(t.GatewayType + " 没有实现查询接口");
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        public void SetGatewayParameterValue(string gatewayParameterName, string gatewayParameterValue)
        {
            Gateway.SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue);
        }

        #endregion

    }
}
