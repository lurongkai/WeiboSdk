﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NetDimension.Weibo.Entities.trend
{
	public class Trend
	{
		[JsonProperty("trend_id")]
		public string ID { get; internal set; }
		[JsonProperty("hotword")]
		public string HotWord { get; internal set; }
		[JsonProperty("num")]
		public string Number { get; internal set; }
	}
}