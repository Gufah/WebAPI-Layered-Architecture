using ApplicatonProcess.Domain.Repsitories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Data.Repositories
{
    public class HttpClient : IHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetCountryByName(string countryName)
        {
            var client = _httpClientFactory.CreateClient("countries");
            var result = await client.GetAsync($"name/{countryName}?fields=name");
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return string.Empty;
            }

            var countryResult = await result.Content.ReadAsStringAsync();
            if (countryResult == string.Empty)
            {
                return string.Empty;
            }

            var s = JArray.Parse(countryResult);
            string countryNameResult = (string)s[0]["name"];
            return countryNameResult;
        }
    }
}
