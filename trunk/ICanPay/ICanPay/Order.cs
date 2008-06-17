using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// 订单的金额、编号
    /// </summary>
    public class Order
    {
        double amount = 0.0;
        string orderid;

        /// <summary>
        /// 订单总金额，以元为单位。例如：1.00，1元人民币。0.01，1角人民币。
        /// </summary>
        public double Amount
        {
            get
            {
                if (amount >= 0.01)
                {
                    return amount;
                }
                else
                {
                    throw new ArgumentNullException("Amount", "订单金额没有设置");
                }
                
            }

            set
            {
                if (value >= 0.01)
                {
                    amount = value;
                }
                else
                {
                    throw new ArgumentException("订单金额必须大于或等于0.01", "Amount");
                }
            }
        }

        /// <summary>
        /// 订单编号或名称
        /// </summary>
        public string OrderId
        {
            get
            {
                if (orderid != null)
                {
                    return orderid;
                }
                else
                {
                    throw new ArgumentNullException("OrderId", "订单订单编号没有设置");
                }
            }
            set
            {
                if (value != null)
                {
                    orderid = value;
                }
                else
                {
                    throw new ArgumentNullException("OrderId", "订单订单编号不能为空");
                }
            }
        }
    }
}
