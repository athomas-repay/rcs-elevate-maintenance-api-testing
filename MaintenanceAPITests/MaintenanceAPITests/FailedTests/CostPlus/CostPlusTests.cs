using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using MaintenanceAPITests.Authorization.Interfaces;
using MaintenanceAPITests.Authorization.Implementations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using MaintenanceAPITests.Contracts.DTOs;
using System.Net;

namespace MaintenanceAPITests.FailedTests.CostPlus
{
    [TestClass()]
    public class CostPlusTests
    {
        private string _gatewayUrl;
        private IRepaySsoApiClient _apiClient;
        private HttpClient _client;

        [TestInitialize]
        public void Init()
        {
            var configuration = Configure();
            _gatewayUrl = configuration["GatewayUrl"];
            _apiClient = new RepaySsoApiClient(configuration["Authorization:ClientId"],
                                               configuration["Authorization:ClientSecret"],
                                               configuration["Authorization:SsoUrl"]);

            ConfigureHttpClient();

        }

        [TestMethod]
        public async Task CostPlus_CannotBeCostPlusAndDailyDiscount_ReturnsBadRequest()
        {
            await ConfigureJwt();

            //The mid I'm using here is a known Daily Discount
            var response = await _client.PutAsJsonAsync("costplus/400100039447/settings", new CostPlusSettings() { IsCostPlusEnabled = true });

            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }


        private IConfiguration Configure()
        {
            return new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json")
                       .Build();
        }

        private void ConfigureHttpClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_gatewayUrl);
        }

        private async Task ConfigureJwt()
        {
            var jwt = await _apiClient.GetValidJwtAsync();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            _client.DefaultRequestHeaders.Add("subdomain", "repay");
        }
    }
}
