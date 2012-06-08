using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetDimension.Weibo;
using System.IO;

namespace WeiboSDKWinformDemo
{
	public partial class frmChat : Form
	{

		const string htmlPattern = @"<!DOCTYPE html>
<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
<head>
	<title></title>
	<style type=""text/css"">
		html, body {
			font-size: 12px;
			cursor: default;
			padding: 5px;
			margin: 0;
		}

		div.status {
			padding-left: 60px;
			position: relative;
			margin-bottom: 10px;
		}

			div.status p {
				margin: 0 0 5px 0;
				line-height: 1.5;
				padding: 0;
			}

				div.status p span.name {
					color: #369;
				}

				div.status p.status-cotent {
					color: #333;
				}

			div.status .face {
				position: absolute;
				left: 0;
				top: 0;
			}

			div.status div.repost {
				border: solid 1px #ACD;
				background: #F0FAFF;
				padding: 10px;
			}

		div.repost p.repost-cotent {
			color: #666 !important;
		}
	</style>
</head>
<body>
<!--StatusesList-->
</body>
</html>";

		Client Sina;	//操作类
		byte[] imgBytes = null;
		public frmChat(OAuth oauth)
		{
			InitializeComponent();
			Sina = new Client(oauth);	//用授权成功的OAuth对象实例化操作类
		}

		private void btnInsertPicture_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				FileInfo imageFile = new FileInfo(dlg.FileName);
				if (imageFile.Exists)
				{
					using (Image img = Bitmap.FromFile(imageFile.FullName))
					{
						using (MemoryStream ms = new MemoryStream())
						{
							System.Drawing.Imaging.ImageFormat formate = System.Drawing.Imaging.ImageFormat.Jpeg;

							if (imageFile.Extension == "png")
							{
								formate = System.Drawing.Imaging.ImageFormat.Png;
							}

							if (imageFile.Extension == "gif")
							{
								formate = System.Drawing.Imaging.ImageFormat.Gif;
							}

							img.Save(ms, formate);
							imgBytes = ms.ToArray();
							ms.Close();
						}
					}

					btnInsertPicture.Enabled = false;

					MessageBox.Show(this, "发条微博看看吧～", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void frmChat_Load(object sender, EventArgs e)
		{
			LoadFriendTimeline();
		}

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

			var json = Sina.API.Dynamic.Statuses.FriendsTimeline(count: 10);
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

			var html = htmlPattern.Replace("<!--StatusesList-->", statusBuilder.ToString());
			wbStatuses.DocumentText = html;
		}

		private void btnPublish_Click(object sender, EventArgs e)
		{
			string status = string.Empty;
			if (txtStatusBody.TextLength == 0)
			{
				status = "我很懒，所以我直接点了发布按钮。";
			}
			else
			{
				status = txtStatusBody.Text;
			}

			try
			{
				if (imgBytes == null)
				{
					dynamic result = Sina.API.Dynamic.Statuses.Update(status);
					MessageBox.Show(this, "你的微博已经发送了哟～", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					dynamic result = Sina.API.Dynamic.Statuses.Upload(status, imgBytes);
					
					MessageBox.Show(this, "小心自己的艳照被发到网上哟～去主页上看看吧～", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					
					imgBytes = null;
					btnInsertPicture.Enabled = true;
					
				}


				txtStatusBody.Text = string.Empty;
				LoadFriendTimeline();
			}
			catch (WeiboException ex)
			{
				MessageBox.Show(this, ex.ErrorCode + ":" + ex.ErrorMessage, "哟！出错了？！", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			finally
			{
				imgBytes = null;
				btnInsertPicture.Enabled = true;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			LoadFriendTimeline();
		}
	}
}
