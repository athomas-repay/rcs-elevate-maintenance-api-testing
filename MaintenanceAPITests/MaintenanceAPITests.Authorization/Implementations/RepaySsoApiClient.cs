using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MaintenanceAPITests.Authorization.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using MaintenanceAPITests.Authorization.DTOs;

namespace MaintenanceAPITests.Authorization.Implementations
{
    public class RepaySsoApiClient : IRepaySsoApiClient
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private HttpClient _client;

        public RepaySsoApiClient(string clientId, 
                                 string clientSecret, 
                                 string ssoUrl)
        {
            _clientId     = clientId;
            _clientSecret = clientSecret;

            ConfigureHttpClient(ssoUrl);
        }

        public async Task<string> GetValidJwtAsync()
        {
            var authHeaderString = _clientId + ":" + _clientSecret;
            var base64AuthHeaderBytes = Encoding.ASCII.GetBytes(authHeaderString);
            var base64AuthHeaderString = Convert.ToBase64String(base64AuthHeaderBytes);

            var keyValueDictionary = new Dictionary<string, string>();
            keyValueDictionary.Add("grant_type", "client_credentials");
            var httpMessage = new HttpRequestMessage(HttpMethod.Post, _client.BaseAddress) { Content = new FormUrlEncodedContent(keyValueDictionary) };

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64AuthHeaderString);

            var response = await _client.SendAsync(httpMessage);
            string json = await response.Content.ReadAsStringAsync();
            var repaySsoDto = JsonSerializer.Deserialize<RepaySsoDto>(json);

            return repaySsoDto.AccessToken;
        }

        private void ConfigureHttpClient(string ssoUrl)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ssoUrl);
        }
    }
}
