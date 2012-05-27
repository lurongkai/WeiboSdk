using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetDimension.Weibo
{
	public class WeiboStringParameter : WeiboParameter
	{
		public WeiboStringParameter()
			: base()
		{

		}

		public WeiboStringParameter(string name, string value)
			: base(name, value)
		{ 
		
		}

		public WeiboStringParameter(string name, bool value)
			: base(name, value? "1" : "0")
		{

		}

		public WeiboStringParameter(string name, int value)
			: base(name, string.Format("{0}",value))
		{

		}

		public WeiboStringParameter(string name, long value)
			: base(name, string.Format("{0}", value))
		{

		}

		public WeiboStringParameter(string name, object value)
			: base(name, string.Format("{0}", value))
		{

		}

		public new string Value
		{
			get
			{
				return (string)base.Value;
			}
			set
			{
				base.Value=value;
			}
		}
	}
	
}
