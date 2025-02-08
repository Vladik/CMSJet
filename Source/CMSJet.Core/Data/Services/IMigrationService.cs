using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Services;

public interface IMigrationService
{
    Task<IEnumerable<Migration>> GetAllMigrationsAsync();
    Task<Migration?> GetMigrationByIdAsync(int id);
    Task<int> AddMigrationAsync(Migration migration);
}