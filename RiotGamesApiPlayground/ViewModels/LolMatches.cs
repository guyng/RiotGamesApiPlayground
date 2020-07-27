using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiotGamesApiPlayground.ViewModels
{
	public class LolMatches
	{
		public List<LoLMatch> Matches { get; set; }
		public int EndIndex { get; set; }
		public int StartIndex { get; set; }
	}
}
