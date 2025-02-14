using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Repositories
{
    public class ConnectionRepository
    {
        private readonly List<Connection> _connections = new()
        {
            new Connection
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                ConnectorId = Guid.NewGuid(),
                Name = "Demo Source Connection",
                Details = JsonDocument.Parse("{\"type\":\"source\",\"config\":\"value\"}"),
                LastSync = DateTime.UtcNow
            },
            new Connection
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                ConnectorId = Guid.NewGuid(),
                Name = "Demo Target Connection",
                Details = JsonDocument.Parse("{\"type\":\"target\",\"config\":\"value\"}"),
                LastSync = DateTime.UtcNow
            }
        };

        public Task<IEnumerable<Connection>> GetAllConnectionsAsync() =>
            Task.FromResult<IEnumerable<Connection>>(_connections);

        public Task<Connection?> GetConnectionByIdAsync(Guid id) =>
            Task.FromResult(_connections.FirstOrDefault(c => c.Id == id));

        public Task<Guid> AddConnectionAsync(Connection connection)
        {
            connection.Id = Guid.NewGuid();
            _connections.Add(connection);
            return Task.FromResult(connection.Id);
        }

        public Task<bool> UpdateConnectionAsync(Connection connection)
        {
            var existingConnection = _connections.FirstOrDefault(c => c.Id == connection.Id);
            if (existingConnection == null) return Task.FromResult(false);

            existingConnection.Name = connection.Name;
            existingConnection.Details = connection.Details;
            existingConnection.LastSync = connection.LastSync;

            return Task.FromResult(true);
        }

        public Task<bool> DeleteConnectionAsync(Guid id)
        {
            var connection = _connections.FirstOrDefault(c => c.Id == id);
            if (connection == null) return Task.FromResult(false);

            _connections.Remove(connection);
            return Task.FromResult(true);
        }
    }
}
