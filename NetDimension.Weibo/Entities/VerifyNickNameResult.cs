﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NetDimension.Weibo.Entities
{
	public class VerifyNickNameResult
	{
		[JsonProperty("is_legal")]
		public bool IsLegal { get; internal set; }

		[JsonProperty("suggest_nickname")]
		public IEnumerable<string> SuggestNickName { get; internal set; }
	}
}