using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NetDimension.Weibo.Entities.tag
{
	public class Tag
	{
		
		public string ID { get; internal set; }
		public string Name { get; internal set; }
		public string Weight { get; internal set; }
	}
}
