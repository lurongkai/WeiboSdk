namespace WeiboSDKWinformDemo
{
	partial class frmChat
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.txtStatusBody = new System.Windows.Forms.TextBox();
			this.btnPublish = new System.Windows.Forms.Button();
			this.btnInsertPicture = new System.Windows.Forms.Button();
			this.wbStatuses = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// txtStatusBody
			// 
			this.txtStatusBody.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtStatusBody.Location = new System.Drawing.Point(12, 12);
			this.txtStatusBody.MaxLength = 140;
			this.txtStatusBody.Multiline = true;
			this.txtStatusBody.Name = "txtStatusBody";
			this.txtStatusBody.Size = new System.Drawing.Size(280, 88);
			this.txtStatusBody.TabIndex = 0;
			this.txtStatusBody.Text = "这里是你要说的话";
			// 
			// btnPublish
			// 
			this.btnPublish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPublish.Image = global::WeiboSDKWinformDemo.Properties.Resources.comment;
			this.btnPublish.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnPublish.Location = new System.Drawing.Point(207, 106);
			this.btnPublish.Name = "btnPublish";
			this.btnPublish.Size = new System.Drawing.Size(85, 26);
			this.btnPublish.TabIndex = 2;
			this.btnPublish.Text = "发布微博";
			this.btnPublish.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnPublish.UseVisualStyleBackColor = true;
			this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
			// 
			// btnInsertPicture
			// 
			this.btnInsertPicture.Image = global::WeiboSDKWinformDemo.Properties.Resources.picture;
			this.btnInsertPicture.Location = new System.Drawing.Point(12, 106);
			this.btnInsertPicture.Name = "btnInsertPicture";
			this.btnInsertPicture.Size = new System.Drawing.Size(26, 26);
			this.btnInsertPicture.TabIndex = 3;
			this.btnInsertPicture.TabStop = false;
			this.btnInsertPicture.UseVisualStyleBackColor = true;
			this.btnInsertPicture.Click += new System.EventHandler(this.btnInsertPicture_Click);
			// 
			// wbStatuses
			// 
			this.wbStatuses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.wbStatuses.Location = new System.Drawing.Point(-1, 138);
			this.wbStatuses.Margin = new System.Windows.Forms.Padding(0);
			this.wbStatuses.MinimumSize = new System.Drawing.Size(20, 20);
			this.wbStatuses.Name = "wbStatuses";
			this.wbStatuses.Size = new System.Drawing.Size(305, 305);
			this.wbStatuses.TabIndex = 4;
			// 
			// frmChat
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.BackgroundImage = global::WeiboSDKWinformDemo.Properties.Resources.Default_Final;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(304, 442);
			this.Controls.Add(this.wbStatuses);
			this.Controls.Add(this.btnInsertPicture);
			this.Controls.Add(this.btnPublish);
			this.Controls.Add(this.txtStatusBody);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(320, 480);
			this.Name = "frmChat";
			this.Text = "新浪微博客户端DEMO";
			this.Load += new System.EventHandler(this.frmChat_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtStatusBody;
		private System.Windows.Forms.Button btnPublish;
		private System.Windows.Forms.Button btnInsertPicture;
		private System.Windows.Forms.WebBrowser wbStatuses;
	}
}

