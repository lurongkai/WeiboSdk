﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NetDimension.Weibo.Entities.shortUrl
{
	public class Url
	{
		[JsonProperty("url_short")]
		public string ShortUrl { get; internal set; }
		[JsonProperty("url_long")]
		public string LongUrl { get; internal set; }
		[JsonProperty("type")]
		public string Type { get; internal set; }
		[JsonProperty("result")]
		public bool Result { get; internal set; }
	}
}
