using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;
using MaintenanceAPITests.Common.Helpers;
using MaintenanceAPITests.Authorization.Interfaces;
using System.Net.Http.Json;
using MaintenanceAPITests.Contracts.DTOs;
using System.Text.Json;
using System.Net;

namespace MaintenanceAPITests.SucessfulTests.CostPlus
{
    [TestClass]
    [TestCategory("Success")]
    [TestCategory("CostPlus")]
    public class CostPlusTests
    {
        private HttpClient _client;
        private IRepaySsoApiClient _apiClient;
        private const string costPlusMid = "700100015022";

        [TestInitialize]
        public void Init()
        {
            var configuration = ConfigurationHelper.Configure();
            _client = HttpClientHelper.ConfigureHttpClient(configuration["GatewayUrl"]);
            _apiClient = RepaySsoApiClientHelper.ConfigureRepaySsoApiClient(configuration);
        }

        [TestMethod]
        public async Task EnableCostPlus_MerchantIsNotDailyDiscount_CostPlusEnabled()
        {
            await _client.SetUpAuthenticationHeader(_apiClient);

            var response = await _client.PutAsJsonAsync($"costplus/{costPlusMid}/settings", new CostPlusSettings() { IsCostPlusEnabled = true });

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await SetCostPlusSettingsBack();
        }

        private async Task SetCostPlusSettingsBack()
        {
            await _client.PutAsJsonAsync($"costplus/{costPlusMid}/settings", new CostPlusSettings() { IsCostPlusEnabled = false });
        }
    }
}
