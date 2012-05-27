using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetDimension.Weibo.Interface
{
	public class Interfaces
	{
		public AccountInterface Account{get;private set;}
		public CommentInterface Comments{get;private set;}
		public CommonInterface Common{get;private set;}
		public FavoriteInterface Favorites{get;private set;}
		public FriendshipInterface Friendships{get;private set;}
		public SearchInterface Search{get;private set;}
		public ShortUrlInterface ShortUrl{get;private set;}
		public StatusInterface Statuses{get;private set;}
		public SuggestionInterface Suggestions{get;private set;}
		public TagInterface Tags{get;private set;}
		public TrendInterface Trends{get;private set;}
		public UserInterface Users{get;private set;}
		

		public Interfaces(Client client)
		{
			Account = new AccountInterface(client);
			Comments = new CommentInterface(client);
			Common = new CommonInterface(client);
			Favorites = new FavoriteInterface(client);
			Friendships = new FriendshipInterface(client);
			Search = new SearchInterface(client);
			ShortUrl = new ShortUrlInterface(client);
			Statuses = new StatusInterface(client);
			Suggestions = new SuggestionInterface(client);
			Tags = new TagInterface(client);
			Trends = new TrendInterface(client);
			Users = new UserInterface(client);
		}
	}
}
