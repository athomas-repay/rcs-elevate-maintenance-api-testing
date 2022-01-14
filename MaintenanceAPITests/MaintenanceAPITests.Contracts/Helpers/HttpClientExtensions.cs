using MaintenanceAPITests.Authorization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace MaintenanceAPITests.Common.Helpers
{
    public static class HttpClientExtensions
    {
        public static async Task SetUpAuthenticationHeader(this HttpClient client, IRepaySsoApiClient repaySsoApiClient)
        {
            var jwt = await repaySsoApiClient.GetValidJwtAsync();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            client.DefaultRequestHeaders.Add("subdomain", "repay");
        }
    }
}
