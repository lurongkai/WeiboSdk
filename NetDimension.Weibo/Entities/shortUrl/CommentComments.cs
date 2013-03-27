using System.Collections.Generic;
using Newtonsoft.Json;
using NetDimension.Weibo.Entities.comment;

namespace NetDimension.Weibo.Entities.shortUrl
{
    public class CommentComments : EntityBase
    {
        [JsonProperty("url_short")]
        public string ShortUrl { get; internal set; }

        [JsonProperty("url_long")]
        public string LongUrl { get; internal set; }

        [JsonProperty("share_comments")]
        public IEnumerable<Entity> Referers { get; internal set; }
    }
}