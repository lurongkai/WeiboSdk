using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetDimension.Weibo
{
	public class WeiboBinaryParameter : WeiboParameter
	{
		public WeiboBinaryParameter()
			: base()
		{

		}

		public WeiboBinaryParameter(string name, byte[] value)
			: base(name, value)
		{ 
		
		}

		public new byte[] Value
		{
			get
			{
				return (byte[])base.Value;
			}
			set
			{
				base.Value = value;
			}
		}
	}
}
