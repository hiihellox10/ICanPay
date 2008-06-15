using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Web;

namespace ICanPay
{
    /// <summary>
    /// ���ط��ص�����
    /// </summary>
    public class PaymentNotify
    {
        private PayGateway gateway;


        /// <summary>
        /// ���ش������
        /// </summary>
        public PaymentNotify()
        {
        }


        /// <summary>
        /// ����֪ͨ�����صĴ�����
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
        /// �Ƿ��н��յ���ȷ������֪ͨ
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
