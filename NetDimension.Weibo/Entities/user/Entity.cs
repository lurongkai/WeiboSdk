using System;
using System.Collections.Generic;
using System.Text;
using NetDimension.Json;
namespace NetDimension.Weibo.Entities.user
{
	public class Entity : EntityBase
	{
		[JsonProperty(PropertyName = "id")]
		public string ID{get;internal set;}
		[JsonProperty(PropertyName = "screen_name")]
		public string ScreenName{get;internal set;}
		[JsonProperty(PropertyName = "name")]
		public string Name{get;internal set;}
		[JsonProperty(PropertyName = "province")]
		public string Province{get;internal set;}
		[JsonProperty(PropertyName = "city")]
		public string City { get; internal set; }
		[JsonProperty(PropertyName = "location")]
		public string Location { get; internal set; }
		[JsonProperty(PropertyName = "description")]
		public string Description { get; internal set; }
		[JsonProperty(PropertyName = "url")]
		public string Url { get; internal set; }
		[JsonProperty(PropertyName = "profile_image_url")]
		public string ProfileImageUrl { get; internal set; }
		[JsonProperty(PropertyName = "domain")]
		public string Domain { get; internal set; }
		[JsonProperty(PropertyName = "gender")]
		public string Gender { get; internal set; }
		[JsonProperty(PropertyName = "followers_count")]
		public int FollowersCount { get; internal set; }
		[JsonProperty(PropertyName = "friends_count")]
		public int FriendsCount { get; internal set; }
		[JsonProperty(PropertyName = "statuses_count")]
		public int StatusesCount { get; internal set; }
		[JsonProperty(PropertyName = "favourites_count")]
		public long FavouritesCount { get; internal set; }
		[JsonProperty(PropertyName = "created_at")]
		public string CreatedAt { get; internal set; }
		[JsonProperty(PropertyName = "following")]
		public bool Following { get; internal set; }
		[JsonProperty(PropertyName = "allow_all_act_msg")]
		public bool AllowAllActMsg { get; internal set; }
		[JsonProperty(PropertyName = "remark")]
		public string Remark { get; internal set; }
		[JsonProperty(PropertyName = "geo_enabled")]
		public bool GEOEnabled { get; internal set; }
		[JsonProperty(PropertyName = "verified")]
		public bool Verified { get; internal set; }
		[JsonProperty(PropertyName = "allow_all_comment")]
		public bool AllowAllComment { get; internal set; }
		[JsonProperty(PropertyName = "avatar_large")]
		public string AvatarLarge { get; internal set; }
		[JsonProperty(PropertyName = "verified_reason")]
		public string VerifiedReason { get; internal set; }
		[JsonProperty(PropertyName = "follow_me")]
		public bool FollowMe { get; internal set; }
		[JsonProperty(PropertyName = "online_status")]
		public int OnlineStatus { get; internal set; }
		[JsonProperty(PropertyName = "bi_followers_count")]
		public int BIFollowersCount { get; internal set; }
		[JsonProperty(PropertyName = "status")]
		public status.Entity Status { get; internal set; }

	}
}
