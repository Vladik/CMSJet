using CMSJet.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public interface IConnectorService
    {
        Task<IEnumerable<Connector>> GetAllConnectorsAsync();
        Task<Connector?> GetConnectorByIdAsync(Guid id);
        Task<Guid> AddConnectorAsync(Connector connector);
        Task<bool> UpdateConnectorAsync(Connector connector);
        Task<bool> DeleteConnectorAsync(Guid id);
    }
}