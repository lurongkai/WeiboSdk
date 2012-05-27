using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace NetDimension.Weibo
{
	[Serializable]
	public class WeiboException : System.Net.WebException
	{
		public string Request
		{
			get;
			private set;
		}

		public string ErrorCode
		{
			get;
			private set;
		}

		public string ErrorMessage
		{
			get;
			private set;
		}

		public string ErrorStatus
		{
			get;
			private set;
		}

		public WeiboException()
		{
		}
		public WeiboException(string message)
			: base(message)
		{
		}

		public WeiboException(string message, System.Net.WebException inner)
			: base(message, inner)
		{

		}

		public WeiboException(string code, string status, string request) :
			base(GetErrorMsg(code))
		{ 
			ErrorCode = code;
			ErrorStatus = status;
			Request =request;
			ErrorMessage = GetErrorMsg(code);
		}

		protected WeiboException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context)
		{

		}

		private static string GetErrorMsg(string errorCode)
		{
			//GO http://open.weibo.com/wiki/Error_code
			Dictionary<string, string> ErrorMsgBag = new Dictionary<string, string>
			{
				{"0","unknown error:未知错误"},
				{"1","network error:网络问题"},
				{"21322","redirect_uri_mismatch:重定向地址不匹配"},
				{"21323","invalid_request:请求不合法 "},
				{"21324","invalid_client:client_id或client_secret参数无效"},
				{"21325","invalid_grant:提供的Access Grant是无效的、过期的或已撤销的"},
				{"21326","unauthorized_client:客户端没有权限"},
				{"21327","expired_token:token过期"},
				{"21328","unsupported_grant_type:不支持的 GrantType"},
				{"21329","unsupported_response_type:不支持的 ResponseType"},
				{"21330","access_denied:用户或授权服务器拒绝授予数据访问权限"},
				{"21331","temporarily_unavailable:服务暂时无法访问"},
			};

			if (ErrorMsgBag.ContainsKey(errorCode))
				return ErrorMsgBag[errorCode];
			else
				return "未知错误";
		}

	}
}
