using CMSJet.Core.Data.Repositories;
using CMSJet.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly ConnectionRepository _connectionRepository;
        private readonly ILogger<ConnectionService> _logger;

        public ConnectionService(ConnectionRepository connectionRepository, ILogger<ConnectionService> logger)
        {
            _connectionRepository = connectionRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Connection>> GetAllConnectionsByUserIdAsync(Guid userId)
        {
            _logger.LogInformation("Fetching all CMS connections...");
            return await _connectionRepository.GetAllConnectionsByUserIdAsync(userId);
        }

        public async Task<Connection?> GetConnectionByIdAsync(Guid id)
        {
            var connection = await _connectionRepository.GetConnectionByIdAsync(id);
            if (connection == null)
            {
                _logger.LogWarning("Connection not found: {Id}", id);
            }
            return connection;
        }

        public async Task<Guid> AddConnectionAsync(Connection connection)
        {
            if (string.IsNullOrWhiteSpace(connection.Name))
                throw new ArgumentException("Connection name cannot be empty.", nameof(connection.Name));

            _logger.LogInformation("Adding new connection: {Name}", connection.Name);
            return await _connectionRepository.AddConnectionAsync(connection);
        }

        public async Task<bool> UpdateConnectionAsync(Connection connection)
        {
            if (string.IsNullOrWhiteSpace(connection.Name))
                throw new ArgumentException("Connection name cannot be empty.", nameof(connection.Name));

            _logger.LogInformation("Updating connection: {Name}", connection.Name);
            return await _connectionRepository.UpdateConnectionAsync(connection);
        }

        public async Task<bool> DeleteConnectionAsync(Guid id)
        {
            _logger.LogInformation("Deleting connection with ID: {Id}", id);
            return await _connectionRepository.DeleteConnectionAsync(id);
        }
    }
}
