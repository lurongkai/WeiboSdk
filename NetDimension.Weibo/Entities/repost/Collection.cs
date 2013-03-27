using System.Collections.Generic;
using NetDimension.Json;
using NetDimension.Weibo.Entities.status;

namespace NetDimension.Weibo.Entities.repost
{
    public class Collection : EntityBase
    {
        [JsonProperty(PropertyName = "reposts")]
        public IEnumerable<Entity> Statuses { get; internal set; }

        [JsonProperty(PropertyName = "previous_cursor")]
        public string ProviousCursor { get; internal set; }

        [JsonProperty(PropertyName = "next_cursor")]
        public string NextCursor { get; internal set; }

        [JsonProperty(PropertyName = "total_number")]
        public int TotalNumber { get; internal set; }
    }
}