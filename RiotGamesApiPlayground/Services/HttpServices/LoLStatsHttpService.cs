using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RiotGamesApiPlayground.Constants;
using RiotGamesApiPlayground.Services.HttpServices.Interfaces;

namespace RiotGamesApiPlayground.Services.HttpServices
{
	public class LoLStatsHttpService : ILoLStatsHttpService
	{
		public LoLStatsHttpService(IHttpBaseService httpBaseService)
		{
			HttpBaseService = httpBaseService;
			httpBaseService.SetCustomHeader("X-Riot-Token", ApiKeys.RIOT_API_KEY);
			httpBaseService.SetCustomHeader("Origin", "https://developer.riotgames.com");
		}

		public async Task<T> GetStatsData<T>(string uri)
		{
			var httpRequestResult = await HttpBaseService.GetData<T>(uri, true);
			if (httpRequestResult.statusCode == HttpStatusCode.Unauthorized)
			{
				HttpBaseService.SetCustomHeader("X-Riot-Token", ApiKeys.RIOT_API_KEY);
				httpRequestResult = await HttpBaseService.GetData<T>(uri);
			}

			return httpRequestResult.result;
		}

		public IHttpBaseService HttpBaseService { get; set; }
	}
}
