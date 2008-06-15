using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// ������Ҫ֧���Ķ��������ݣ�����֧������HTML�����URL��ַ
    /// </summary>
    /// <remarks>
    /// ����Ҫ��֤���HTML�����ҳ��ΪGB2312���룬�������ؽ��յ������ݽ���������롣
    /// ��Web.config�е�configuration/system.web�ڵ�����<globalization requestEncoding="gb2312" responseEncoding="gb2312" />
    /// Ŀǰֻ��ʹ��RMB֧������������֧�����Ķ�������ؽӿ��ĵ��޸ġ�
    /// </remarks>
    public class PaymentSetting<T> where T : PayGateway, new()
    {
        private T t;


        /// <summary>
        /// �������ؽӿڵ�ʵ����
        /// </summary>
        public PaymentSetting()
        {
            this.t = new T();
        }

        /// <summary>
        /// ��������
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
