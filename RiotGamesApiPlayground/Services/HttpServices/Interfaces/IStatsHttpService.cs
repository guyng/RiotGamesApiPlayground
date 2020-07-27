using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiotGamesApiPlayground.Services.HttpServices.Interfaces
{
	public interface IStatsHttpService
	{
		Task<T> GetStatsData<T>(string uri);
		IHttpBaseService HttpBaseService { get; set; }
	}
}
