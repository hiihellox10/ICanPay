using System;
using System.Collections.Generic;

namespace ICanPay.Providers
{
    /// <summary>
    /// 未知网关
    /// </summary>
    public class NullGateway : GatewayBase
    {

        #region 构造函数

        /// <summary>
        /// 初始化未知网关
        /// </summary>
        public NullGateway()
        {
        }


        /// <summary>
        /// 初始化未知网关
        /// </summary>
        /// <param name="gatewayParameterData">网关通知的数据集合</param>
        public NullGateway(List<GatewayParameter> gatewayParameterData)
            : base(gatewayParameterData)
        {
        }

        #endregion


        #region 属性

        public override GatewayType GatewayType
        {
            get { return GatewayType.None; }
        }


        public override PaymentNotifyMethod PaymentNotifyMethod
        {
            get { return PaymentNotifyMethod.None; }
        }

        #endregion


        #region 方法

        protected override bool CheckNotifyData()
        {
            return false;
        }

        public override void WriteSucceedFlag()
        {
        }

        #endregion

    }
}
