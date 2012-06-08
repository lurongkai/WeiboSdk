﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace NetDimension.Weibo.Entities.status
{
	public class IDs
	{
		[JsonProperty("statuses")]
		public IEnumerable<string> Statuses { get; internal set; }
		[JsonProperty(PropertyName = "previous_cursor")]
		public string ProviousCursor { get; internal set; }
		[JsonProperty(PropertyName = "next_cursor")]
		public string NextCursor { get; internal set; }
		[JsonProperty(PropertyName = "total_number")]
		public int TotalNumber { get; internal set; }

	}
}