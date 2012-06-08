﻿using System;
using System.Collections.Generic;
using NetDimension.Weibo.Entities;
using System.Text;
using System.Web;
using Newtonsoft.Json;


namespace NetDimension.Weibo.Interface.Entity
{
	public class SearchInterface: WeiboInterface
	{
		public SearchInterface(Client client)
			: base(client)
		{

		}
		/// <summary>
		/// 搜索用户时的联想搜索建议 
		/// </summary>
		/// <param name="q">搜索的关键字</param>
		/// <param name="count">返回的记录条数，默认为10</param>
		/// <returns></returns>
		public IEnumerable<Entities.search.User> Users(string q, int count = 10)
		{
			return JsonConvert.DeserializeObject<IEnumerable<Entities.search.User>>(Client.GetCommand("search/suggestions/users",
				new WeiboStringParameter("q", q),
				new WeiboStringParameter("count", count)));
		}
		/// <summary>
		/// 搜索微博时的联想搜索建议 
		/// </summary>
		/// <param name="q">搜索的关键字</param>
		/// <param name="count">返回的记录条数，默认为10</param>
		/// <returns></returns>
		public IEnumerable<Entities.search.Status> Statuses(string q, int count = 10)
		{
			return JsonConvert.DeserializeObject < IEnumerable < Entities.search.Status >>( Client.GetCommand("search/suggestions/statuses",
				new WeiboStringParameter("q", q),
				new WeiboStringParameter("count", count)));
		}
		/// <summary>
		/// 搜索学校时的联想搜索建议 
		/// </summary>
		/// <param name="q">搜索的关键字</param>
		/// <param name="count">返回的记录条数，默认为10。 </param>
		/// <param name="type">学校类型，0：全部、1：大学、2：高中、3：中专技校、4：初中、5：小学，默认为0。 </param>
		/// <returns></returns>
		public IEnumerable<Entities.search.School> Schools(string q, int count = 10, int type = 0)
		{
			return JsonConvert.DeserializeObject<IEnumerable<Entities.search.School>>(Client.GetCommand("search/suggestions/schools",
				new WeiboStringParameter("q", q),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("type", type)));
		}
		/// <summary>
		/// 搜索公司时的联想搜索建议 
		/// </summary>
		/// <param name="q">搜索的关键字</param>
		/// <param name="count">返回的记录条数，默认为10</param>
		/// <returns></returns>
		public IEnumerable<string> Companies(string q, int count = 10)
		{
			return Utility.GetStringListFromJSON(Client.GetCommand("search/suggestions/companies",
				new WeiboStringParameter("q", q),
				new WeiboStringParameter("count", count)));
		}
		/// <summary>
		/// 搜索应用时的联想搜索建议 
		/// </summary>
		/// <param name="q">搜索的关键字</param>
		/// <param name="count">返回的记录条数，默认为10</param>
		/// <returns></returns>
		public IEnumerable<Entities.search.App> Apps(string q, int count = 10)
		{
			return JsonConvert.DeserializeObject<IEnumerable<Entities.search.App>>(Client.GetCommand("search/suggestions/apps",
				new WeiboStringParameter("q", q),
				new WeiboStringParameter("count", count)));
		}
		/// <summary>
		/// @用户时的联想建议 
		/// </summary>
		/// <param name="q">搜索的关键字</param>
		/// <param name="count">返回的记录条数，默认为10，粉丝最多1000，关注最多2000。 </param>
		/// <param name="type">联想类型，0：关注、1：粉丝。</param>
		/// <param name="range">联想范围，0：只联想关注人、1：只联想关注人的备注、2：全部，默认为2。</param>
		/// <returns></returns>
		public IEnumerable<Entities.search.AtUser> AtUsers(string q, int count = 10, int type = 0,int range=2)
		{
			return JsonConvert.DeserializeObject<IEnumerable<Entities.search.AtUser>>(Client.GetCommand("search/suggestions/at_users",
				new WeiboStringParameter("q", q),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("type", type),
				new WeiboStringParameter("range", range)));
		}
		/// <summary>
		/// 搜索某一话题下的微博 
		/// </summary>
		/// <param name="q">搜索的话题关键字</param>
		/// <param name="count">单页返回的记录条数，默认为10，最大为50。 </param>
		/// <param name="page">返回结果的页码，默认为1。</param>
		/// <returns></returns>
		public IEnumerable<Entities.status.Entity> Topics(string q, int count = 10,int page=1)
		{
			return JsonConvert.DeserializeObject<IEnumerable<Entities.status.Entity>>(Client.GetCommand("search/suggestions/topics",
				new WeiboStringParameter("q", q),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}


	}
}