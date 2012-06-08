using System;
using System.Collections.Generic;
using System.Text;

namespace NetDimension.Weibo
{
	public class Client
	{

		const string BASE_URL = "https://api.weibo.com/2/";

		/// <summary>
		/// OAuth实例
		/// </summary>
		public OAuth OAuth
		{
			get;
			private set;
		}

		/// <summary>
		/// 微博动态类型接口
		/// </summary>
		public Interface.EntityInterfaces API
		{
			get;
			private set;
		}

		//TODO:如果要编译其他版本的API，请使用这种API
		//public Interface.EntityInterfaces API
		//{
		//	get;
		//	private set;
		//}

		/// <summary>
		/// 实例化微博操作类
		/// </summary>
		/// <param name="oauth">OAuth实例</param>
		public Client(OAuth oauth)
		{
			this.OAuth = oauth;

			API = new Interface.EntityInterfaces(this);
			//TODO:其他版本的请使用下面的实例化方法
			//API = new Interface.EntityInterfaces(this);
		}


		internal string PostCommand(string command, params WeiboParameter[] parameters)
		{
			return PostCommand(command, false, parameters);
		}

		internal string PostCommand(string command, IEnumerable<KeyValuePair<string, object>> parameters)
		{
			List<WeiboParameter> list = new List<WeiboParameter>();
			foreach (var item in parameters)
			{
				list.Add(new WeiboStringParameter(item.Key, item.Value));
			}
			return PostCommand(command, false, list.ToArray());
		}

		internal string PostCommand(string command, bool multi = true, params WeiboParameter[] parameters)
		{
			return Http(command, RequestMethod.Post, multi, parameters);
		}

		internal string GetCommand(string command, params WeiboParameter[] parameters)
		{
			return Http(command, RequestMethod.Get, false, parameters);
		}

		internal string GetCommand(string command, IEnumerable<KeyValuePair<string, object>> parameters)
		{
			List<WeiboParameter> list = new List<WeiboParameter>();
			foreach (var item in parameters)
			{
				list.Add(new WeiboStringParameter(item.Key, item.Value));
			}
			return Http(command, RequestMethod.Get, false, list.ToArray());
		}

		private string Http(string command, RequestMethod method, bool multi, params WeiboParameter[] parameters)
		{
			string url = string.Empty;

			if (command.StartsWith("http://") || command.StartsWith("https://"))
			{
				url = command;
			}
			else
			{
				url = string.Format("{0}{1}.json", BASE_URL, command);
			}
			return OAuth.Request(url, method, multi, parameters);
		}
	}

}
