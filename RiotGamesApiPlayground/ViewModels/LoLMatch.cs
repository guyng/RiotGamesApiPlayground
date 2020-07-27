using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiotGamesApiPlayground.ViewModels
{
	public class LoLMatch
	{
		public string Lane { get; set; }
		public long GameId { get; set; }
		public int Champion { get; set; }
		public string PlatformId { get; set; }
		public long Timestamp { get; set; }
		public int Queue { get; set; }
		public string Role { get; set; }
		public int Season { get; set; }
	}
}
