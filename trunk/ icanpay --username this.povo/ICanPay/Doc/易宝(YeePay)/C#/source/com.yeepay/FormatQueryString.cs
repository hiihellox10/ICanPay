using System;

namespace com.yeepay
{
	/// <summary>
	/// formatQueryString ��ժҪ˵����
	/// </summary>
	public class FormatQueryString
	{
		public FormatQueryString()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public static string GetQueryString(string strArgName,string strUrl)
		{
			//����Request��������룡����
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
