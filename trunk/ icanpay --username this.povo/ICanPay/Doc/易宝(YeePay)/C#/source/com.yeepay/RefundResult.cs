using System;

namespace com.yeepay
{
	/// <summary>
	/// RefundResult ��ժҪ˵����
	/// </summary>
	public class RefundResult
	{
		public string ReturnCode;
		public string ReturnAllPara;

		public RefundResult(string returnCode,string returnAllPara)
		{
			this.ReturnCode		= returnCode;
			this.ReturnAllPara	= returnAllPara;
		}
	}
}
