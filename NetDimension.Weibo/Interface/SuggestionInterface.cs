using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Codeplex.Data;

namespace NetDimension.Weibo.Interface
{
	public class SuggestionInterface: WeiboInterface
	{
		public SuggestionInterface(Client client)
			: base(client)
		{

		}

		public dynamic HotUsers(HotUserCatagory category)
		{
			return DynamicJson.Parse(Client.GetCommand("suggestions/users/hot",
				new WeiboStringParameter("category", category)));
		}

		public dynamic MayInterestedUsers(int count = 10, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("suggestions/users/may_interested",
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("page", page)));
		}

		public dynamic UsersByStatus(string content, int num = 10)
		{
			return DynamicJson.Parse(Client.GetCommand("suggestions/users/by_status",
					new WeiboStringParameter("content", content),
					new WeiboStringParameter("num", num)));
		}

		public dynamic HotStatuses(int type = 1, bool isPic = false, int count = 20, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("suggestions/statuses/hot",
					new WeiboStringParameter("type", type),
					new WeiboStringParameter("isPic", isPic),
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("page", page)));
		}

		public dynamic ReorderStatuses(int section, int count = 50, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("suggestions/statuses/reorder",
					new WeiboStringParameter("section", section),
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("page", page)));
		}

		public dynamic ReorderStatusIDs(int section, int count = 50, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("suggestions/statuses/reorder/ids",
					new WeiboStringParameter("section", section),
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("page", page)));
		}

		public dynamic HotFavorites(int count = 20, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("suggestions/favorites/hot",
						new WeiboStringParameter("count", count),
						new WeiboStringParameter("page", page)));
		}

		public dynamic NotInterestedUsers(string uid)
		{
			return DynamicJson.Parse(Client.PostCommand("suggestions/users/not_interested",
						new WeiboStringParameter("uid", uid)));
		}
	}
}
