﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NetDimension.Weibo.Entities
{
	public class GeoEntity
	{
		[JsonProperty("type")]	
		public string Type { get; internal set; }
		[JsonProperty("coordinates")]	
		public IEnumerable<float> Coordinates { get; internal set; }
	}
}