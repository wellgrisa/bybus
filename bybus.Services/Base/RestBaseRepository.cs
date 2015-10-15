using System;
using bybus.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace bybus.Services
{
	public abstract class RestBaseRepository
	{
		private HttpClient _client;

		public string ServiceBaseUrl 
		{
			get;
			set;
		}

		public RestBaseRepository (string url)
		{
			ServiceBaseUrl = url;

			_client = new HttpClient();

			Authenticate ();

			SetHeaders();
		}

		private void Authenticate(){

			_client.DefaultRequestHeaders.Authorization = 
				new AuthenticationHeaderValue ("Basic", Convert.ToBase64String (new System.Text.UTF8Encoding().GetBytes (String.Format ("{0}:{1}", "WKD4N7YMA1uiM8V", "DtdTtzMLQlA0hk2C1Yi5pLyVIlAQ68"))));
		}	

		private void SetHeaders ()
		{
			_client.DefaultRequestHeaders.Add ("X-AppGlu-Environment", "staging");
			_client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
		}	

		private HttpRequestMessage ConfigureRequest (string query, object body)
		{
			var uri = new Uri (string.Format (ServiceBaseUrl, query));

			var json = JsonConvert.SerializeObject (body);

			return new HttpRequestMessage (HttpMethod.Post, uri) 
			{
				Content = new StringContent (json, Encoding.UTF8, "application/json") 
			};
		}	

		public IList<T> Query<T>(string query, object body) where T : Entity
		{
			var request = ConfigureRequest (query, body);

			var responseTask = Task.Run (() => _client.SendAsync(request).Result.Content.ReadAsStringAsync());;

			var rows = JObject.Parse (responseTask.Result) ["rows"].ToObject<List<T>> ();

			return rows;
		}
	}
}

