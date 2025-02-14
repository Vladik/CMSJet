using CMSJet.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public interface IConnectionService
    {
        Task<IEnumerable<Connection>> GetAllConnectionsByUserIdAsync(Guid userId);
        Task<Connection?> GetConnectionByIdAsync(Guid id);
        Task<Guid> AddConnectionAsync(Connection connection);
        Task<bool> UpdateConnectionAsync(Connection connection);
        Task<bool> DeleteConnectionAsync(Guid id);
    }
}