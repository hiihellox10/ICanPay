using System;
using System.Collections.Generic;
using System.Text;

namespace ICanPay
{
    /// <summary>
    /// �����Ľ����
    /// </summary>
    public class Order
    {
        double amount = 0.0;
        string orderid;

        /// <summary>
        /// �����ܽ���ԪΪ��λ�����磺1.00��1Ԫ����ҡ�0.01��1������ҡ�
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
                    throw new ArgumentNullException("Amount", "�������û������");
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
                    throw new ArgumentException("������������ڻ����0.01", "Amount");
                }
            }
        }

        /// <summary>
        /// ������Ż�����
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
                    throw new ArgumentNullException("OrderId", "�����������û������");
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
                    throw new ArgumentNullException("OrderId", "����������Ų���Ϊ��");
                }
            }
        }
    }
}
