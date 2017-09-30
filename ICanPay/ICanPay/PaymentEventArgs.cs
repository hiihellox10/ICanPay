using System;
using System.Collections.Generic;

namespace ICanPay
{
    /// <summary>
    /// ֧���¼����ݵĻ���
    /// </summary>
    public abstract class PaymentEventArgs : EventArgs
    {

        #region ˽���ֶ�

        protected GatewayBase gateway;
        string notifyServerHostAddress;

        #endregion


        #region ���캯��

        /// <summary>
        /// ��ʼ��֧���¼����ݵĻ���
        /// </summary>
        /// <param name="gateway">֧������</param>
        public PaymentEventArgs(GatewayBase gateway)
        {
            this.gateway = gateway;

#if NET35
            notifyServerHostAddress = HttpContext.Current.Request.UserHostAddress;
#elif NETSTANDARD2_0
            notifyServerHostAddress = HttpContext.Current.Request.Host.Value;
#endif
        }


        #endregion


        #region ����

        /// <summary>
        /// ����֧��֪ͨ������IP��ַ
        /// </summary>
        public string NotifyServerHostAddress
        {
            get
            {
                return notifyServerHostAddress;
            }
        }


        /// <summary>
        /// ֧�����ص�Get��Post���ݵļ���
        /// </summary>
        public ICollection<GatewayParameter> GatewayParameterData
        {
            get
            {
                return gateway.GatewayParameterData;
            }
        }

        #endregion


        #region ����

        /// <summary>
        /// ������صĲ���ֵ��û�в���ֵʱ���ؿ��ַ�����Get��ʽ��ֵ��Ϊδ���롣
        /// </summary>
        /// <param name="gatewayParameterName">���صĲ�������</param>
        public string GetGatewayParameterValue(string gatewayParameterName)
        {
            return gateway.GetGatewayParameterValue(gatewayParameterName);
        }


        /// <summary>
        /// ������صĲ���ֵ��û�в���ֵʱ���ؿ��ַ�����Get��ʽ��ֵ��Ϊδ���롣
        /// </summary>
        /// <param name="gatewayParameterName">���صĲ�������</param>
        /// <param name="gatewayParameterRequestMethod">���ص����ݵ����󷽷�������</param>
        public string GetGatewayParameterValue(string gatewayParameterName, GatewayParameterRequestMethod gatewayParameterRequestMethod)
        {
            return gateway.GetGatewayParameterValue(gatewayParameterName, gatewayParameterRequestMethod);
        }

        #endregion

    }
}