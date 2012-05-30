using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetDimension.Web;
using NetDimension.Weibo;

namespace WeiboDotNet
{
	public partial class Callback : System.Web.UI.Page
	{
		Cookie cookie = new Cookie("WeiboDemo", 24, TimeUnit.Hour);
		OAuth oauth = new OAuth(ConfigurationManager.AppSettings["AppKey"], ConfigurationManager.AppSettings["AppSecret"]);
		string callbackUrl = ConfigurationManager.AppSettings["CallbackUrl"];
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Request.QueryString["code"]))
			{
				
				string accessToken = oauth.GetAccessTokenByAuthorizationCode(Request.QueryString["code"], callbackUrl);

				cookie["AccessToken"] = accessToken;

				Response.RedirectPermanent("Default.aspx");
			}
			else
			{


			}
		}
	}
}