﻿using Newtonsoft.Json;

namespace NetDimension.Weibo.Entities.search
{
    public class AtUser : EntityBase
    {
        [JsonProperty("nickname")]
        public string NickName { get; internal set; }

        [JsonProperty("remark")]
        public string Remark { get; internal set; }

        [JsonProperty("uid")]
        public string UID { get; internal set; }
    }
}