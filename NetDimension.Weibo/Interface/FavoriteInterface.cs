using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codeplex.Data;

namespace NetDimension.Weibo.Interface
{
	public class FavoriteInterface: WeiboInterface
	{
		public FavoriteInterface(Client client)
			: base(client)
		{

		}

		public dynamic Favorites(int count = 50, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("favorites",
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}

		public dynamic FavoriteIDs(int count = 50, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("favorites/ids",
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}

		public dynamic Show(string id)
		{
			return DynamicJson.Parse(Client.GetCommand("favorites/show",
				new WeiboStringParameter("id", id)));
		}

		public dynamic ByTags(string tid,int count = 50, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("favorites/by_tags",
				new WeiboStringParameter("tid", tid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}

		public dynamic ByTagIDs(string tid, int count = 50, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("favorites/by_tags/ids",
				new WeiboStringParameter("tid", tid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}

		public dynamic Create(string id)
		{
			return DynamicJson.Parse(Client.PostCommand("favorites/create",
				new WeiboStringParameter("id", id)));
		}

		public dynamic Destroy(string id)
		{
			return DynamicJson.Parse(Client.PostCommand("favorites/destroy",
				  new WeiboStringParameter("id", id)));
			
		}

		public dynamic DestroyBatch(params string[] ids)
		{
			return DynamicJson.Parse(Client.PostCommand("favorites/destroy_batch",
				  new WeiboStringParameter("ids", string.Join(",",ids))));

		}

		public dynamic UpdateTags(string id, params string[] tags)
		{
			return DynamicJson.Parse(Client.PostCommand("favorites/tags/update",
				  new WeiboStringParameter("id", id),
				  new WeiboStringParameter("tags", string.Join(",", tags))));

		}

		public dynamic UpdateTagsBatch(string tid, string tag)
		{
			return DynamicJson.Parse(Client.PostCommand("favorites/tags/update_batch",
				  new WeiboStringParameter("tid", tid),
				  new WeiboStringParameter("tag", tag)));
		}

		public dynamic DestroyTags(string tid)
		{
			return DynamicJson.Parse(Client.PostCommand("favorites/tags/destroy_batch",
				  new WeiboStringParameter("tid", string.Join(",", tid))));
		}




	}
}
