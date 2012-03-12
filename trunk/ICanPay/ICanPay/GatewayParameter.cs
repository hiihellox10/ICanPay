using System;

namespace ICanPay
{
    /// <summary>
    /// 支付网关的Get、Post数据
    /// </summary>
    public class GatewayParameter
    {

        #region 私有字段

        string parameterName;

        #endregion


        #region 构造函数

        public GatewayParameter()
        {
        }


        public GatewayParameter(string parameterName, string parameterValue, GatewayParameterType parameterType)
        {
            this.parameterName = parameterName;
            ParameterValue = parameterValue;
            ParameterType = parameterType;
        }

        #endregion


        #region 属性

        /// <summary>
        /// 参数名
        /// </summary>
        public string ParameterName
        {
            get
            {
                return parameterName;
            }

            set
            {
                if (string.IsNullOrEmpty(parameterName))
                {
                    throw new ArgumentNullException("parameterName", "参数名不能为空");
                }

                parameterName = value;
            }
        }


        /// <summary>
        /// 参数值
        /// </summary>
        public string ParameterValue { get; set; }


        /// <summary>
        /// 参数类型
        /// </summary>
        public GatewayParameterType ParameterType { get; set; }

        #endregion

    }
}
