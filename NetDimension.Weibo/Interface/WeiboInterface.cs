using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetDimension.Weibo.Interface
{
	public abstract class WeiboInterface
	{
		protected Client Client;
		public WeiboInterface(Client client)
		{
			this.Client = client;
		}
	}
}
