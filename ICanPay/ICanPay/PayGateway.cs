using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// 支付网关的抽象接口
    /// </summary>
    public abstract class PayGateway
    {

        #region 私有字段

        Merchant merchant;
        Order order;
        List<GatewayParameter> gatewayParameterData;
        const string formItem = "<input type='hidden' name='{0}' value='{1}'>";

        #endregion


        #region 构造函数


        protected PayGateway()
        {
        }


        protected PayGateway(List<GatewayParameter> gatewayParameterData)
        {
            this.gatewayParameterData = gatewayParameterData;
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
        /// 支付网关的Get、Post数据的集合。Get方式传入QueryString的值均为未解码
        /// </summary>
        public List<GatewayParameter> GatewayParameterData
        {
            get
            {
                if (gatewayParameterData == null)
                {
                    gatewayParameterData = new List<GatewayParameter>();
                }

                return gatewayParameterData;
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
            html.AppendLine("<form name='Gateway' method='post' action ='" + url + "'>");
            foreach (GatewayParameter item in GatewayParameterData)
            {
                if ((item.Type & GatewayParameterType.Post) == GatewayParameterType.Post)
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
        /// 验证订单是否支付成功
        /// </summary>
        public bool ValidateNotify()
        {
            if (CheckNotifyData())
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        /// <remarks>
        /// 设置的参数存在时，如果参数的值不一致则保存新的参数值。
        /// </remarks>
        public void SetGatewayParameterValue(string gatewayParameterName, object gatewayParameterValue)
        {
            SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue, GatewayParameterType.Both);
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        /// <remarks>
        /// 设置的参数存在时，如果参数的值不一致则保存新的参数值。
        /// </remarks>
        public void SetGatewayParameterValue(string gatewayParameterName, string gatewayParameterValue)
        {
            SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue, GatewayParameterType.Both);
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        /// <param name="gatewayParameterType">网关的参数的类型</param>
        /// <remarks>
        /// 设置的参数存在时，如果参数的值不一致则保存新的参数值。
        /// </remarks>
        public void SetGatewayParameterValue(string gatewayParameterName, object gatewayParameterValue, GatewayParameterType gatewayParameterType)
        {
            SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue.ToString(), gatewayParameterType);
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        /// <param name="gatewayParameterValue">网关的参数值</param>
        /// <param name="gatewayParameterType">网关的参数的类型</param>
        /// <remarks>
        /// 设置的参数存在时，如果参数的值不一致则保存新的参数值。
        /// </remarks>
        public void SetGatewayParameterValue(string gatewayParameterName, string gatewayParameterValue, GatewayParameterType gatewayParameterType)
        {
            GatewayParameter existsParam = GatewayParameterData.SingleOrDefault(p => string.Compare(p.Name, gatewayParameterName) == 0);
            if (existsParam == null)
            {
                GatewayParameter param = new GatewayParameter(gatewayParameterName, gatewayParameterValue, gatewayParameterType);
                GatewayParameterData.Add(param);
            }
            else
            {
                if (string.Compare(existsParam.Value, gatewayParameterValue) == 0)
                {
                    existsParam.Type = existsParam.Type | gatewayParameterType;
                }
                else
                {
                    existsParam.Type = gatewayParameterType;
                    existsParam.Value = gatewayParameterValue;
                }
            }
        }


        /// <summary>
        /// 获得网关的参数值。没有参数值时返回空字符串，Get方式的值均为未解码
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        public string GetGatewayParameterValue(string gatewayParameterName)
        {
            return GetGatewayParameterValue(gatewayParameterName, GatewayParameterType.Both);
        }


        /// <summary>
        /// 获得网关的参数值。没有参数值时返回空字符串，Get方式的值均为未解码
        /// </summary>
        /// <param name="gatewayParameterName">网关的参数名称</param>
        /// <param name="gatewayParameterType">网关的数据的接收、发送方式</param>
        public string GetGatewayParameterValue(string gatewayParameterName, GatewayParameterType gatewayParameterType)
        {
            GatewayParameter parameter = GatewayParameterData.SingleOrDefault(p => string.Compare(p.Name, gatewayParameterName) == 0 &&
                                                                                   (p.Type & gatewayParameterType) == p.Type);
            if(parameter != null)
            {
                return parameter.Value;
            }

            return string.Empty;
        }


        /// <summary>
        /// 根据参数顺序，获取数据项中的值
        /// </summary>
        /// <param name="parmaName">参数名</param>
        protected string GetGatewayParameterValue(string[] parmaName)
        {
            StringBuilder valueBuilder = new StringBuilder();
            GatewayParameter parameter;
            foreach (string item in parmaName)
            {
                parameter = GatewayParameterData.SingleOrDefault(p => string.Compare(p.Name, item) == 0);
                if (parameter != null)
                {
                    valueBuilder.Append(parameter.Value);
                }
            }

            return valueBuilder.ToString();
        }


        /// <summary>
        /// 设置网关的数据
        /// </summary>
        /// <param name="gatewayParameterData">网关的数据的集合</param>
        protected void SetGatewayParameterData(List<GatewayParameter> gatewayParameterData)
        {
            this.gatewayParameterData = gatewayParameterData;
        }


        /// <summary>
        /// 检验网关返回的通知，确认订单是否支付成功
        /// </summary>
        protected abstract bool CheckNotifyData();


        /// <summary>
        /// 按网关要求格式输出成功接收到网关通知的标记字符
        /// </summary>
        public virtual void WriteSucceedFlag()
        {
        }

        #endregion

    }
}
