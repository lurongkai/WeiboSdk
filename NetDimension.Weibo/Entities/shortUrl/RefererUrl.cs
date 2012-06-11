using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NetDimension.Weibo.Entities.shortUrl
{
	public class RefererUrl : EntityBase
	{
		[JsonProperty("clicks")]
		public string Clicks { get; internal set; }

		[JsonProperty("referer")]
		public string Referer { get; internal set; }
	}
}
