using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Codeplex.Data;

namespace NetDimension.Weibo.Interface
{
	public class SearchInterface: WeiboInterface
	{
		public SearchInterface(Client client)
			: base(client)
		{

		}

		public dynamic Users(string q, int count = 10)
		{
			return DynamicJson.Parse(Client.GetCommand("search/suggestions/users",
				new WeiboStringParameter("q", q),
				new WeiboStringParameter("count", count)));
		}

		public dynamic Statuses(string q, int count = 10)
		{
			return DynamicJson.Parse(Client.GetCommand("search/suggestions/statuses",
				new WeiboStringParameter("q", q),
				new WeiboStringParameter("count", count)));
		}

		public dynamic Schools(string q, int count = 10,int type=0)
		{
			return DynamicJson.Parse(Client.GetCommand("search/suggestions/schools",
				new WeiboStringParameter("q", q),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("type", type)));
		}

		public dynamic Companies(string q, int count = 10)
		{
			return DynamicJson.Parse(Client.GetCommand("search/suggestions/companies",
				new WeiboStringParameter("q", q),
				new WeiboStringParameter("count", count)));
		}

		public dynamic Apps(string q, int count = 10)
		{
			return DynamicJson.Parse(Client.GetCommand("search/suggestions/apps",
				new WeiboStringParameter("q", q),
				new WeiboStringParameter("count", count)));
		}

		public dynamic AtUsers(string q, int count = 10, int type = 0,int range=2)
		{
			return DynamicJson.Parse(Client.GetCommand("search/suggestions/at_users",
				new WeiboStringParameter("q", q),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("type", type),
				new WeiboStringParameter("range", range)));
		}

		public dynamic Topics(string q, int count = 10,int page=1)
		{
			return DynamicJson.Parse(Client.GetCommand("search/suggestions/topics",
				new WeiboStringParameter("q", q),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}


	}
}
