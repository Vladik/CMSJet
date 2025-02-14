using System;
using System.Collections.Generic;
using System.Linq;
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
                UserId = Guid.Parse("56d287d3-3d95-44b5-ad9c-f0d84b6f59eb"),
                ConnectorId = Guid.NewGuid(),
                Name = "Demo Source Connection",
                Description = "Source system connection",
                Configurations = new(),
                LastSync = DateTime.UtcNow
            },
            new Connection
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("56d287d3-3d95-44b5-ad9c-f0d84b6f59eb"),
                ConnectorId = Guid.NewGuid(),
                Name = "Demo Target Connection",
                Description = "Target system connection",
                Configurations = new(),
                LastSync = DateTime.UtcNow
            }
        };
        
        public Task<IEnumerable<Connection>> GetAllConnectionsByUserIdAsync(Guid userId) =>
            Task.FromResult<IEnumerable<Connection>>(_connections.FindAll(c => c.UserId == userId));
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
            existingConnection.Description = connection.Description;
            existingConnection.Configurations = connection.Configurations;
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
