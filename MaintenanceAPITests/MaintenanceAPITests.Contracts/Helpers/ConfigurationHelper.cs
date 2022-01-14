using Microsoft.Extensions.Configuration;
using System.IO;

namespace MaintenanceAPITests.Common.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfiguration Configure()
        {
            return new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json")
                      .Build();
        }
    }
}
