using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web;

namespace ICanPay
{
    /// <summary>
    /// 支付网关的抽象接口
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
        /// 初始化私有数据
        /// </summary>
        protected PayGateway()
        {
            customer = new Customer();
            merchant = new Merchant();
            order = new Order();
            otherData = new Dictionary<string, string>();
            safeAddress = new List<string>();
        }

        /// <summary>
        /// 客户数据
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
        /// 商家数据
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
        /// 订单数据
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
        /// 支付网关的名称
        /// </summary>
        abstract public GatewayType GatewayName
        {
            get;
        }

        /// <summary>
        /// 网关中的其他数据项
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
        /// 网关服务器IP。可以通过检查发送通知数据的IP是否在列表中以加强安全性
        /// </summary>
        public IList<string> SafeAddress
        {
            get
            {
                return safeAddress;
            }
        }


        /// <summary>
        /// 验证发送通知服务器的IP是否在网关IP列表中，以加强安全性。默认false
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
        /// 验证发送通知的网关是否在安全服务器IP列表中
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
        /// 创建Form HTML代码
        /// </summary>
        /// <param name="parma">需要添加的参数</param>
        /// <param name="url">网关的Url</param>
        protected static string GetForm(Dictionary<string, string> parma, string url)
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
        /// 验证订单是否支付成功
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
        /// 检验网关返回的通知，确认订单是否支付成功
        /// </summary>
        abstract protected bool CheckNotifyData();
    }
}
