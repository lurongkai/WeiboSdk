using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetDimension.Weibo
{
	public abstract class WeiboParameter
	{
		public string Name
		{
			get;
			set;
		}

		public object Value
		{
			get;
			set;
		}

		public WeiboParameter()
		{ 
		
		}

		public WeiboParameter(string name, object value)
		{
			this.Name = name;
			this.Value = value;
		}
	}
}
