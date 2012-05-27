using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Codeplex.Data;

namespace NetDimension.Weibo.Interface
{
	public class FriendshipInterface : WeiboInterface
	{
		public FriendshipInterface(Client client)
			: base(client)
		{

		}

		public dynamic Friends(string uid = "", string screenName = "", int count = 50, int cursor = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("friendships/friends",
				string.IsNullOrEmpty(uid) ? new WeiboStringParameter("screen_name", screenName) : new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("cursor", cursor)));
		}

		public dynamic FriendIDs(string uid = "", string screenName = "", int count = 50, int cursor = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("friendships/friends/ids",
					string.IsNullOrEmpty(uid) ? new WeiboStringParameter("screen_name", screenName) : new WeiboStringParameter("uid", uid),
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("cursor", cursor)));
		}


		public dynamic FriendsInCommon(string uid = "", string suid="", int count = 50, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("friendships/friends/in_common",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("suid", suid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}

		public dynamic FriendsOnBilateral(string uid, int count = 50, int page = 1, bool sort=false)
		{
			return DynamicJson.Parse(Client.GetCommand("friendships/friends/bilateral",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page),
				new WeiboStringParameter("sort", sort)));
		}

		public dynamic FriendsOnBilateralIDs(string uid, int count = 50, int page = 1, bool sort = false)
		{
			return DynamicJson.Parse(Client.GetCommand("friendships/friends/bilateral/ids",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page),
				new WeiboStringParameter("sort", sort)));
		}

		public dynamic Followers(string uid = "", string screenName = "", int count = 50, int cursor = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("friendships/followers",
				string.IsNullOrEmpty(uid) ? new WeiboStringParameter("screen_name", screenName) : new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("cursor", cursor)));
		}

		public dynamic FollowerIDs(string uid = "", string screenName = "", int count = 50, int cursor = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("friendships/followers/ids",
				string.IsNullOrEmpty(uid) ? new WeiboStringParameter("screen_name", screenName) : new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("cursor", cursor)));
		}

		public dynamic FollowersInActive(string uid, int count = 20)
		{
			return DynamicJson.Parse(Client.GetCommand("friendships/followers/active",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count)));

		}

		public dynamic FriendsChain(string uid, int count = 50, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("friendships/friends_chain/followers",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}

		public dynamic Show(string sourceID="", string sourceScreenName="", string targetID="", string targetScreenName="")
		{
			return DynamicJson.Parse(Client.GetCommand("friendships/show",
				string.IsNullOrEmpty(sourceID) ? new WeiboStringParameter("source_screen_name", sourceScreenName) : new WeiboStringParameter("source_id", sourceID),
				string.IsNullOrEmpty(targetID) ? new WeiboStringParameter("target_screen_name", targetScreenName) : new WeiboStringParameter("uid", targetID)));
		}

		public dynamic Create(string uid = "", string screenName = "")
		{
			return DynamicJson.Parse(Client.PostCommand("friendships/create",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("screen_name", screenName)));
		}

		public dynamic Destroy(string uid = "", string screenName = "")
		{
			return DynamicJson.Parse(Client.PostCommand("friendships/destroy",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("screen_name", screenName)));
		}

		public dynamic UpdateRemark(string uid, string remark)
		{
			return DynamicJson.Parse(Client.PostCommand("friendships/remark/update",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("remark", remark)));
		}


	}
}
