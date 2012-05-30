using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NetDimension.Web
{

	public enum TimeUnit
	{
		Minute, Hour, Day, Month, Year
	}

	public class Cookie
	{
		public string Name 
		{ 
			get; 
			set; 
		}
		public int Expires 
		{ 
			get; 
			set; 
		}
		public TimeUnit TimeUnit 
		{ 
			get; 
			set; 
		}

		public DateTime ExpiresTime
		{
			get;
			private set;
		}

		[System.ComponentModel.DefaultValue(false)]
		public bool Encrypt
		{
			get;
			set;
		}
				
		public Cookie(string cookieName, int expires=0, TimeUnit unit=TimeUnit.Minute)
		{
			Name = cookieName;
			Expires = expires;
			TimeUnit = unit;
		}


		/// <summary>
		/// 根据配置获取实际超时时间
		/// </summary>
		/// <returns>计算结果</returns>
		private DateTime CacluteExpiresTime()
		{
			DateTime expiresTime = DateTime.Now;
			switch (TimeUnit)
			{
				case TimeUnit.Minute:
					expiresTime = expiresTime.AddMinutes(Expires);
					break;
				case TimeUnit.Hour:
					expiresTime = expiresTime.AddHours(Expires);
					break;
				case TimeUnit.Day:
					expiresTime = expiresTime.AddDays(Expires);
					break;
				case TimeUnit.Month:
					expiresTime = expiresTime.AddMonths(Expires);
					break;
				case TimeUnit.Year:
					expiresTime = expiresTime.AddYears(Expires);
					break;
			}
			return expiresTime;
		}

		/// <summary>
		/// 设置Cookie值
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="value">值</param>
		private void SetCookieValue(string key, string value)
		{
			HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(Name);

			if (cookie == null)
			{
				cookie = new HttpCookie(Name);
				HttpContext.Current.Response.AppendCookie(cookie);
			}

			if (string.IsNullOrWhiteSpace(value) && cookie.Values.AllKeys.Contains(key))
			{
				cookie.Values.Remove(key);
			}
			else
			{
				if (Encrypt)
				{
					cookie.Values[key] = NetDimension.Strings.Crypto.DESC.Encrypt(value);
				}
				else
				{
					cookie.Values[key] = value;
				}
			}

			if (Expires != 0)
			{
				cookie.Expires = CacluteExpiresTime();
			}

			HttpContext.Current.Response.SetCookie(cookie);
		}

		/// <summary>
		/// 获取Cookie值
		/// </summary>
		/// <param name="key">键</param>
		/// <returns>值</returns>
		private string GetCookieValue(string key)
		{
			HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(Name);

			if (cookie == null)
			{
				return null;
			}
			else
			{
				
				if (Encrypt)
				{
					return NetDimension.Strings.Crypto.DESC.Decrypt(cookie.Values[key]);
				}
				else
				{ 
					return cookie.Values[key];
				}
			}
		}


		/// <summary>
		/// 返回或设置实例Cookie中的值
		/// </summary>
		/// <param name="key">键</param>
		/// <returns>值</returns>
		public string this[string key]
		{
			get
			{
				return GetCookieValue(key);
			}
			set
			{
				SetCookieValue(key, value);
			}
		}

		/// <summary>
		/// 设置指定键Cookie的值
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="value">值</param>
		public static void SetValue(string key, string value)
		{
			HttpCookie cookie = new HttpCookie(key);
			cookie.Value = value;

			if (HttpContext.Current.Response.Cookies.Get(key) != null)
			{
				HttpContext.Current.Response.SetCookie(cookie);
			}
			else
			{
				HttpContext.Current.Response.AppendCookie(cookie);
			}
		}

		/// <summary>
		/// 设置指定键Cookie的值
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="value">值</param>
		/// <param name="expiresTime">到期时间</param>
		public static void SetValue(string key, string value, DateTime expiresTime)
		{
			HttpCookie cookie = new HttpCookie(key);
			cookie.Value = value;
			cookie.Expires = expiresTime;

			if (HttpContext.Current.Response.Cookies.Get(key) != null)
			{
				HttpContext.Current.Response.SetCookie(cookie);
			}
			else
			{
				HttpContext.Current.Response.AppendCookie(cookie);
			}
		}

		/// <summary>
		/// 设置指定键Cookie的值
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="value">值</param>
		/// <param name="minutes">到期时间增量</param>
		public static void SetValue(string key, string value, int minutes)
		{
			HttpCookie cookie = new HttpCookie(key);
			cookie.Value = value;
			cookie.Expires = DateTime.Now.AddMinutes(minutes);

			if (HttpContext.Current.Response.Cookies.Get(key) != null)
			{
				HttpContext.Current.Response.SetCookie(cookie);
			}
			else
			{
				HttpContext.Current.Response.AppendCookie(cookie);
			}
		}


		/// <summary>
		/// 获取指定键Cookie的值
		/// </summary>
		/// <param name="key">键</param>
		/// <returns>值</returns>
		public static string GetValue(string key)
		{
			if (HttpContext.Current.Request.Cookies.Get(key) == null)
			{
				return string.Empty;
			}
			else
			{
				return HttpContext.Current.Request.Cookies[key].Value;
			}
		}



	}
}
