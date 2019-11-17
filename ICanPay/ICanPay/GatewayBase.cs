using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace ICanPay
{
    /// <summary>
    /// 支付网关的抽象基类
    /// </summary>
    public abstract class GatewayBase
    {

        #region 私有字段

        private Merchant _merchant;
        private Order _order;
        private Dictionary<string, GatewayParameter> _gatewayParameterList;
        private const string FormItem = "<input type='hidden' name='{0}' value='{1}'>";

        #endregion


        #region 构造函数


        protected GatewayBase() : this(new Dictionary<string, GatewayParameter>())
        {
        }


        protected GatewayBase(Dictionary<string, GatewayParameter> gatewayParameterList)
        {
            InitHttpContextEncoding();

            _gatewayParameterList = gatewayParameterList;
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
                if (_merchant == null)
                {
                    _merchant = new Merchant();
                }

                return _merchant;
            }

            set
            {
                _merchant = value;
            }
        }


        /// <summary>
        /// 订单数据
        /// </summary>
        public Order Order
        {
            get
            {
                if (_order == null)
                {
                    _order = new Order();
                }

                return _order;
            }

            set
            {
                _order = value;
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
        /// 支付网关的Get、Post数据的集合。
        /// </summary>
        public ICollection<GatewayParameter> GatewayParameterData
        {
            get
            {
                return _gatewayParameterList.Values;
            }
        }


        /// <summary>
        /// 支付网关所使用的编码
        /// </summary>
        /// <remarks>
        /// 请阅读支付网关文档设置正确的编码，默认为 UTF8。错误的编码会导致出现乱码。
        /// </remarks>
        protected virtual Encoding PageEncoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }


        #endregion


        #region 方法

        /// <summary>
        /// 初始化 HttpContext 的编码，创建、查询订单，接收网关通知的页面将使用设置的编码。
        /// </summary>
        /// <remarks>
        /// 支付平台所使用的编码请阅读支付平台的开发文档，设置 HttpContext 的目的是为了解决不同支付平台的编码不一致的问题。
        /// 如果没有正确设置支付平台所使用的编码会导致出现乱码。
        /// </remarks>
        private void InitHttpContextEncoding()
        {
            HttpContext.Current.Request.ContentEncoding = PageEncoding;
            HttpContext.Current.Response.ContentEncoding = PageEncoding;
        }


        /// <summary>
        /// 创建Form HTML代码
        /// </summary>
        /// <param name="url">网关的Url</param>
        protected string GetFormHtml(string url)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<body>");
            htmlBuilder.AppendLine(string.Format("<form name='Gateway' method='post' action ='{0}'>", url));
            foreach (GatewayParameter item in GatewayParameterData)
            {
                if (item.HttpMethod == HttpMethod.None || item.HttpMethod  == HttpMethod.Post)
                {
                    htmlBuilder.AppendLine(string.Format(FormItem, item.Name, item.Value));
                }
            }
            htmlBuilder.AppendLine("</form>");
            htmlBuilder.AppendLine("<script language='javascript' type='text/javascript'>");
            htmlBuilder.AppendLine("document.Gateway.submit();");
            htmlBuilder.AppendLine("</script>");
            htmlBuilder.AppendLine("</body>");

            return htmlBuilder.ToString();
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
        /// 创建网关参数的查询字符串。
        /// </summary>
        /// <param name="parameterList">网关参数的集合</param>
        protected string BuildQueryString(ICollection<GatewayParameter> parameterList)
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            foreach (GatewayParameter item in parameterList)
            {
                queryStringBuilder.AppendFormat("{0}={1}&", item.Name, item.Value);
            }

            return queryStringBuilder.ToString().TrimEnd('&');
        }


        /// <summary>
        /// 创建网关参数的查询字符串。
        /// </summary>
        /// <param name="parameterDictionary">网关参数的 Dictionary</param>
        protected string BuildQueryString(IDictionary<string, string> parameterDictionary)
        {
            StringBuilder queryStringBuilder = new StringBuilder();
            foreach (var item in parameterDictionary)
            {
                queryStringBuilder.AppendFormat("{0}={1}&", item.Key, item.Value);
            }

            return queryStringBuilder.ToString().TrimEnd('&');
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
        /// <param name="gatewayParameterName">网关参数的名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        protected void SetGatewayParameterValue(string gatewayParameterName, object gatewayParameterValue)
        {
            SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue, HttpMethod.None);
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterName">网关参数的名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        protected void SetGatewayParameterValue(string gatewayParameterName, string gatewayParameterValue)
        {
            SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue, HttpMethod.None);
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterName">网关参数的名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        /// <param name="httpMethod">网关的参数的请求方法的类型</param>
        protected void SetGatewayParameterValue(string gatewayParameterName, object gatewayParameterValue, HttpMethod httpMethod)
        {
            SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue.ToString(), httpMethod);
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterName">网关参数的名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        /// <param name="httpMethod">网关的参数的请求方法的类型</param>
        protected void SetGatewayParameterValue(string gatewayParameterName, string gatewayParameterValue, HttpMethod httpMethod)
        {
            if (_gatewayParameterList.ContainsKey(gatewayParameterName))
            {
                GatewayParameter gatewayParameter = _gatewayParameterList[gatewayParameterName];
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
                _gatewayParameterList.Add(gatewayParameterName, gatewayParameter);
            }
        }


        /// <summary>
        /// 获得网关的参数值，参数不存在时返回空字符串。
        /// </summary>
        /// <param name="gatewayParameterName">网关参数的名称</param>
        public string GetGatewayParameterValue(string gatewayParameterName)
        {
            return GetGatewayParameterValue<string>(gatewayParameterName, HttpMethod.None);
        }


        /// <summary>
        /// 获得网关的参数值，参数不存在时返回空字符串。
        /// </summary>
        /// <param name="gatewayParameterName">网关参数的名称</param>
        /// <param name="httpMethod">网关数据请求方法的类型</param>
        public string GetGatewayParameterValue(string gatewayParameterName, HttpMethod httpMethod)
        {
            return GetGatewayParameterValue<string>(gatewayParameterName, httpMethod);
        }


        /// <summary>
        /// 获得网关的参数值，参数不存在时返回空字符串。
        /// </summary>
        /// <typeparam name="T">返回的数据类型</typeparam>
        /// <param name="gatewayParameterName">网关参数的名称</param>
        public T GetGatewayParameterValue<T>(string gatewayParameterName)
        {
            return GetGatewayParameterValue<T>(gatewayParameterName, HttpMethod.None);
        }


        /// <summary>
        /// 获得网关的参数值，参数不存在时返回空字符串。
        /// </summary>
        /// <typeparam name="T">返回的数据类型</typeparam>
        /// <param name="gatewayParameterName">网关参数的名称</param>
        /// <param name="httpMethod">网关数据请求方法的类型</param>
        public T GetGatewayParameterValue<T>(string gatewayParameterName, HttpMethod httpMethod)
        {
            string gatewayParameterValue = string.Empty;
            if (_gatewayParameterList.ContainsKey(gatewayParameterName))
            {
                GatewayParameter gatewayParameter = _gatewayParameterList[gatewayParameterName];
                if (httpMethod == HttpMethod.None || httpMethod == gatewayParameter.HttpMethod)
                {
                    gatewayParameterValue = gatewayParameter.Value;
                }
            }

            return (T)Convert.ChangeType(gatewayParameterValue, typeof(T));
        }


        /// <summary>
        /// 比较网关参数的值与指定的值是否一致
        /// </summary>
        /// <typeparam name="T">比较的值的类型</typeparam>
        /// <param name="gatewayParameterName">网关参数的名称</param>
        /// <param name="compareValue">比较的值</param>
        protected bool CompareGatewayParameterValue<T>(string gatewayParameterName, T compareValue)
        {
            return CompareGatewayParameterValue(gatewayParameterName, HttpMethod.None, compareValue);
        }


        /// <summary>
        /// 比较网关参数的值与指定的值是否一致
        /// </summary>
        /// <typeparam name="T">比较的值的类型</typeparam>
        /// <param name="gatewayParameterName">网关参数的名称</param>
        /// <param name="httpMethod">网关数据请求方法的类型</param>
        /// <param name="compareValue">比较的值</param>
        protected bool CompareGatewayParameterValue<T>(string gatewayParameterName, HttpMethod httpMethod, T compareValue)
        {
            T gatewayParameterValue = GetGatewayParameterValue<T>(gatewayParameterName, httpMethod);

            return gatewayParameterValue.Equals(compareValue);
        }


        /// <summary>
        /// 清除所有网关的参数
        /// </summary>
        protected void ClearAllGatewayParameter()
        {
            _gatewayParameterList.Clear();
        }


        #endregion

    }
}
