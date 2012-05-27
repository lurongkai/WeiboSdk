using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using Codeplex.Data;
using System.Diagnostics;

namespace NetDimension.Weibo
{
	public class OAuth
	{
		private const string AUTHORIZE_URL = "https://api.weibo.com/oauth2/authorize";
		private const string ACCESS_TOKEN_URL = "https://api.weibo.com/oauth2/access_token";


		public string ClientID
		{
			get;
			internal set;
		}

		public string ClientSecret
		{
			get;
			internal set;
		}

		public string AccessToken
		{
			get;
			internal set;
		}

		public string RefreshToken
		{
			get;
			internal set;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clientID">AppKey</param>
		/// <param name="clientSecret">AppSecret</param>
		/// <param name="accessToken">可选参数，如果之前申请的AccessToken没有过期，在此处赋值后可直接调用API</param>
		/// <param name="refreshToken">目前还不知道这个参数会不会开放，保留</param>
		public OAuth(string clientID, string clientSecret, string accessToken = null, string refreshToken = null)
		{
			this.ClientID = clientID;
			this.ClientSecret = clientSecret;
			this.AccessToken = accessToken ?? string.Empty;
			this.RefreshToken = refreshToken ?? string.Empty;
		}

		internal string Request(string url, RequestMethod method = RequestMethod.Get, bool multi = false, params WeiboParameter[] parameters)
		{
			string rawUrl = string.Empty;
			UriBuilder uri = new UriBuilder(url);
			string result = string.Empty;

			switch (method)
			{
				case RequestMethod.Get:
					{
						uri.Query = Utility.BuildQueryString(parameters);
					}
					break;
				case RequestMethod.Post:
					{
						if (!multi)
						{
							uri.Query = Utility.BuildQueryString(parameters);
						}
					}
					break;
			}

			HttpWebRequest http = WebRequest.Create(uri.Uri) as HttpWebRequest;
			http.ServicePoint.Expect100Continue = false;
			http.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0)";

			if (!string.IsNullOrWhiteSpace(AccessToken))
			{
				http.Headers["Authorization"] = string.Format("OAuth2 {0}", AccessToken);
			}

			switch (method)
			{
				case RequestMethod.Get:
					{
						http.Method = "GET";
					}
					break;
				case RequestMethod.Post:
					{
						http.Method = "POST";

						if (multi)
						{
							http.ContentType = "multipart/form-data";
							http.AllowWriteStreamBuffering = true;
							using (Stream request = http.GetRequestStream())
							{
								try
								{
									var raw = Utility.BuildPostData(parameters);
									request.Write(raw, 0, raw.Length);
								}
								finally
								{
									request.Close();
								}
							}
						}
						else
						{
							http.ContentType = "application/x-www-form-urlencoded";
							
							using (StreamWriter request = new StreamWriter(http.GetRequestStream()))
							{
								try
								{
									request.Write(Utility.BuildQueryString(parameters));
								}
								finally
								{
									request.Close();
								}
							}
						}
					}
					break;
			}

			try
			{
				using (WebResponse response = http.GetResponse())
				{

					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						try
						{
							result = reader.ReadToEnd();
						}
						catch (Exception readEx)
						{
							throw readEx;
						}
						finally
						{
							reader.Close();
						}
					}


					response.Close();
				}
			}
			catch (System.Net.WebException webEx)
			{
				if (webEx.Response != null)
				{
					using (StreamReader reader = new StreamReader(webEx.Response.GetResponseStream()))
					{
						string errorInfo = reader.ReadToEnd();
#if DEBUG
						Debug.WriteLine(errorInfo);
#endif
						dynamic json = DynamicJson.Parse(errorInfo);
						reader.Close();

						throw new WeiboException(string.Format("{0}",json.error_code), json.error, json.request);
					}
				}
				else
				{
					throw new WeiboException(webEx.Message);
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}

		/// <summary>
		/// OAuth2的authorize接口
		/// </summary>
		/// <param name="callbackUrl">授权回调地址，站外应用需与设置的回调地址一致，站内应用需填写canvas page的地址。 </param>
		/// <param name="response">返回类型，支持code、token，默认值为code。</param>
		/// <param name="state">用于保持请求和回调的状态，在回调时，会在Query Parameter中回传该参数。 </param>
		/// <param name="display">授权页面的终端类型，取值见下面的说明。 
		/// default 默认的授权页面，适用于web浏览器。 
		/// mobile 移动终端的授权页面，适用于支持html5的手机。 
		/// popup 弹窗类型的授权页，适用于web浏览器小窗口。 
		/// wap1.2 wap1.2的授权页面。 
		/// wap2.0 wap2.0的授权页面。 
		/// js 微博JS-SDK专用授权页面，弹窗类型，返回结果为JSONP回掉函数。
		/// apponweibo 默认的站内应用授权页，授权后不返回access_token，只刷新站内应用父框架。 
		/// </param>
		/// <returns></returns>
		public string GetAuthorizeURL(string callbackUrl, ResponseType response= ResponseType.Code,string state=null, DisplayType display = DisplayType.Default)
		{
			Dictionary<string, string> config = new Dictionary<string, string>()
			{
				{"client_id",ClientID},
				{"redirect_uri",callbackUrl},
				{"response_type",response.ToString().ToLower()},
				{"state",state??string.Empty},
				{"display",display.ToString().ToLower()},
			};
			UriBuilder builder = new UriBuilder(AUTHORIZE_URL);
			builder.Query = Utility.BuildQueryString(config);

			return builder.ToString();
		}


		public bool VerifierAccessToken(string accessToken)
		{
			return false;
		}

		/// <summary>
		/// 使用code方式获取AccessToken
		/// </summary>
		/// <param name="code">Code</param>
		/// <param name="callbackUrl">绑定的回调地址</param>
		/// <returns></returns>
		public string GetAccessTokenByAuthorizationCode(string code,string callbackUrl)
		{
			return GetAccessToken(GrantType.AuthorizationCode, new Dictionary<string, string> { 
				{"code",code},
				{"redirect_uri", callbackUrl}
			});
		}
		
		/// <summary>
		/// 使用password方式获取AccessToken
		/// </summary>
		/// <param name="passport">账号</param>
		/// <param name="password">密码</param>
		/// <returns></returns>
		public string GetAccessTokenByPassword(string passport, string password)
		{
			return GetAccessToken(GrantType.Password, new Dictionary<string, string> { 
				{"username",passport},
				{"password", password}
			});
		}

		/// <summary>
		/// 使用token方式获取AccessToken
		/// </summary>
		/// <param name="refreshToken">refresh token，目前还不知道从哪里获取这个token，未开放</param>
		/// <returns></returns>
		public string GetAccessTokenByRefreshToken(string refreshToken) {
			return GetAccessToken(GrantType.RefreshToken, new Dictionary<string, string> { 
				{"refresh_token",refreshToken}
			});
		}

		/// <summary>
		/// 使用模拟方式进行登录并获得AccessToken
		/// </summary>
		/// <param name="passport">微博账号</param>
		/// <param name="password">微博密码</param>
		/// <param name="callbackUrl">绑定的回调地址</param>
		/// <returns></returns>
		public bool ClientLogin(string passport, string password, string callbackUrl)
		{
			bool result = false;

			HttpWebRequest http = WebRequest.Create(AUTHORIZE_URL) as HttpWebRequest;
			http.Referer = GetAuthorizeURL(callbackUrl);
			http.Method = "POST";
			http.ContentType = "application/x-www-form-urlencoded";
			string postBody = string.Format("action=submit&withOfficalFlag=0&ticket=&isLoginSina=&response_type=code&regCallback=&redirect_uri={0}&client_id={1}&state=&from=&userId={2}&passwd={3}", HttpUtility.UrlEncode(callbackUrl), HttpUtility.UrlEncode(ClientID) , HttpUtility.UrlEncode(passport), HttpUtility.UrlEncode(password));
			byte[] postData = Encoding.Default.GetBytes(postBody);
			http.ContentLength = postData.Length;

			using (Stream request = http.GetRequestStream())
			{
				try
				{
					request.Write(postData, 0, postData.Length);
				}
				catch(Exception ex)
				{
					throw ex;
				}
				finally
				{ 
					request.Close();
				}
			}

			using (HttpWebResponse response = http.GetResponse() as HttpWebResponse)
			{
				if (response != null && response.ResponseUri != null)
				{
					var queryStrs = HttpUtility.ParseQueryString(response.ResponseUri.Query);
					if (!string.IsNullOrEmpty(queryStrs["code"]))
					{
						string code = queryStrs["code"];
						try
						{
							string accessToken = GetAccessTokenByAuthorizationCode(code, callbackUrl);
							if (!string.IsNullOrEmpty(accessToken))
							{ 
								result = true;
							}
						}
						catch
						{ 
							
						}
					}
				}
			}

			return result;

		}

		internal string GetAccessToken(GrantType type, Dictionary<string,string> parameters)
		{

			List<WeiboStringParameter> config = new List<WeiboStringParameter>()
			{
				new WeiboStringParameter(){ Name= "client_id", Value= ClientID},
				new WeiboStringParameter(){ Name="client_secret", Value=ClientSecret}
			};

			switch (type)
			{
				case GrantType.AuthorizationCode:
					{
						config.Add(new WeiboStringParameter(){ Name="grant_type",  Value= "authorization_code"});
						config.Add(new WeiboStringParameter(){ Name="code", Value= parameters["code"]});
						config.Add(new WeiboStringParameter() { Name = "redirect_uri", Value = parameters["redirect_uri"] });
					}
					break;
				case GrantType.Password:
					{
						config.Add(new WeiboStringParameter() { Name = "grant_type", Value = "password" });
						config.Add(new WeiboStringParameter(){ Name="username",  Value= parameters["username"]});
						config.Add(new WeiboStringParameter(){ Name="password", Value=  parameters["password"]});
					}
					break;
				case GrantType.RefreshToken:
					{
						config.Add(new WeiboStringParameter() { Name = "grant_type", Value = "refresh_token" });
						config.Add(new WeiboStringParameter() { Name = "refresh_token", Value = parameters["refresh_token"] });
					}
					break;
			}

			var response = Request(ACCESS_TOKEN_URL, RequestMethod.Post, false, config.ToArray());

			if (!string.IsNullOrEmpty(response))
			{
				dynamic json = DynamicJson.Parse(response);
				AccessToken = json.access_token;
				return json.access_token;
			}
			else
			{
				return string.Empty;
			}
		}


	}
}
