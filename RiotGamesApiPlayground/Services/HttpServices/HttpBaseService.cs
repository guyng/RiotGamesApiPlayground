using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RiotGamesApiPlayground.Services.HttpServices.Interfaces;

namespace RiotGamesApiPlayground.Services.HttpServices
{
	public class HttpBaseService : IHttpBaseService
	{
		private HttpClient _client;
		private readonly ILogger<HttpBaseService> _logger;

		public HttpBaseService(IHttpClientFactory httpClientFactory, ILogger<HttpBaseService> logger)
		{
			_logger = logger;
			_client = httpClientFactory.CreateClient();
		}

		public void SetAuthenticationHeader(AuthenticationHeaderValue authHeaderValue)
		{
			_client.DefaultRequestHeaders.Authorization = authHeaderValue;
		}

		public void SetAcceptRequestHeader(MediaTypeWithQualityHeaderValue requestHeaderValue)
		{
			_client.DefaultRequestHeaders.Accept.Add(requestHeaderValue);
		}

		public void SetCustomHeader(string key, string value)
		{
			_client.DefaultRequestHeaders.Add(key, value);
		}

		public async Task<(T result, HttpStatusCode statusCode)> GetData<T>(string uri, bool jsonParser = false)
		{
			T data = default(T);
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
			try
			{
				httpResponseMessage = await _client.GetAsync(uri);
				if (httpResponseMessage.IsSuccessStatusCode)
				{
					if (jsonParser)
					{
						var tmp = await httpResponseMessage.Content.ReadAsStringAsync();
						data = JsonConvert.DeserializeObject<T>(tmp);
					}
					else
					{
						data = await httpResponseMessage.Content.ReadAsAsync<T>();
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogInformation($"Get Data Failed: {ex.Message}");

			}

			return (data, httpResponseMessage.StatusCode);
		}


		public async Task<Dictionary<string, string>> GetDataCookies(string uri)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			try
			{
				var httpResponseMessage = await _client.GetAsync(uri);
				if (httpResponseMessage.IsSuccessStatusCode)
				{
					httpResponseMessage.Headers.TryGetValues("Set-Cookie", out var cookies);
					foreach (var cookie in cookies)
					{
						var cookieArr = cookie.Split("=");
						if (cookieArr == null || (cookieArr != null && cookieArr.Length < 2))
						{
							continue;
						}

						var valueArr = cookieArr[1].Split(';');
						result.Add(cookieArr[0], valueArr[0]);
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogInformation($"Get Data Failed: {ex.Message}");

			}

			return result;

		}

		public async Task<(T result, HttpStatusCode statusCode)> PostData<T>(string uri, HttpContent body)
		{
			T data = default(T);
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
			try
			{
				httpResponseMessage = await _client.PostAsync(uri, body);
				if (httpResponseMessage.IsSuccessStatusCode)
				{
					data = await httpResponseMessage.Content.ReadAsAsync<T>();
				}
			}
			catch (Exception ex)
			{
				_logger.LogInformation($"Post Data Failed: {ex.Message}");
			}

			return (data, httpResponseMessage.StatusCode);
		}

		public async Task<T> PutData<T>(string uri, object body)
		{
			T data = default(T);
			try
			{
				var httpResponseMessage = await _client.PutAsync(uri,
					new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
				if (httpResponseMessage.IsSuccessStatusCode)
				{
					data = await httpResponseMessage.Content.ReadAsAsync<T>();
				}
			}
			catch (Exception ex)
			{
				_logger.LogInformation($"Put Data Failed: {ex.Message}");

			}
			return data;
		}

		public async Task<bool> DeleteData(string uri)
		{
			bool result = false;
			try
			{
				result = (await _client.DeleteAsync(uri)).IsSuccessStatusCode;
			}
			catch (Exception ex)
			{
				_logger.LogInformation($"Delete Data Failed: {ex.Message}");
			}
			return result;
		}
	}
}
