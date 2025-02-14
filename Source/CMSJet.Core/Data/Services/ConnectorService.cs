using CMSJet.Core.Data.Repositories;
using CMSJet.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public class ConnectorService : IConnectorService
    {
        private readonly ConnectorRepository _connectorRepository;
        private readonly ILogger<ConnectorService> _logger;

        public ConnectorService(ConnectorRepository connectorRepository, ILogger<ConnectorService> logger)
        {
            _connectorRepository = connectorRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Connector>> GetAllConnectorsAsync()
        {
            _logger.LogInformation("Fetching all connectors...");
            return await _connectorRepository.GetAllConnectorsAsync();
        }

        public async Task<Connector?> GetConnectorByIdAsync(Guid id)
        {
            var connector = await _connectorRepository.GetConnectorByIdAsync(id);
            if (connector == null)
            {
                _logger.LogWarning("Connector not found: {Id}", id);
            }
            return connector;
        }

        public async Task<Guid> AddConnectorAsync(Connector connector)
        {
            if (string.IsNullOrWhiteSpace(connector.Name))
                throw new ArgumentException("Connector name cannot be empty.", nameof(connector.Name));

            _logger.LogInformation("Adding new connector: {Name}", connector.Name);
            return await _connectorRepository.AddConnectorAsync(connector);
        }

        public async Task<bool> UpdateConnectorAsync(Connector connector)
        {
            if (string.IsNullOrWhiteSpace(connector.Name))
                throw new ArgumentException("Connector name cannot be empty.", nameof(connector.Name));

            _logger.LogInformation("Updating connector: {Name}", connector.Name);
            return await _connectorRepository.UpdateConnectorAsync(connector);
        }

        public async Task<bool> DeleteConnectorAsync(Guid id)
        {
            _logger.LogInformation("Deleting connector with ID: {Id}", id);
            return await _connectorRepository.DeleteConnectorAsync(id);
        }
    }
}