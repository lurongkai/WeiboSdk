using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codeplex.Data;

namespace NetDimension.Weibo.Interface
{
	public class CommentInterface : WeiboInterface
	{
		public CommentInterface(Client client)
			: base(client)
		{

		}

		public dynamic Show(string id, long sinceID = 0, long maxID = 0, int count = 50, int page = 1, int filterByAuthor = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("comments/show",
				new WeiboStringParameter("id", id),
				new WeiboStringParameter("since_id", sinceID),
				new WeiboStringParameter("max_id", maxID),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page),
				new WeiboStringParameter("filter_by_author", filterByAuthor)));
		}

		public dynamic ByMe(long sinceID = 0, long maxID = 0, int count = 50, int page = 1, int filterBySource = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("comments/by_me",
				
				new WeiboStringParameter("since_id", sinceID),
				new WeiboStringParameter("max_id", maxID),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page),
				new WeiboStringParameter("filter_by_source", filterBySource)));
		}

		public dynamic ToMe(long sinceID = 0, long maxID = 0, int count = 50, int page = 1, int filterByAuthor = 0, int filterBySource = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("comments/to_me",
					new WeiboStringParameter("since_id", sinceID),
					new WeiboStringParameter("max_id", maxID),
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("page", page),
					new WeiboStringParameter("filter_by_author", filterByAuthor),
					new WeiboStringParameter("filter_by_source", filterBySource)));
		}

		public dynamic Timeline(long sinceID = 0, long maxID = 0, int count = 50, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("comments/timeline",
					new WeiboStringParameter("since_id", sinceID),
					new WeiboStringParameter("max_id", maxID),
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("page", page)));
		}

		public dynamic Mentions(long sinceID = 0, long maxID = 0, int count = 50, int page = 1, int filterByAuthor = 0, int filterBySource = 0, int filterByType = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("comments/mentions",
					new WeiboStringParameter("since_id", sinceID),
					new WeiboStringParameter("max_id", maxID),
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("page", page),
					new WeiboStringParameter("filter_by_author", filterByAuthor),
					new WeiboStringParameter("filter_by_source ", filterBySource)));
		}

		public dynamic ShowBatch(params string[] cids)
		{ 
			return DynamicJson.Parse(Client.GetCommand("comments/show_batch",
				new WeiboStringParameter("cids", string.Join(",",cids))));
		}

		public dynamic Create(string id, string comment, bool commentOrigin = false)
		{
			return DynamicJson.Parse(Client.PostCommand("comments/create",
				new WeiboStringParameter("id", id),	
				new WeiboStringParameter("comment", comment),
				new WeiboStringParameter("comment_ori", commentOrigin)));
		}

		public dynamic Destroy(string cid)
		{
			return DynamicJson.Parse(Client.PostCommand("comments/destroy",
				new WeiboStringParameter("cid", cid)));
		}

		public dynamic DestroyBatch(params string[] ids)
		{
			return DynamicJson.Parse(Client.PostCommand("comments/destroy",
					new WeiboStringParameter("destroy_batch", string.Join(",",ids))));
		}

		public dynamic Reply(string cid, string id, string comment, bool withoutMention = false, bool commentOrigin = false)
		{
			return DynamicJson.Parse(Client.PostCommand("comments/reply",
				new WeiboStringParameter("cid", cid),
				new WeiboStringParameter("id", id),
				new WeiboStringParameter("comment", comment),
				new WeiboStringParameter("without_mention ", withoutMention),
				new WeiboStringParameter("comment_ori", commentOrigin)));
		}
	}
}
