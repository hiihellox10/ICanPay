using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web;

namespace ICanPay
{
    /// <summary>
    /// ֧�����صĳ���ӿ�
    /// </summary>
    public abstract class PayGateway
    {
        private Customer customer;
        private Merchant merchant;
        private Order order;
        private Dictionary<string, string> otherData;
        private IList<string> safeAddress;
        private bool validateServer;
        private const string formItem = "<input type='hidden' name='{0}' value='{1}'>";

        /// <summary>
        /// ��ʼ��˽������
        /// </summary>
        public PayGateway()
        {
            customer = new Customer();
            merchant = new Merchant();
            order = new Order();
            otherData = new Dictionary<string, string>();
            safeAddress = new List<string>();
            validateServer = false;
        }

        /// <summary>
        /// �ͻ�����
        /// </summary>
        public Customer Customer
        {
            get
            {
                return customer;
            }

            set
            {
                customer = value;
            }
        }

        /// <summary>
        /// �̼�����
        /// </summary>
        public Merchant Merchant
        {
            get
            {
                return merchant;
            }

            set
            {
                merchant = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public Order Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
            }
        }

        /// <summary>
        /// ֧�����ص�����
        /// </summary>
        abstract public GatewayType GatewayName
        {
            get;
        }

        /// <summary>
        /// �����е�����������
        /// </summary>
        public Dictionary<string, string> OtherData
        {
            get
            {
                return otherData;
            }
            set
            {
                otherData = value;
            }
        }

        /// <summary>
        /// ���ط�����IP������ͨ����鷢��֪ͨ���ݵ�IP�Ƿ����б����Լ�ǿ��ȫ��
        /// </summary>
        public IList<string> SafeAddress
        {
            get
            {
                return safeAddress;
            }
        }


        /// <summary>
        /// ��֤����֪ͨ��������IP�Ƿ�������IP�б��У��Լ�ǿ��ȫ�ԡ�Ĭ��false
        /// </summary>
        public bool ValidateNotifyHostServerAddress
        {
            get
            {
                return validateServer;
            }
            set
            {
                validateServer = value;
            }
        }


        /// <summary>
        /// ��֤����֪ͨ�������Ƿ��ڰ�ȫ������IP�б���
        /// </summary>
        private bool ValidateServerIP()
        {
            if (!ValidateNotifyHostServerAddress)
            {
                return true;
            }

            string serverIp = HttpContext.Current.Request.UserHostAddress;

            foreach (String ip in SafeAddress)
            {
                if (serverIp == ip)
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// ����Form HTML����
        /// </summary>
        /// <param name="parma">��Ҫ��ӵĲ���</param>
        /// <param name="url">���ص�Url</param>
        protected string GetForm(Dictionary<string, string> parma, string url)
        {
            StringBuilder html = new StringBuilder();

            html.AppendLine("<form name='Gateway' method='post'" + " action =" + url + ">");

            foreach (KeyValuePair<string, string> item in parma)
            {
                html.AppendLine(string.Format(formItem, item.Key, item.Value));
            }

            html.AppendLine("</form>");
            html.AppendLine("<script type='text/javascript'>window.document.Gateway.submit();</script>");

            return html.ToString();
        }


        /// <summary>
        /// ��֤�����Ƿ�֧���ɹ�
        /// </summary>
        public bool ValidateNotify()
        {
            if (ValidateServerIP())
            {
                return CheckNotifyData();
            }

            return false;
        }

        /// <summary>
        /// �������ط��ص�֪ͨ��ȷ�϶����Ƿ�֧���ɹ�
        /// </summary>
        abstract protected bool CheckNotifyData();
    }
}
