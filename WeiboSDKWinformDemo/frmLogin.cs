using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using NetDimension.Weibo;

namespace WeiboSDKWinformDemo
{
	public partial class frmLogin : Form
	{

		OAuth oAuth;
		public frmLogin(OAuth oauth)
		{
			oAuth = oauth;
			InitializeComponent();

		}

		private void frmLogin_Load(object sender, EventArgs e)
		{
			
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			if(!Regex.IsMatch(txtPassport.Text,@"^\w+([\.\-]\w+)*\@\w+([\.\-]\w+)*\.\w+$"))
			{
				MessageBox.Show(this, "账号错误，请填写有效的新浪账号。", "账号错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			if (txtPassword.Text.Length == 0)
			{
				MessageBox.Show(this, "请填写登录密码。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			btnLogin.Text = "登录中...";
			btnLogin.Enabled = false;

			var loginResult = oAuth.ClientLogin(txtPassport.Text, txtPassword.Text, WeiboSDKWinformDemo.Properties.Settings.Default.CallbackUrl);

			if (!loginResult)
			{
				MessageBox.Show(this, "账号密码错误，或授权失败。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
				//如果授权失败，在账号密码正确的前提下，请再次确定ClientLogin中您填写的callback回调地址参数是正确的。
				//关于如何修改回调地址，请参考http://weibosdk.codeplex.com/wikipage?title=如何在新浪后台绑定和修改回调地址
				btnLogin.Text = "登录";
				btnLogin.Enabled = true;

				return;
			}

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();

		}
	}
}
