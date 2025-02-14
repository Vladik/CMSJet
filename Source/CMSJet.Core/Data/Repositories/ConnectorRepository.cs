using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Repositories
{
    public class ConnectorRepository
    {
        private readonly List<Connector> _connectors = new()
        {
            new Connector
            {
                Id = Guid.NewGuid(),
                Name = "Sitecore Connector",
                Description = "Connector for Sitecore CMS",
                Details = new(),
                Configurations = new()
            },
            new Connector
            {
                Id = Guid.NewGuid(),
                Name = "WordPress Connector",
                Description = "Connector for WordPress CMS",
                Details = new(),
                Configurations = new()
            }
        };

        public Task<IEnumerable<Connector>> GetAllConnectorsAsync() =>
            Task.FromResult<IEnumerable<Connector>>(_connectors);

        public Task<Connector?> GetConnectorByIdAsync(Guid id) =>
            Task.FromResult(_connectors.FirstOrDefault(c => c.Id == id));

        public Task<Guid> AddConnectorAsync(Connector connector)
        {
            connector.Id = Guid.NewGuid();
            _connectors.Add(connector);
            return Task.FromResult(connector.Id);
        }

        public Task<bool> UpdateConnectorAsync(Connector connector)
        {
            var existingConnector = _connectors.FirstOrDefault(c => c.Id == connector.Id);
            if (existingConnector == null) return Task.FromResult(false);

            existingConnector.Name = connector.Name;
            existingConnector.Description = connector.Description;
            existingConnector.Details = connector.Details;
            existingConnector.Configurations = connector.Configurations;

            return Task.FromResult(true);
        }

        public Task<bool> DeleteConnectorAsync(Guid id)
        {
            var connector = _connectors.FirstOrDefault(c => c.Id == id);
            if (connector == null) return Task.FromResult(false);

            _connectors.Remove(connector);
            return Task.FromResult(true);
        }
    }
}
