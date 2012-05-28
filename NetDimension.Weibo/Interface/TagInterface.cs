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
		/// <summary>
		/// 返回指定用户的标签列表 
		/// </summary>
		/// <param name="uid">要获取的标签列表所属的用户ID。 </param>
		/// <param name="count">单页返回的记录条数，默认为20。</param>
		/// <param name="page">返回结果的页码，默认为1。 </param>
		/// <returns></returns>
		public dynamic Tags(string uid, int count = 20, int page = 1)
		{
			return DynamicJson.Parse(Client.GetCommand("tags",
				new WeiboStringParameter("uid", uid),
				new WeiboStringParameter("count", count),
				new WeiboStringParameter("page", page)));
		}
		/// <summary>
		/// 批量获取用户的标签列表 
		/// </summary>
		/// <param name="uids">要获取标签的用户ID。最大20，逗号分隔。 </param>
		/// <returns></returns>
		public dynamic TagsBatch(params string[] uids)
		{
			return DynamicJson.Parse(Client.GetCommand("tags/tags_batch",
				new WeiboStringParameter("uids", string.Join(",",uids))));
		}
		/// <summary>
		/// 获取系统推荐的标签列表 
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public dynamic Suggestions(int count = 10)
		{ 
			return DynamicJson.Parse(Client.GetCommand("tags/suggestions",new WeiboStringParameter("count", count)));
		}
		/// <summary>
		/// 为当前登录用户添加新的用户标签 
		/// </summary>
		/// <param name="tags">要创建的一组标签，每个标签的长度不可超过7个汉字，14个半角字符。 </param>
		/// <returns></returns>
		public dynamic Create(params string[] tags)
		{
			return DynamicJson.Parse(Client.PostCommand("tags/create",
				new WeiboStringParameter("tags", string.Join(",",tags))));
		}
		/// <summary>
		/// 删除用户标签 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public dynamic Destroy(string id)
		{
			return DynamicJson.Parse(Client.PostCommand("tags/destroy",
				  new WeiboStringParameter("tag_id", id)));

		}
		/// <summary>
		/// 批量删除用户标签 
		/// </summary>
		/// <param name="ids"></param>
		/// <returns></returns>
		public dynamic DestroyBatch(params string[] ids)
		{
			return DynamicJson.Parse(Client.PostCommand("tags/destroy_batch",
				  new WeiboStringParameter("ids", string.Join(",", ids))));
		}
	}
}
