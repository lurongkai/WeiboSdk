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
		/// <summary>
		/// 获取用户信
		/// </summary>
		/// <param name="uid">需要查询的用户ID。 </param>
		/// <param name="screenName">需要查询的用户昵称。 </param>
		/// <returns></returns>
		public dynamic Show(string uid = "", string screenName = "")
		{
			return DynamicJson.Parse(Client.GetCommand("users/show",
				string.IsNullOrEmpty(uid) ? new WeiboStringParameter("screen_name", screenName) : new WeiboStringParameter("uid", uid)));
		}
		/// <summary>
		/// 通过个性化域名获取用户资料以及用户最新的一条微博 
		/// </summary>
		/// <param name="domain">需要查询的个性化域名。 </param>
		/// <returns></returns>
		public dynamic ShowByDomain(string domain)
		{
			return DynamicJson.Parse(Client.GetCommand("users/domain_show", new WeiboStringParameter("domain", domain)));
		}
		/// <summary>
		/// 批量获取用户的粉丝数、关注数、微博数
		/// </summary>
		/// <param name="uids"></param>
		/// <returns></returns>
		public dynamic Counts(params string[] uids)
		{
			return DynamicJson.Parse(Client.GetCommand("users/counts", new WeiboStringParameter("uids", string.Join(",",uids))));
		}
	}
}
