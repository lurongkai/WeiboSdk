using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Codeplex.Data;

namespace NetDimension.Weibo.Interface
{
	public class AccountInterface: WeiboInterface
	{
		public AccountInterface(Client client)
			: base(client)
		{

		}

		public dynamic GetPrivacy()
		{
			return DynamicJson.Parse(Client.GetCommand("account/get_privacy"));
		}

		public dynamic SchoolList(int province, int city, int area, int type = 1, char capital = 'A', string keyword = "", int count = 10)
		{
			return DynamicJson.Parse(Client.GetCommand("account/profile/school_list",
				new WeiboStringParameter("province", province),
				new WeiboStringParameter("city", city),
				new WeiboStringParameter("area", area),
				new WeiboStringParameter("type", type),
				new WeiboStringParameter("capital", capital),
				new WeiboStringParameter("keyword", keyword),
				new WeiboStringParameter("count", count)));
		}

		public dynamic RateLimitStatus()
		{
			return DynamicJson.Parse(Client.GetCommand("account/rate_limit_status"));
		}

		public dynamic GetUID()
		{
			return DynamicJson.Parse(Client.GetCommand("account/get_uid"));
		}

		public dynamic EndSession()
		{
			return DynamicJson.Parse(Client.GetCommand("account/end_session"));
		}

		public dynamic VerifyNickname(string nickname)
		{
			return DynamicJson.Parse(Client.GetCommand("register/verify_nickname", new WeiboStringParameter("nickname", nickname)));
		}

		public dynamic UnreadCount(string uid,string callback="")
		{
			return DynamicJson.Parse(Client.GetCommand("https://rm.api.weibo.com/2/remind/unread_count.json", 
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("callback", callback)));
		}

		public dynamic SetCount(ResetCountType type)
		{
			return DynamicJson.Parse(Client.PostCommand("https://rm.api.weibo.com/2/remind/set_count.json", new WeiboStringParameter("type", type)));
		}

	}
}
