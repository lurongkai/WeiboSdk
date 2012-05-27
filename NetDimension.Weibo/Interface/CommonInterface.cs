using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Codeplex.Data;

namespace NetDimension.Weibo.Interface
{
	public class CommonInterface: WeiboInterface
	{
		public CommonInterface(Client client)
			: base(client)
		{

		}

		public dynamic CodeToLocation(params string[] codes)
		{
			return DynamicJson.Parse(Client.GetCommand("common/code_to_location", new WeiboStringParameter("codes", string.Join(",",codes))));
		}

		public dynamic GetCity(string province,string capital="")
		{
			return DynamicJson.Parse(Client.GetCommand("common/get_city", 
				new WeiboStringParameter("province", province),
				new WeiboStringParameter("capital", capital)));
		}

		public dynamic GetProvince(string country, string capital = "")
		{
			return DynamicJson.Parse(Client.GetCommand("common/get_province",
				new WeiboStringParameter("country", country),
				new WeiboStringParameter("capital", capital)));
		}

		public dynamic GetCountry(string capital = "")
		{
			return DynamicJson.Parse(Client.GetCommand("common/get_country",
				new WeiboStringParameter("capital", capital)));
		}

		public dynamic GetTimezone()
		{
			return DynamicJson.Parse(Client.GetCommand("common/get_timezone"));
		}

	}
}
