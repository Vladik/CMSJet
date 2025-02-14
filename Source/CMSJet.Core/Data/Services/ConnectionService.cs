using CMSJet.Core.Data.Repositories;
using CMSJet.Core.Models;
using Microsoft.Extensions.Logging;

namespace CMSJet.Core.Data.Services;

public class ConnectionService : IConnectionService
{
    private readonly ConnectionRepository _connectionRepository;
    private readonly ILogger<ConnectionService> _logger;

    public ConnectionService(ConnectionRepository connectionRepository, ILogger<ConnectionService> logger)
    {
        _connectionRepository = connectionRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Connection>> GetAllConnectionsAsync()
    {
        _logger.LogInformation("Fetching all CMS connections...");
        return await _connectionRepository.GetAllConnectionsAsync();
    }

    public async Task<Connection?> GetConnectionByIdAsync(Guid id)
    {
        var connection = await _connectionRepository.GetConnectionByIdAsync(id);
        if (connection == null)
            _logger.LogWarning("CMS connection not found: {Id}", id);

        return connection;
    }

    public async Task<Guid> AddConnectionAsync(Connection connection)
    {
        if (string.IsNullOrWhiteSpace(connection.Name))
            throw new ArgumentException("Connection name cannot be empty.", nameof(connection.Name));

        if (connection.UserId == Guid.Empty)
            throw new ArgumentException("UserId must be provided.", nameof(connection.UserId));

        _logger.LogInformation("Adding CMS connection: {Name}", connection.Name);
        return await _connectionRepository.AddConnectionAsync(connection);
    }

    public async Task<bool> UpdateConnectionAsync(Connection connection)
    {
        if (string.IsNullOrWhiteSpace(connection.Name))
            throw new ArgumentException("Connection name cannot be empty.", nameof(connection.Name));

        _logger.LogInformation("Updating CMS connection: {Name}", connection.Name);
        return await _connectionRepository.UpdateConnectionAsync(connection);
    }

    public async Task<bool> DeleteConnectionAsync(Guid id)
    {
        _logger.LogInformation("Deleting CMS connection: {Id}", id);
        return await _connectionRepository.DeleteConnectionAsync(id);
    }
}
