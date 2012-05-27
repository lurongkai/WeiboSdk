using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Codeplex.Data;

namespace NetDimension.Weibo.Interface
{
	public class StatusInterface : WeiboInterface
	{
		public StatusInterface(Client client)
			: base(client)
		{

		}

		/// <summary>
		/// 返回最新的公共微博 
		/// </summary>
		/// <see cref=""/>
		/// <param name="count">单页返回的记录条数，默认为50。 </param>
		/// <param name="page">返回结果的页码，默认为1。 </param>
		/// <param name="baseApp">是否只获取当前应用的数据。0为否（所有数据），1为是（仅当前应用），默认为0。 </param>
		/// <returns>dynamic json</returns>
		public dynamic PublicTimeline(int count = 50, int page = 1, bool baseApp = false)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/public_timeline",
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page),
				new WeiboStringParameter("base_app", baseApp)));
		}

		/// <summary>
		/// 获取当前登录用户及其所关注用户的最新微博
		/// </summary>
		/// <param name="sinceID">若指定此参数，则返回ID比since_id大的微博（即比since_id时间晚的微博），默认为0。</param>
		/// <param name="maxID">若指定此参数，则返回ID小于或等于max_id的微博，默认为0。</param>
		/// <param name="count">单页返回的记录条数，默认为50。</param>
		/// <param name="page">返回结果的页码，默认为1。</param>
		/// <param name="baseApp">否只获取当前应用的数据。0为否（所有数据），1为是（仅当前应用），默认为0。</param>
		/// <param name="feature">过滤类型ID，0：全部、1：原创、2：图片、3：视频、4：音乐，默认为0。</param>
		/// <returns>dynamic json</returns>
		public dynamic FriendsTimeline(long sinceID = 0, long maxID = 0, int count = 50, int page = 1, bool baseApp = false, int feature = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/friends_timeline",
				new WeiboStringParameter("since_id", sinceID),
				new WeiboStringParameter("max_id", maxID),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page),
				new WeiboStringParameter("base_app", baseApp),
				new WeiboStringParameter("feature", feature)));
		}

		public dynamic HomeTimeline(long sinceID = 0, long maxID = 0, int count = 50, int page = 1, bool baseApp = false, int feature = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/home_timeline",
				new WeiboStringParameter("since_id", sinceID),
				new WeiboStringParameter("max_id", maxID),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page),
				new WeiboStringParameter("base_app", baseApp),
				new WeiboStringParameter("feature", feature)));
		}

		public dynamic FriendsTimelineIDs(long sinceID = 0, long maxID = 0, int count = 50, int page = 1, bool baseApp = false, int feature = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/friends_timeline/ids",
					new WeiboStringParameter("since_id", sinceID),
					new WeiboStringParameter("max_id", maxID),
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("page", page),
					new WeiboStringParameter("base_app", baseApp),
					new WeiboStringParameter("feature", feature)));
		}

		public dynamic UserTimeline(string uid = "", string screenName = "", long sinceID = 0, long maxID = 0, int count = 50, int page = 1, bool baseApp = false, int feature = 0, bool trimUser = false)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/user_timeline",
				string.IsNullOrEmpty(uid) ? new WeiboStringParameter("screen_name", screenName) : new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("since_id", sinceID),
				new WeiboStringParameter("max_id", maxID),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page),
				new WeiboStringParameter("base_app", baseApp),
				new WeiboStringParameter("feature", feature),
				new WeiboStringParameter("trim_user", trimUser)));
		}

		public dynamic UserTimelineIDs(string uid = "", string screenName = "", long sinceID = 0, long maxID = 0, int count = 50, int page = 1, bool baseApp = false, int feature = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/user_timeline/ids",
				string.IsNullOrEmpty(uid) ? new WeiboStringParameter("screen_name", screenName) : new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("since_id", sinceID),
				new WeiboStringParameter("max_id", maxID),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page),
				new WeiboStringParameter("base_app", baseApp),
				new WeiboStringParameter("feature", feature)));
		}

		public dynamic RepostTimeline(string id, long sinceID = 0, long maxID = 0, int count = 50, int page = 1, int filterByAuthor = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/repost_timeline",
				new WeiboStringParameter("id", id),
				new WeiboStringParameter("since_id", sinceID),
				new WeiboStringParameter("max_id", maxID),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page),
				new WeiboStringParameter("filter_by_author", filterByAuthor)));
		}

		public dynamic RepostTimelineIDs(string id, long sinceID = 0, long maxID = 0, int count = 50, int page = 1, int filterByAuthor = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/repost_timeline/ids",
					new WeiboStringParameter("id", id),
					new WeiboStringParameter("since_id", sinceID),
					new WeiboStringParameter("max_id", maxID),
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("page", page),
					new WeiboStringParameter("filter_by_author", filterByAuthor)));
		}

		public dynamic RepostByMe(long sinceID = 0, long maxID = 0, int count = 50, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/repost_by_me",
					new WeiboStringParameter("since_id", sinceID),
					new WeiboStringParameter("max_id", maxID),
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("page", page)));
		}

		public dynamic Mentions(long sinceID = 0, long maxID = 0, int count = 50, int page = 1, int filterByAuthor = 0, int filterBySource = 0, int filterByType = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/mentions",
					new WeiboStringParameter("since_id", sinceID),
					new WeiboStringParameter("max_id", maxID),
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("page", page),
					new WeiboStringParameter("filter_by_author", filterByAuthor),
					new WeiboStringParameter("filter_by_source ", filterBySource),
					new WeiboStringParameter("filter_by_type ", filterByType)));
		}

		public dynamic MentionIDs(long sinceID = 0, long maxID = 0, int count = 50, int page = 1, int filterByAuthor = 0, int filterBySource = 0, int filterByType = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/mentions/ids",
					new WeiboStringParameter("since_id", sinceID),
					new WeiboStringParameter("max_id", maxID),
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("page", page),
					new WeiboStringParameter("filter_by_author", filterByAuthor),
					new WeiboStringParameter("filter_by_source ", filterBySource),
					new WeiboStringParameter("filter_by_type ", filterByType)));
		}

		public dynamic BilateralTimeline(long sinceID = 0, long maxID = 0, int count = 50, int page = 1, bool baseApp = false, int feature = 0)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/bilateral_timeline",
					new WeiboStringParameter("since_id", sinceID),
					new WeiboStringParameter("max_id", maxID),
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("page", page),
					new WeiboStringParameter("base_app", baseApp),
					new WeiboStringParameter("feature", feature)));
		}


		public dynamic Show(string id)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/show",
					new WeiboStringParameter("id", id)));
		}

		public dynamic QueryMID(int type=1, params string[] ids)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/querymid",
				new WeiboStringParameter("id", string.Join(",",ids)),
				new WeiboStringParameter("type", type),
				new WeiboStringParameter("is_batch ", ids.Length==1? 0 : 1)));
		}

		public dynamic QueryID(int type = 1, bool inbox = false, bool isBase62 = false, params string[] mids)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/queryid",
				new WeiboStringParameter("mid", string.Join(",", mids)),
				new WeiboStringParameter("type", type),
				new WeiboStringParameter("is_batch ", mids.Length == 1 ? 0 : 1),
				new WeiboStringParameter("inbox", inbox),
				new WeiboStringParameter("isBase62", isBase62)));
		}

		public dynamic HotRepostDaily(int count = 20, bool baseApp = false)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/hot/repost_daily",
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("base_app", baseApp)));
		}

		public dynamic HotRepostWeekly(int count = 20, bool baseApp = false)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/hot/repost_weekly",
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("base_app", baseApp)));
		}

		public dynamic HotCommentsDaily(int count = 20, bool baseApp = false)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/hot/comments_daily",
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("base_app", baseApp)));
		}

		public dynamic HotCommentsWeekly(int count = 20, bool baseApp = false)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/hot/comments_weekly",
					new WeiboStringParameter("count", count),
					new WeiboStringParameter("base_app", baseApp)));
		}

		public dynamic Count(params string[] ids)
		{
			return DynamicJson.Parse(Client.GetCommand("statuses/count",
				new WeiboStringParameter("ids", string.Join(",", ids))));
		}

		public dynamic Repost(string id, string status, int isComment)
		{
			return DynamicJson.Parse(Client.PostCommand("statuses/repost",
					new WeiboStringParameter("id", id),
					new WeiboStringParameter("status", status),
					new WeiboStringParameter("is_comment", isComment)));
		}

		public dynamic Destroy(string id)
		{
			return DynamicJson.Parse(Client.PostCommand("statuses/destroy",
					new WeiboStringParameter("id", id)));
		}

		public dynamic Update(string status, float lat = 0.0f, float log = 0.0f, string annotations = "")
		{
			return DynamicJson.Parse(Client.PostCommand("statuses/update",
					new WeiboStringParameter("status", status),
					new WeiboStringParameter("lat", lat),
					new WeiboStringParameter("long", log),
					new WeiboStringParameter("annotations", annotations)));
		}

		public dynamic Upload(string status, byte[] pic, float lat = 0.0f, float log = 0.0f, string annotations = "")
		{
			return DynamicJson.Parse(Client.PostCommand("https://upload.api.weibo.com/2/statuses/upload",true,
						new WeiboStringParameter("status", status),
						new WeiboBinaryParameter("pic",pic),
						new WeiboStringParameter("lat", lat),
						new WeiboStringParameter("long", log),
						new WeiboStringParameter("annotations", annotations)));
		}

		public dynamic UploadUrlText(string status, string url, float lat = 0.0f, float log = 0.0f, string annotations = "")
		{
			return DynamicJson.Parse(Client.PostCommand("statuses/upload_url_text",
						new WeiboStringParameter("status", status),
						new WeiboStringParameter("url", url),
						new WeiboStringParameter("lat", lat),
						new WeiboStringParameter("long", log),
						new WeiboStringParameter("annotations", annotations)));
		}

		public dynamic Emotions(EmotionType type, LanguageType language)
		{
			return DynamicJson.Parse(Client.GetCommand("emotions",
						new WeiboStringParameter("type", type),
						new WeiboStringParameter("language", language)));
		}

	}
}
