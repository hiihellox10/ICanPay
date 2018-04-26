using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// 支付网关的抽象基类
    /// </summary>
    public abstract class GatewayBase
    {

        #region 私有字段

        Merchant merchant;
        Order order;
        Dictionary<string, GatewayParameter> gatewayParameterList;
        const string formItem = "<input type='hidden' name='{0}' value='{1}'>";

        #endregion


        #region 构造函数


        protected GatewayBase() : this(new Dictionary<string, GatewayParameter>())
        {
        }


        protected GatewayBase(Dictionary<string, GatewayParameter> gatewayParameterList)
        {
            this.gatewayParameterList = gatewayParameterList;
        }

        #endregion


        #region 属性


        /// <summary>
        /// 商家数据
        /// </summary>
        public Merchant Merchant
        {
            get
            {
                if (merchant == null)
                {
                    merchant = new Merchant();
                }

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
                if (order == null)
                {
                    order = new Order();
                }

                return order;
            }

            set
            {
                order = value;
            }
        }


        /// <summary>
        /// 支付网关的类型
        /// </summary>
        public abstract GatewayType GatewayType
        {
            get;
        }


        /// <summary>
        /// 支付通知的返回方式
        /// </summary>
        /// <remarks>
        /// 目前的支付网关在支付成功后会以Get或Post方式将支付结果返回给商户。
        /// POST方式的返回一般是通过网关服务器发送，这里可能要求商户输出字符标记表示已成功接收到支付结果。
        /// 而另一种是通过GET方式将用户返回到商户的网站，这时如果以POST数据时的方式来处理将会输出标记已成功接收的字符串。
        /// 如果这样用户会感到很奇怪，这时显示支付成功的页面将会更合适。所以可以通过PaymentNotifyMethod属性来判断
        /// 支付结果的发送方式，以决定是应该输出标记已成功接收的字符串还是向用户显示支付成功的页面。
        /// 服务器发送通知时属性为ServerNotify，如果是用户通过浏览器跳转到接收网关通知的页面属性为AutoReturn。
        /// </remarks>
        public abstract PaymentNotifyMethod PaymentNotifyMethod
        {
            get;
        }



        /// <summary>
        /// 支付网关的Get、Post数据的集合，Get方式传入QueryString的值均为未解码。
        /// </summary>
        public ICollection<GatewayParameter> GatewayParameterData
        {
            get
            {
                return gatewayParameterList.Values;
            }
        }


        #endregion


        #region 方法


        /// <summary>
        /// 创建Form HTML代码
        /// </summary>
        /// <param name="url">网关的Url</param>
        protected string GetFormHtml(string url)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<body>");
            html.AppendLine(string.Format("<form name='Gateway' method='post' action ='{0}'>", url));
            foreach (GatewayParameter item in GatewayParameterData)
            {
                if (item.HttpMethod == HttpMethod.None || item.HttpMethod  == HttpMethod.Post)
                {
                    html.AppendLine(string.Format(formItem, item.Name, item.Value));
                }
            }
            html.AppendLine("</form>");
            html.AppendLine("<script language='javascript' type='text/javascript'>");
            html.AppendLine("document.Gateway.submit();");
            html.AppendLine("</script>");
            html.AppendLine("</body>");

            return html.ToString();
        }


        /// <summary>
        /// 获得按字母升序排序后的网关参数的集合
        /// </summary>
        /// <returns></returns>
        protected SortedDictionary<string, string> GetSortedGatewayParameter()
        {
            SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
            foreach (GatewayParameter item in GatewayParameterData)
            {
                sortedDictionary.Add(item.Name, item.Value);
            }

            return sortedDictionary;
        }


        /// <summary>
        /// 检验网关返回的通知，验证订单是否支付成功。
        /// </summary>
        public abstract bool ValidateNotify();


        /// <summary>
        /// 当接收到支付网关通知并验证无误时，按照支付网关要求格式输出表示成功接收到网关通知的字符串。
        /// </summary>
        public abstract void WriteSucceedFlag();


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        public void SetGatewayParameterValue(string gatewayParameterName, object gatewayParameterValue)
        {
            SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue, HttpMethod.None);
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        public void SetGatewayParameterValue(string gatewayParameterName, string gatewayParameterValue)
        {
            SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue, HttpMethod.None);
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        /// <param name="httpMethod">网关的参数的请求方法的类型</param>
        public void SetGatewayParameterValue(string gatewayParameterName, object gatewayParameterValue, HttpMethod httpMethod)
        {
            SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue.ToString(), httpMethod);
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        /// <param name="httpMethod">网关的参数的请求方法的类型</param>
        public void SetGatewayParameterValue(string gatewayParameterName, string gatewayParameterValue, HttpMethod httpMethod)
        {
            if (gatewayParameterList.ContainsKey(gatewayParameterName))
            {
                GatewayParameter gatewayParameter = gatewayParameterList[gatewayParameterName];
                if (string.Compare(gatewayParameter.Value, gatewayParameterValue) != 0 ||
                                   gatewayParameter.HttpMethod != httpMethod)
                {
                    gatewayParameter.HttpMethod = httpMethod;
                    gatewayParameter.Value = gatewayParameterValue;
                }
            }
            else
            {
                GatewayParameter gatewayParameter = new GatewayParameter(gatewayParameterName, gatewayParameterValue, httpMethod);
                gatewayParameterList.Add(gatewayParameterName, gatewayParameter);
            }
        }


        /// <summary>
        /// 获得网关的参数值。没有参数值时返回空字符串，Get方式的值均为未解码。
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        public string GetGatewayParameterValue(string gatewayParameterName)
        {
            return GetGatewayParameterValue(gatewayParameterName, HttpMethod.None);
        }


        /// <summary>
        /// 获得网关的参数值。没有参数值时返回空字符串，Get方式的值均为未解码。
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        /// <param name="httpMethod">网关的数据的请求方法的类型</param>
        public string GetGatewayParameterValue(string gatewayParameterName, HttpMethod httpMethod)
        {
            if (gatewayParameterList.ContainsKey(gatewayParameterName))
            {
                GatewayParameter gatewayParameter = gatewayParameterList[gatewayParameterName];
                if (httpMethod == HttpMethod.None || httpMethod == gatewayParameter.HttpMethod)
                {
                    return gatewayParameter.Value;
                }

            }

            return string.Empty;
        }


        /// <summary>
        /// 清除所有网关的参数
        /// </summary>
        protected void ClearAllGatewayParameter()
        {
            gatewayParameterList.Clear();
        }

        #endregion

    }
}
