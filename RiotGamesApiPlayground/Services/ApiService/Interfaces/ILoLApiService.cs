﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiotGamesApiPlayground.ViewModels;

namespace RiotGamesApiPlayground.Services.ApiService.Interfaces
{
	public interface ILoLApiService : IApiService
	{
		Task<LoLPlayerInfo> GetSummonerInfoByName(string playerName, string region);
	}
}
