using System;
using System.Collections.Generic;

using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NetDimension.Weibo.Entities;
namespace NetDimension.Weibo.Interface.Entity
{
	public class FavoriteInterface: WeiboInterface
	{
		public FavoriteInterface(Client client)
			: base(client)
		{

		}
		/// <summary>
		/// 获取当前登录用户的收藏列表
		/// </summary>
		/// <param name="count">单页返回的记录条数，默认为50。</param>
		/// <param name="page">返回结果的页码，默认为1。 </param>
		/// <returns></returns>
		public IEnumerable<NetDimension.Weibo.Entities.favorite.Entity> Favorites(int count = 50, int page = 1)
		{
			return JsonConvert.DeserializeObject<IEnumerable<Entities.favorite.Entity>>(Client.GetCommand("favorites",
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}
		/// <summary>
		/// 获取当前用户的收藏列表的ID
		/// </summary>
		/// <param name="count">单页返回的记录条数，默认为50。 </param>
		/// <param name="page">返回结果的页码，默认为1。 </param>
		/// <returns></returns>
		public IEnumerable<NetDimension.Weibo.Entities.favorite.IDEntity> FavoriteIDs(int count = 50, int page = 1)
		{
			return JsonConvert.DeserializeObject<IEnumerable<NetDimension.Weibo.Entities.favorite.IDEntity>>(Client.GetCommand("favorites/ids",
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}
		/// <summary>
		/// 根据收藏ID获取指定的收藏信息 
		/// </summary>
		/// <param name="id">需要查询的收藏ID。 </param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.favorite.Entity Show(string id)
		{
			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.favorite.Entity>(Client.GetCommand("favorites/show",
				new WeiboStringParameter("id", id)));
		}
		/// <summary>
		/// 根据标签获取当前登录用户该标签下的收藏列表  
		/// </summary>
		/// <param name="tid">需要查询的标签ID。</param>
		/// <param name="count">单页返回的记录条数，默认为50。</param>
		/// <param name="page">返回结果的页码，默认为1。</param>
		/// <returns></returns>
		public IEnumerable<NetDimension.Weibo.Entities.favorite.Entity> ByTags(string tid, int count = 50, int page = 1)
		{
			return JsonConvert.DeserializeObject<IEnumerable<NetDimension.Weibo.Entities.favorite.Entity>>(Client.GetCommand("favorites/by_tags",
				new WeiboStringParameter("tid", tid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}
		/// <summary>
		/// 获取当前登录用户的收藏标签列表 
		/// </summary>
		/// <param name="count">单页返回的记录条数，默认为10。</param>
		/// <param name="page">返回结果的页码，默认为1。</param>
		/// <returns></returns>
		public IEnumerable<NetDimension.Weibo.Entities.favorite.TagEntity> Tags(int count = 10, int page = 1)
		{
			return JsonConvert.DeserializeObject<IEnumerable<NetDimension.Weibo.Entities.favorite.TagEntity>>(Client.GetCommand("favorites/tags",
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("page", page)));
		}
		/// <summary>
		/// 获取当前用户某个标签下的收藏列表的ID 
		/// </summary>
		/// <param name="tid">需要查询的标签ID。</param>
		/// <param name="count">单页返回的记录条数，默认为50。</param>
		/// <param name="page">返回结果的页码，默认为1。</param>
		/// <returns></returns>
		public IEnumerable<NetDimension.Weibo.Entities.favorite.IDEntity> ByTagIDs(string tid, int count = 50, int page = 1)
		{
			return JsonConvert.DeserializeObject<IEnumerable<NetDimension.Weibo.Entities.favorite.IDEntity>>(Client.GetCommand("favorites/by_tags/ids",
				new WeiboStringParameter("tid", tid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}
		/// <summary>
		/// 添加一条微博到收藏里 
		/// </summary>
		/// <param name="id">要收藏的微博ID。</param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.favorite.Entity Create(string id)
		{
			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.favorite.Entity>(Client.PostCommand("favorites/create",
				new WeiboStringParameter("id", id)));
		}
		/// <summary>
		/// 取消收藏一条微博
		/// </summary>
		/// <param name="id">要取消收藏的微博ID。</param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.favorite.Entity Destroy(string id)
		{
			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.favorite.Entity>(Client.PostCommand("favorites/destroy",
				  new WeiboStringParameter("id", id)));
			
		}
		/// <summary>
		/// 根据收藏ID批量取消收藏 
		/// </summary>
		/// <param name="ids">要取消收藏的收藏ID最多不超过10个。 </param>
		/// <returns></returns>
		public bool DestroyBatch(params string[] ids)
		{
			return Convert.ToBoolean(JObject.Parse(Client.PostCommand("favorites/destroy_batch",
				  new WeiboStringParameter("ids", string.Join(",", ids))))["result"]);

		}
		/// <summary>
		/// 更新一条收藏的收藏标签
		/// </summary>
		/// <param name="id">需要更新的收藏ID。</param>
		/// <param name="tags">需要更新的标签内容，最多不超过2条。</param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.favorite.Entity UpdateTags(string id, params string[] tags)
		{
			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.favorite.Entity>(Client.PostCommand("favorites/tags/update",
				  new WeiboStringParameter("id", id),
				  new WeiboStringParameter("tags", string.Join(",", tags))));

		}
		/// <summary>
		/// 更新当前登录用户所有收藏下的指定标签 
		/// </summary>
		/// <param name="tid">需要更新的标签ID</param>
		/// <param name="tag">需要更新的标签内容</param>
		/// <returns></returns>
		public NetDimension.Weibo.Entities.favorite.TagEntity UpdateTagsBatch(string tid, string tag)
		{
			return JsonConvert.DeserializeObject<NetDimension.Weibo.Entities.favorite.TagEntity>(Client.PostCommand("favorites/tags/update_batch",
				  new WeiboStringParameter("tid", tid),
				  new WeiboStringParameter("tag", tag)));
		}
		/// <summary>
		/// 删除当前登录用户所有收藏下的指定标签 
		/// </summary>
		/// <param name="tid">需要删除的标签ID</param>
		/// <returns></returns>
		public bool DestroyTags(string[] tid)
		{
			return Convert.ToBoolean(JObject.Parse(Client.PostCommand("favorites/tags/destroy_batch",
				  new WeiboStringParameter("tid", string.Join(",", tid)))));
		}
	}
}
