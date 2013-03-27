using System.Collections.Generic;
using NetDimension.Json;

namespace NetDimension.Weibo.Entities
{
    public class GeoEntity : EntityBase
    {
        [JsonProperty("type")]
        public string Type { get; internal set; }

        [JsonProperty("coordinates")]
        public IEnumerable<float> Coordinates { get; internal set; }
    }
}