using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NetDimension.Weibo;

namespace WeiboSDKWinformDemo
{
	static class Program
	{
		/// <summary>
		/// 注意：我的AppKey已经到达注册上线，请自行更改AppKey，否则无法使用本DEMO
		/// </summary>
		static OAuth oauth = new OAuth(WeiboSDKWinformDemo.Properties.Settings.Default.ClientID,WeiboSDKWinformDemo.Properties.Settings.Default.ClientSecret);
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			frmLogin LoginForm = new frmLogin(oauth);
			if (LoginForm.ShowDialog() == DialogResult.OK)
			{
				Application.Run(new frmChat(oauth));
			}
		}
	}
}
