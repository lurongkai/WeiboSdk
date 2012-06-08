using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace NetDimension.Weibo
{
	public class Error
	{
		[JsonProperty(PropertyName = "error_code")]
		public string Code
		{
			get;
			internal set;
		}
		[JsonProperty(PropertyName = "request")]
		public string Request
		{
			get;
			internal set;
		}
		[JsonProperty(PropertyName = "error_description")]
		public string Message
		{
			get;
			internal set;
		}
	}
}
