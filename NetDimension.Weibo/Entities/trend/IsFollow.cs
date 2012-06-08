﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NetDimension.Weibo.Entities.trend
{
	public class IsFollow
	{
		[JsonProperty("trend_id")]
		public string ID { get; internal set; }
		[JsonProperty("is_follow")]
		public bool Following { get; internal set; }
	}
}
