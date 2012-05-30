using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetDimension.Web;
using NetDimension.Weibo;

namespace WeiboDotNet
{
	public partial class Login : System.Web.UI.Page
	{
		Cookie cookie = new Cookie("WeiboDemo", 24, TimeUnit.Hour);
		OAuth oauth = new OAuth(ConfigurationManager.AppSettings["AppKey"], ConfigurationManager.AppSettings["AppSecret"]);
		string callbackUrl = ConfigurationManager.AppSettings["CallbackUrl"];
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string url = oauth.GetAuthorizeURL(callbackUrl);
				authUrl.NavigateUrl = url;
			}
			else
			{
				authUrl.Enabled = false;
			}

			
		}

		protected void btnLogin_Click(object sender, EventArgs e)
		{
			if (!Regex.IsMatch(txtPassport.Text, @"^\w+([\.\-]\w+)*\@\w+([\.\-]\w+)*\.\w+$"))
			{
				lblResult.Text = "账号错误，请填写有效的新浪账号。";
				return;
			}

			if (txtPassword.Text.Length == 0)
			{
				lblResult.Text = "请填写登录密码";
				return;
			}

			if (oauth.ClientLogin(txtPassport.Text, txtPassword.Text, callbackUrl))
			{
				cookie["AccessToken"] = oauth.AccessToken;

				Response.RedirectPermanent("Default.aspx");
			}
			else
			{
				lblResult.Text = "授权/登录失败，请确定账号密码是对的，还有最上面的回调地址是绑定过的。";
			}
		}
	}
}