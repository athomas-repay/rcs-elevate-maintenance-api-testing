using Microsoft.Extensions.Configuration;
using MaintenanceAPITests.Authorization.Implementations;
using MaintenanceAPITests.Authorization.Interfaces;

namespace MaintenanceAPITests.Common.Helpers
{
    public static class RepaySsoApiClientHelper
    {
        public static IRepaySsoApiClient ConfigureRepaySsoApiClient(IConfiguration configuration)
        {
            return new RepaySsoApiClient(configuration["Authorization:ClientId"],
                                         configuration["Authorization:ClientSecret"],
                                         configuration["Authorization:SsoUrl"]);
        }
    }
}
