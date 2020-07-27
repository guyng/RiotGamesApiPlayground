using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiotGamesApiPlayground.ViewModels
{
	public class LoLPlayerInfo
	{
		public int ProfileIconId { get; set; }
		public string Name { get; set; }
		public string Puuid { get; set; }
		public int SummonerLevel { get; set; }
		public string AccountId { get; set; }
		public long RevisionDate { get; set; }
	}
}
