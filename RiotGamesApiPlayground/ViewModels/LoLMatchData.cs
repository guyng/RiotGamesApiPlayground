using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiotGamesApiPlayground.ViewModels
{
	public class LoLMatchData
	{
		public int SeasonId { get; set; }
		public int QueueId { get; set; }
		public string GameVersion { get; set; }
		public string GameMode { get; set; }
		public int MapId { get; set; }
		public string GameType { get; set; }
		public long GameId { get; set; }
		public int GameDuration { get; set; }
		public long GameCreation { get; set; }

		// ... etc etc
	}
}
