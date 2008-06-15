using System;

namespace com.yeepay
{
	/// <summary>
	/// formatQueryString 的摘要说明。
	/// </summary>
	public class FormatQueryString
	{
		public FormatQueryString()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public static string GetQueryString(string strArgName,string strUrl)
		{
			//处理Request，解决乱码！！！
			strUrl = strUrl.Replace("?","&");
			string strArgValue = "";
			string[] strList = strUrl.Split('&');
			int intCount = strList.Length;
			for (int i = 0; i < intCount; i++)
			{
				int intPos = strList[i].ToString().IndexOf("=");
				if (intPos == -1) continue;
				string strListArgName = strList[i].ToString().Substring(0, intPos);
				if (strListArgName == strArgName)
				{
					strArgValue = strList[i].ToString().Substring(intPos + 1);
				}
			}
			strArgValue=System.Web.HttpUtility.UrlDecode(strArgValue,System.Text.Encoding.GetEncoding("gb2312"));
			return strArgValue;
		}

	}
}
