using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// ֧�����صĳ������
    /// </summary>
    public abstract class GatewayBase
    {

        #region ˽���ֶ�

        Merchant merchant;
        Order order;
        ICollection<GatewayParameter> gatewayParameterData;
        const string formItem = "<input type='hidden' name='{0}' value='{1}'>";

        #endregion


        #region ���캯��


        protected GatewayBase() : this(new List<GatewayParameter>())
        {
        }


        protected GatewayBase(ICollection<GatewayParameter> gatewayParameterData)
        {
            this.gatewayParameterData = gatewayParameterData;
        }

        #endregion


        #region ����


        /// <summary>
        /// �̼�����
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
        /// ��������
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
        /// ֧�����ص�����
        /// </summary>
        public abstract GatewayType GatewayType
        {
            get;
        }


        /// <summary>
        /// ֧��֪ͨ�ķ��ط�ʽ
        /// </summary>
        /// <remarks>
        /// Ŀǰ��֧��������֧���ɹ������Get��Post��ʽ��֧��������ظ��̻���
        /// POST��ʽ�ķ���һ����ͨ�����ط��������ͣ��������Ҫ���̻�����ַ���Ǳ�ʾ�ѳɹ����յ�֧�������
        /// ����һ����ͨ��GET��ʽ���û����ص��̻�����վ����ʱ�����POST����ʱ�ķ�ʽ���������������ѳɹ����յ��ַ�����
        /// ��������û���е�����֣���ʱ��ʾ֧���ɹ���ҳ�潫������ʡ����Կ���ͨ��PaymentNotifyMethod�������ж�
        /// ֧������ķ��ͷ�ʽ���Ծ�����Ӧ���������ѳɹ����յ��ַ����������û���ʾ֧���ɹ���ҳ�档
        /// ����������֪ͨʱ����ΪServerNotify��������û�ͨ���������ת����������֪ͨ��ҳ������ΪAutoReturn��
        /// </remarks>
        public abstract PaymentNotifyMethod PaymentNotifyMethod
        {
            get;
        }



        /// <summary>
        /// ֧�����ص�Get��Post���ݵļ��ϡ�Get��ʽ����QueryString��ֵ��Ϊδ����
        /// </summary>
        public ICollection<GatewayParameter> GatewayParameterData
        {
            get
            {
                return gatewayParameterData;
            }
        }


        #endregion


        #region ����


        /// <summary>
        /// ����Form HTML����
        /// </summary>
        /// <param name="url">���ص�Url</param>
        protected string GetFormHtml(string url)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<body>");
            html.AppendLine("<form name='Gateway' method='post' action ='" + url + "'>");
            foreach (GatewayParameter item in GatewayParameterData)
            {
                if ((item.RequestMethod & GatewayParameterRequestMethod.Post) == GatewayParameterRequestMethod.Post)
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
        /// ��ð���ĸ�������������ز����ļ���
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
        /// ��֤�����Ƿ�֧���ɹ�
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
        /// �������ص�����
        /// </summary>
        /// <param name="gatewayParameterName">���صĲ�������</param>
        /// <param name="gatewayParameterValue">���صĲ���ֵ</param>
        /// <remarks>
        /// ���õĲ�������ʱ�����������ֵ��һ���򱣴��µĲ���ֵ��
        /// </remarks>
        public void SetGatewayParameterValue(string gatewayParameterName, object gatewayParameterValue)
        {
            SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue, GatewayParameterRequestMethod.Both);
        }


        /// <summary>
        /// �������ص�����
        /// </summary>
        /// <param name="gatewayParameterName">���صĲ�������</param>
        /// <param name="gatewayParameterValue">���صĲ���ֵ</param>
        /// <remarks>
        /// ���õĲ�������ʱ�����������ֵ��һ���򱣴��µĲ���ֵ��
        /// </remarks>
        public void SetGatewayParameterValue(string gatewayParameterName, string gatewayParameterValue)
        {
            SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue, GatewayParameterRequestMethod.Both);
        }


        /// <summary>
        /// �������ص�����
        /// </summary>
        /// <param name="gatewayParameterName">���صĲ�������</param>
        /// <param name="gatewayParameterValue">���صĲ���ֵ</param>
        /// <param name="gatewayParameterRequestMethod">���صĲ��������󷽷�������</param>
        /// <remarks>
        /// ���õĲ�������ʱ�����������ֵ��һ���򱣴��µĲ���ֵ��
        /// </remarks>
        public void SetGatewayParameterValue(string gatewayParameterName, object gatewayParameterValue, GatewayParameterRequestMethod gatewayParameterRequestMethod)
        {
            SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue.ToString(), gatewayParameterRequestMethod);
        }


        /// <summary>
        /// �������ص�����
        /// </summary>
        /// <param name="gatewayParameterName">���صĲ�������</param>
        /// <param name="gatewayParameterValue">���صĲ���ֵ</param>
        /// <param name="gatewayParameterRequestMethod">���صĲ��������󷽷�������</param>
        /// <remarks>
        /// �����õĲ�������ʱ�����������ֵ��һ�����޸�Ϊ�µĲ���ֵ��
        /// </remarks>
        public void SetGatewayParameterValue(string gatewayParameterName, string gatewayParameterValue, GatewayParameterRequestMethod gatewayParameterRequestMethod)
        {
            GatewayParameter existsParam = GatewayParameterData.SingleOrDefault(p => string.Compare(p.Name, gatewayParameterName) == 0);
            if (existsParam == null)
            {
                GatewayParameter param = new GatewayParameter(gatewayParameterName, gatewayParameterValue, gatewayParameterRequestMethod);
                GatewayParameterData.Add(param);
            }
            else
            {
                if (string.Compare(existsParam.Value, gatewayParameterValue) == 0)
                {
                    existsParam.RequestMethod = existsParam.RequestMethod | gatewayParameterRequestMethod;
                }
                else
                {
                    existsParam.RequestMethod = gatewayParameterRequestMethod;
                    existsParam.Value = gatewayParameterValue;
                }
            }
        }


        /// <summary>
        /// ������صĲ���ֵ��û�в���ֵʱ���ؿ��ַ�����Get��ʽ��ֵ��Ϊδ���롣
        /// </summary>
        /// <param name="gatewayParameterName">���صĲ�������</param>
        public string GetGatewayParameterValue(string gatewayParameterName)
        {
            return GetGatewayParameterValue(gatewayParameterName, GatewayParameterRequestMethod.Both);
        }


        /// <summary>
        /// ������صĲ���ֵ��û�в���ֵʱ���ؿ��ַ�����Get��ʽ��ֵ��Ϊδ���롣
        /// </summary>
        /// <param name="gatewayParameterName">���صĲ�������</param>
        /// <param name="gatewayParameterRequestMethod">���ص����ݵ����󷽷�������</param>
        public string GetGatewayParameterValue(string gatewayParameterName, GatewayParameterRequestMethod gatewayParameterRequestMethod)
        {
            GatewayParameter parameter = GatewayParameterData.SingleOrDefault(p => string.Compare(p.Name, gatewayParameterName) == 0 &&
                                                                                   (p.RequestMethod & gatewayParameterRequestMethod) == p.RequestMethod);
            if (parameter != null)
            {
                return parameter.Value;
            }

            return string.Empty;
        }


        /// <summary>
        /// �������ط��ص�֪ͨ��ȷ�϶����Ƿ�֧���ɹ�
        /// </summary>
        protected abstract bool CheckNotifyData();


        /// <summary>
        /// �����յ�֧������֪ͨ����֤����ʱ����֧������Ҫ���ʽ�����ʾ�ɹ����յ�����֪ͨ���ַ���
        /// </summary>
        public abstract void WriteSucceedFlag();

        #endregion

    }
}
