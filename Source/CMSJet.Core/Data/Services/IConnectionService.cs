using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Services;
public interface IConnectionService
{
    Task<IEnumerable<Connection>> GetAllConnectionsAsync();
    Task<Connection?> GetConnectionByIdAsync(Guid id);
    Task<Guid> AddConnectionAsync(Connection connection);
    Task<bool> UpdateConnectionAsync(Connection connection);
    Task<bool> DeleteConnectionAsync(Guid id);
}