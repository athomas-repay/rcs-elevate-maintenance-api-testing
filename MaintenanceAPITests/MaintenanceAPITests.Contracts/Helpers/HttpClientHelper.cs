using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceAPITests.Common.Helpers
{
    public static class HttpClientHelper
    {
        public static HttpClient ConfigureHttpClient(string url)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);

            return client;
        } 

    }
}
