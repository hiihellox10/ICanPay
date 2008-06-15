using System;

namespace com.yeepay
{
	/// <summary>
	/// RefundResult 的摘要说明。
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
