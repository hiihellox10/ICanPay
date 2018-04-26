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
        /// <param name="gatewayParameterList">网关通知的数据集合</param>
        public NullGateway(Dictionary<string, GatewayParameter> gatewayParameterList)
            : base(gatewayParameterList)
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

        public override bool ValidateNotify()
        {
            return false;
        }

        public override void WriteSucceedFlag()
        {
        }

        #endregion

    }
}
