using System.Collections.Generic;
using System.Linq;

namespace ICanPay
{
    /// <summary>
    /// 网关返回的支付通知数据的接受
    /// </summary>
    public class PaymentNotify
    {

        #region 私有字段

        ICollection<Merchant> merchantList;

        #endregion


        #region 构造函数

        /// <summary>
        /// 初始化接受阅读反馈的PaymentNotify
        /// </summary>
        public PaymentNotify() :
            this(new List<Merchant>())
        {
        }

        /// <summary>
        /// 初始化接受阅读反馈的PaymentNotify
        /// </summary>
        /// <param name="merchant">商户数据</param>
        public PaymentNotify(Merchant merchant)
        {
            merchantList = new List<Merchant>();
            AddMerchant(merchant);
        }


        /// <summary>
        /// 初始化接受阅读反馈的PaymentNotify
        /// </summary>
        /// <param name="merchantList">用于验证支付网关返回数据的商户数据列表</param>
        public PaymentNotify(ICollection<Merchant> merchantList)
        {
            this.merchantList = merchantList;
        }

        #endregion


        #region 事件

        /// <summary>
        /// 网关返回的支付通知验证失败时触发
        /// </summary>
        public event PaymentFailedEventHandler PaymentFailed;


        /// <summary>
        /// 网关返回的支付通知验证成功时触发
        /// </summary>
        public event PaymentSucceedEventHandler PaymentSucceed;


        /// <summary>
        /// 返回通知消息的网关无法识别时触发
        /// </summary>
        public event UnknownGatewayEventHandler UnknownGateway;

        #endregion


        #region 方法

        protected virtual void OnPaymentFailed(PaymentFailedEventArgs e) => PaymentFailed?.Invoke(this, e);


        protected virtual void OnPaymentSucceed(PaymentSucceedEventArgs e) => PaymentSucceed?.Invoke(this, e);


        protected virtual void OnUnknownGateway(UnknownGatewayEventArgs e) => UnknownGateway?.Invoke(this, e);


        /// <summary>
        /// 接收并验证网关的支付通知
        /// </summary>
        public void Received()
        {
            GatewayBase gateway = NotifyProcess.GetGateway();
            if (gateway.GatewayType != GatewayType.None)
            {
                gateway.Merchant = GetMerchant(gateway.GatewayType);
                if (gateway.ValidateNotify())
                {
                    OnPaymentSucceed(new PaymentSucceedEventArgs(gateway));
                    gateway.WriteSucceedFlag();
                }
                else
                {
                    OnPaymentFailed(new PaymentFailedEventArgs(gateway));
                }
            }
            else
            {
                OnUnknownGateway(new UnknownGatewayEventArgs(gateway));
            }
        }


        /// <summary>
        /// 添加商户数据。与添加的商户数据重复的网关将会被删除
        /// </summary>
        /// <param name="merchant">商户数据</param>
        public void AddMerchant(Merchant merchant)
        {
            RemoveMerchant(merchant.GatewayType);
            merchantList.Add(merchant);
        }


        /// <summary>
        /// 获得商户数据。网关存在多个商户数据时返回第一个，无法找到返回null
        /// </summary>
        /// <param name="gatewayType">网关类型</param>
        /// <returns>网关存在多个商户数据时返回第一个，无法找到返回null</returns>
        public Merchant GetMerchant(GatewayType gatewayType)
        {
            return merchantList.FirstOrDefault(m => m.GatewayType == gatewayType);
        }


        /// <summary>
        /// 删除商户数据
        /// </summary>
        /// <param name="gatewayType">网关类型</param>
        public void RemoveMerchant(GatewayType gatewayType)
        {
            Merchant removeMerchant = merchantList.FirstOrDefault(m => m.GatewayType == gatewayType);
            if (removeMerchant != null)
            {
                merchantList.Remove(removeMerchant);
            }
        }

        #endregion

    }

    #region 委托

    /// <summary>
    /// 支付成功时引发事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void PaymentFailedEventHandler(object sender, PaymentFailedEventArgs e);


    /// <summary>
    /// 支付失败时引发事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void PaymentSucceedEventHandler(object sender, PaymentSucceedEventArgs e);


    /// <summary>
    /// 无法识别的网关时引发事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void UnknownGatewayEventHandler(object sender, UnknownGatewayEventArgs e);

    #endregion

}