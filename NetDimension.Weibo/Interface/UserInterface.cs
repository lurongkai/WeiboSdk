using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Codeplex.Data;


namespace NetDimension.Weibo.Interface
{
	public class UserInterface: WeiboInterface
	{
		public UserInterface(Client client)
			: base(client)
		{

		}

		public dynamic Show(string uid = "", string screenName = "")
		{
			return DynamicJson.Parse(Client.GetCommand("users/show",
				string.IsNullOrEmpty(uid) ? new WeiboStringParameter("screen_name", screenName) : new WeiboStringParameter("uid", uid)));
		}

		public dynamic ShowByDomain(string domain)
		{
			return DynamicJson.Parse(Client.GetCommand("users/domain_show", new WeiboStringParameter("domain", domain)));
		}

		public dynamic Counts(params string[] uids)
		{
			return DynamicJson.Parse(Client.GetCommand("users/counts", new WeiboStringParameter("uids", string.Join(",",uids))));
		}
	}
}
