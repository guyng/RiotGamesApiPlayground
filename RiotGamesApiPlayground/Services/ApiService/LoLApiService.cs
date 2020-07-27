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
	}
}
