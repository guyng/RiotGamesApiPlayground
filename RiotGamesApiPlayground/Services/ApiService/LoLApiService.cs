using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiotGamesApiPlayground.Services.ApiService.Interfaces;
using RiotGamesApiPlayground.Services.HttpServices.Interfaces;
using RiotGamesApiPlayground.ViewModels;

namespace RiotGamesApiPlayground.Services.ApiService
{
	public class LoLApiService : ILoLApiService
	{
		private readonly ILoLStatsHttpService _loLStatsHttpService;

		public LoLApiService(ILoLStatsHttpService loLStatsHttpService)
		{
			_loLStatsHttpService = loLStatsHttpService;
		}
		public async Task<LoLPlayerInfo> GetSummonerInfoByName(string playerName, string region)
		{
			var lolUserUri = "https://api.riotgames.com/lol/summoner/v4/summoners/by-name/" + playerName;
			lolUserUri = lolUserUri.Insert(lolUserUri.IndexOf("api", StringComparison.InvariantCultureIgnoreCase), region + ".");
			var result = await _loLStatsHttpService.GetStatsData<LoLPlayerInfo>(lolUserUri);
			return result;
		}

		public async Task<List<LoLMatchData>> GetSummonerMatchesData(string playerName,string playerUid, string region)
		{
			var result = new List<LoLMatchData>();
			var matchesCount = await GetSummonerMatchesCount(playerUid, region);
			var finalMatchList = new List<LoLMatch>();
			for (int i = 0; i < matchesCount; i += 100)
			{
				var lolMatchesUri = LoLMatchListV2(playerUid, i, i + 100);
				lolMatchesUri =
					lolMatchesUri.Insert(
						lolMatchesUri.IndexOf("api", StringComparison.InvariantCultureIgnoreCase),
						region + ".");
				var matchList = await _loLStatsHttpService.GetStatsData<LolMatches>(lolMatchesUri);
				finalMatchList.AddRange(matchList.Matches);
				// modify here depends on your rate limit
				await Task.Delay(1 * 1000);
			}

			// Execute time can perform better with multi threading techniques but for simplicity sake i kept it simple
			foreach (var loLMatch in finalMatchList)
			{
				var lolMatchDataUri = "https://api.riotgames.com/lol/match/v4/matches/" + loLMatch.GameId.ToString();
				lolMatchDataUri = lolMatchDataUri.Insert(
					lolMatchDataUri.IndexOf("api", StringComparison.InvariantCultureIgnoreCase),
					region + ".");
				var apiResult =
					await _loLStatsHttpService.GetStatsData<LoLMatchData>(lolMatchDataUri);
				result.Add(apiResult);
			}

			return result;
		}

		public async Task<int> GetSummonerMatchesCount(string playerUid, string region)
		{
			var lolMatchesUri = LoLMatchListV2(playerUid, 100000, 100100);
			lolMatchesUri =
				lolMatchesUri.Insert(lolMatchesUri.IndexOf("api", StringComparison.InvariantCultureIgnoreCase),
					region + ".");
			var result = await _loLStatsHttpService.GetStatsData<LolMatches>(lolMatchesUri);
			return result.EndIndex;
		}

		public static string LoLMatchListV2(string accountId, int begin = 0, int end = 100)
		{

			return
				$"https://api.riotgames.com/lol/match/v4/matchlists/by-account/{accountId}?endIndex={end}&beginIndex={begin}";

		}
	}
}
