using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Codeplex.Data;

namespace NetDimension.Weibo.Interface
{
	public class TagInterface: WeiboInterface
	{
		public TagInterface(Client client)
			: base(client)
		{

		}

		public dynamic Tags(string uid, int count = 20, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("tags",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}

		public dynamic TagsBatch(params string[] uids)
		{
			return DynamicJson.Parse(Client.GetCommand("tags/tags_batch",
				new WeiboStringParameter("uids", string.Join(",",uids))));
		}

		public dynamic Suggestions(int count = 10)
		{ 
			return DynamicJson.Parse(Client.GetCommand("tags/suggestions",new WeiboStringParameter("count", count)));
		}

		public dynamic Create(params string[] tags)
		{
			return DynamicJson.Parse(Client.PostCommand("tags/create",
				new WeiboStringParameter("tags", string.Join(",",tags))));
		}

		public dynamic Destroy(string id)
		{
			return DynamicJson.Parse(Client.PostCommand("tags/destroy",
				  new WeiboStringParameter("tag_id ", id)));

		}

		public dynamic DestroyBatch(params string[] ids)
		{
			return DynamicJson.Parse(Client.PostCommand("tags/destroy_batch",
				  new WeiboStringParameter("ids", string.Join(",", ids))));
		}
	}
}
