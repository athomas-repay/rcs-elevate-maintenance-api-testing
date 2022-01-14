using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceAPITests.Authorization.Interfaces
{
    public interface IRepaySsoApiClient
    {
        Task<string> GetValidJwtAsync();
    }
}
