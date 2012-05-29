using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using NetDimension.Weibo;

namespace TestConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			//初始化oAuth，准备认证
			var oauth = new NetDimension.Weibo.OAuth("1028898141", "78be07c9bcfa30b7871788d3778ce131");

			//注意，我的这个AppKey已经悲剧了，达到使用人数上限，请各位自行更换自己的AppKey进行测试，记得去新浪后台绑定回调地址，不然没法运行。
			//建议绑定到https://api.weibo.com/oauth2/default.html
			
			/*
			 * 正常的流程或Web流程：
			 * 1. 获取授权地址
			 * 2. 访问授权地址
			 * 3. 授权成功后自动跳转至callback指定的网站，并获得code
			 * 4. 通过code换取access token
			 * 
			 * 下面将演示这个过程
			 */

			//这是第一种方法，传统的、正常的流程。适用于web项目
			var url = oauth.GetAuthorizeURL("https://api.weibo.com/oauth2/default.html", ResponseType.Code);
			System.Diagnostics.Process.Start(url);		//模拟弹窗。打开浏览器，进行授权流程，之后会跳转到callback指定的网址，并获得code
						//填写刚才得到的code
			Console.Write("请填写浏览器地址中的Code参数：");
			var code = Console.ReadLine();
			//根据code获取AccessToken
			var accessToken = oauth.GetAccessTokenByAuthorizationCode(code, "https://api.weibo.com/oauth2/default.html");	//注意：callback指定的url必须一致
			//看看我们获得的access token
			Console.WriteLine(accessToken);

			//当然，SDK中还有更牛逼的获取access token的方法。这就是ClientLogin，这个方法模拟上述整个流程，并最终获取access token。登录并授权成则返回true。一步搞定，无需GetAuthorizeURL、GetCode、GetAccessTokenByAuthorizationCode三个步骤。居家旅行，杀人越货必备之良物啊！把上面的过程注释掉，用下面的方法来看看呗～
			//这就是第二种方法，只需一步。而且这绝对不是官方的GetAccessToken时使用的password方式。下面的这个方法，不论你的AppKey的权限有多小都不受影响。非常适用于Winform
			//var result = oauth.ClientLogin("362987303@qq.com", "fengfeng35", "https://api.weibo.com/oauth2/default.html");
			
			if (!string.IsNullOrEmpty(accessToken))
			//if (result)	//如果用ClientLogin，请换一下这个地方
			{
				NetDimension.Weibo.Client Sina = new NetDimension.Weibo.Client(oauth);
				try
				{
					Console.WriteLine(Sina.API.Statuses.FriendsTimeline());	//获得FriendTimeline
					Console.WriteLine(Sina.API.Statuses.Update("发布一条微博来测试下火力！" + DateTime.Now.ToLongTimeString()));	//发条微博测试下
					var mentions = Sina.API.Statuses.Mentions();	//来看看提到我的微博
					//到 http://open.weibo.com/wiki/2/statuses/mentions 查一下mentions的数据结构，接下来咱们操作下数据。
					foreach (var status in mentions.statuses)
					{
						if (status.IsDefined("user"))
						{
							Console.WriteLine(string.Format("{0} 说：{1}", status.user.screen_name, status.text));
						}
					}
				}
				catch (WeiboException wex)
				{
					Console.WriteLine(wex.ErrorMessage);
				}

			}
			else
			{
				Console.WriteLine("认证失败");
			}
			Console.ReadKey();

		}
	}
}
