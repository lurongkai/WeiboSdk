<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WeiboDotNet.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<style type="text/css">
		body {
			font: 12px/1.5 Tahoma;
		}

		a img{
			border: 0;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
		<h1>欢迎使用新浪微博SDK for .Net 4.0</h1>
		<p>
			在使用之前，请确定您已经在web.config中更换了<span class="auto-style1"><strong>appkey</strong></span>, <span class="auto-style1"><strong>appsecret</strong></span>以及<span class="auto-style1"><strong>回调地址</strong></span>。由于本人开发时使用的appkey已经达到了最大使用人数上限，如不更换将会无限出现“未审核应用已达到使用人数上限”的错误进而无法正常演示DEMO
		</p>
		<p>
			关于如何绑定回调地址，请查看codeplex上本人写的《<a href="http://weibosdk.codeplex.com/wikipage?title=%E5%A6%82%E4%BD%95%E5%9C%A8%E6%96%B0%E6%B5%AA%E5%90%8E%E5%8F%B0%E7%BB%91%E5%AE%9A%E5%92%8C%E4%BF%AE%E6%94%B9%E5%9B%9E%E8%B0%83%E5%9C%B0%E5%9D%80">如何在新浪后台绑定和修改回调地址</a>》文档。
		</p>
		<p>在做了如上步骤后，您就可以开始使用DEMO了。</p>
		<p><strong>请注意：</strong>本DEMO没有做任何浏览器兼容测试，仅支持IE8+、Google Chrome和Firefox等现代浏览器。什么360什么IE6的出问题的就不要来问我了。</p>
		<p>接下来，我们开始吧！下面的方法<strong>任选其一</strong>来进行授权和登录。</p>
		<p>第一种方式：正常授权流程</p>
		<p>
			
			<asp:HyperLink id="authUrl" runat="server">
				<img src="images/240.png" alt="点击此处进行授权"/>
			</asp:HyperLink>
		</p>
		<p>第二种方式：简化了流程的ClientLogin方式</p>
		<table cellpadding="3" cellspacing="0" border="1" style="border-collapse: collapse; border-color: #808080">
			<tr>
				<td>微博账号</td>
				<td>
					<asp:TextBox runat="server" ID="txtPassport"></asp:TextBox>
				</td>
				<td rowspan="2">
					<asp:Button runat="server" ID="btnLogin" Text="登录" Height="60px" Width="60px" OnClick="btnLogin_Click" />
				</td>
			</tr>
			<tr>
				<td>微博密码</td>
				<td>
					<asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
				</td>
			</tr>

		</table>
		<asp:Label runat="server" ID="lblResult"></asp:Label>
	</form>
</body>
</html>
