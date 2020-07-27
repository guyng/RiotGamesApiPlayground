using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RiotGamesApiPlayground.Services.HttpServices.Interfaces
{
	public interface IHttpBaseService
	{
		Task<(T result, HttpStatusCode statusCode)> GetData<T>(string uri, bool jsonParser = false);
		Task<(T result, HttpStatusCode statusCode)> PostData<T>(string uri, HttpContent body);
		Task<T> PutData<T>(string uri, object body);
		Task<bool> DeleteData(string uri);

		void SetCustomHeader(string key, string value);

		void SetAuthenticationHeader(AuthenticationHeaderValue authHeaderValue);
		Task<Dictionary<string, string>> GetDataCookies(string uri);
		void SetAcceptRequestHeader(MediaTypeWithQualityHeaderValue requestHeaderValue);
	}
}
