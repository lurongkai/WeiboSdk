using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetDimension.Weibo.Interface
{
	public class InterfaceSelector
	{
		public DynamicInterfaces Dynamic
		{
			get;
			internal set;
		}

		public EntityInterfaces Entity
		{
			get;
			internal set;
		}

		internal InterfaceSelector(Client client)
		{
			Dynamic = new DynamicInterfaces(client);
			Entity = new EntityInterfaces(client);
		}
	}
}
