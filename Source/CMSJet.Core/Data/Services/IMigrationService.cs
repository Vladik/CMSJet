using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Services;

public interface IMigrationService
{
    Task<IEnumerable<Migration>> GetAllMigrationsAsync();
    Task<Migration?> GetMigrationByIdAsync(Guid id);
    Task<Guid> AddMigrationAsync(Migration migration);
    Task<bool> UpdateMigrationAsync(Migration migration);
}