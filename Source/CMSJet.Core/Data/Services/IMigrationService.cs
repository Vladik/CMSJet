using CMSJet.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public interface IMigrationService
    {
        Task<IEnumerable<Migration>> GetAllMigrationsAsync();
        Task<Migration?> GetMigrationByIdAsync(Guid id);
        Task<Guid> AddMigrationAsync(Migration migration);
        Task<bool> UpdateMigrationAsync(Migration migration);
        Task<bool> DeleteMigrationAsync(Guid id);
    }
}