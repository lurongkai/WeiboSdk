using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NetDimension.Weibo.Entities
{
	public class SchoolEntity
	{
		[JsonProperty("id")]
		public string ID { get; internal set; }
		[JsonProperty("name")]
		public string Name { get; internal set; }
	}
}
