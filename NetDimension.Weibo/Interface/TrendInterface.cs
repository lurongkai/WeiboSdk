using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codeplex.Data;


namespace NetDimension.Weibo.Interface
{
	public class TrendInterface: WeiboInterface
	{
		public TrendInterface(Client client)
			: base(client)
		{

		}

		public dynamic Trends(string uid, int count = 10, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("trends",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}

		public dynamic IsFollow(string trendName)
		{
			return DynamicJson.Parse(Client.GetCommand("trends/is_follow",
				new WeiboStringParameter("trend_name ", trendName)));
		}

		/// <summary>
		/// 返回最近一小时内的热门话题。 
		/// </summary>
		/// <param name="base_app">是否基于当前应用来获取数据。true表示基于当前应用来获取数据。 </param>
		/// <returns></returns>
		public dynamic Hourly(bool baseApp = false)
		{
			return DynamicJson.Parse(Client.GetCommand("trends/hourly",
				new WeiboStringParameter("base_app ", baseApp)));
		}

		/// <summary>
		/// 返回最近一天内的热门话题。 
		/// </summary>
		/// <param name="base_app">是否基于当前应用来获取数据。true表示基于当前应用来获取数据。 </param>
		/// <returns></returns>
		public dynamic Daily(bool baseApp = false)
		{
			return DynamicJson.Parse(Client.GetCommand("trends/daily",
				new WeiboStringParameter("base_app ", baseApp)));
		}

		/// <summary>
		/// 返回最近一周内的热门话题。 
		/// </summary>
		/// <param name="base_app">是否基于当前应用来获取数据。true表示基于当前应用来获取数据。 </param>
		/// <returns></returns>
		public dynamic Weekly(bool baseApp = false)
		{
			return DynamicJson.Parse(Client.GetCommand("trends/weekly ",
				new WeiboStringParameter("base_app ", baseApp)));
		}

		public dynamic Follow(string trendName)
		{
			return DynamicJson.Parse(Client.PostCommand("trends/follow",
				new WeiboStringParameter("trend_name", trendName)));
		}

		public dynamic Destroy(string id)
		{
			return DynamicJson.Parse(Client.PostCommand("trends/destroy",
				  new WeiboStringParameter("trend_id ", id)));

		}


	}
}
