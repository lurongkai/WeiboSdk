using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetDimension.Web;
using NetDimension.Weibo;

namespace WeiboDotNet
{
	public partial class Default : System.Web.UI.Page
	{
		Cookie cookie  = new Cookie("WeiboDemo", 24, TimeUnit.Hour);

		Client Sina = null;
		string UserID = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(cookie["AccessToken"]))
			{
				Response.RedirectPermanent("Login.aspx");
			}
			else
			{
				Sina = new Client(new OAuth(ConfigurationManager.AppSettings["AppKey"], ConfigurationManager.AppSettings["AppSecret"],cookie["AccessToken"])); //用cookie里的accesstoken来实例化OAuth，这样OAuth就有操作权限了
			}

			dynamic result = Sina.API.Account.GetUID();
			UserID = string.Format("{0}",result.uid);

			LoadFriendTimeline(); //生成微博列表，当然，你要搞个数据绑定什么也是可以的

		}

		/// <summary>
		/// 获取用户信息，我们来个直接把JSON写到页面的方法和下面的方法区别下
		/// </summary>
		/// <returns>JSON</returns>
		public string LoadUserInfo()
		{
			var user = Sina.API.Users.Show(UserID);
			return string.Format("{0}",user);
		}

		/// <summary>
		/// 加载微博列表
		/// </summary>
		private void LoadFriendTimeline()
		{

			StringBuilder statusBuilder = new StringBuilder();
			string imageParttern = @"<img src=""{0}"" alt=""图片"" class=""inner-pic"" />";
			string statusPattern = @"	<div class=""status"">
		<img src=""{0}"" alt=""{1}"" class=""face"" />
		<p class=""status-cotent""><span class=""name"">{1}</span>：{2}</p>
		{3}
	</div>
";
			string repostPattern = @"	<div class=""status"">
		<img src=""{0}"" alt=""{1}"" class=""face"" />
		<p class=""status-cotent""><span class=""name"">{1}</span>：{2}</p>
		<div class=""repost"">
			<p class=""repost-cotent""><span class=""name"">@{3}</span>：{4}</p>
			{5}
		</div>
	</div>
";

			var json = Sina.API.Statuses.FriendsTimeline(count: 10);
			if (json.IsDefined("statuses"))
			{
				foreach (var status in json.statuses)
				{
					if (!status.IsDefined("user"))
						continue;

					if (status.IsDefined("retweeted_status") && status["retweeted_status"].IsDefined("user"))
					{
						statusBuilder.AppendFormat(repostPattern,
							status.user.profile_image_url,
							status.user.screen_name,
							status.text,
							status.retweeted_status.user.screen_name,
							status.retweeted_status.text,
							status.retweeted_status.IsDefined("thumbnail_pic") ?
							string.Format(imageParttern, status.retweeted_status.thumbnail_pic) : "");

					}
					else
					{
						statusBuilder.AppendFormat(statusPattern,
							status.user.profile_image_url,
							status.user.screen_name,
							status.text,
							status.IsDefined("thumbnail_pic") ?
							string.Format(imageParttern, status.thumbnail_pic) : "");
					}

				}
			}

			statusesHtml.Text = statusBuilder.ToString();
		}

	}
}