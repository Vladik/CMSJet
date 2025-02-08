using CMSJet.Core.Data.Repositories;
using CMSJet.Core.Models;
using Microsoft.Extensions.Logging;

namespace CMSJet.Core.Data.Services;

public class MigrationService : IMigrationService
{
    private readonly MigrationRepository _migrationRepository;
    private readonly ILogger<MigrationService> _logger;

    public MigrationService(MigrationRepository migrationRepository, ILogger<MigrationService> logger)
    {
        _migrationRepository = migrationRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Migration>> GetAllMigrationsAsync()
    {
        _logger.LogInformation("Fetching all migrations...");
        return await _migrationRepository.GetAllMigrationsAsync();
    }

    public async Task<Migration?> GetMigrationByIdAsync(int id)
    {
        if (id <= 0)
        {
            _logger.LogWarning("Invalid migration ID: {Id}", id);
            return null;
        }

        var migration = await _migrationRepository.GetMigrationByIdAsync(id);

        if (migration == null)
            _logger.LogWarning("Migration not found: {Id}", id);

        return migration;
    }

    public async Task<int> AddMigrationAsync(Migration migration)
    {
        if (string.IsNullOrWhiteSpace(migration.Name))
            throw new ArgumentException("Migration name cannot be empty.", nameof(migration.Name));

        if (migration.UserId == Guid.Empty)
            throw new ArgumentException("UserId must be provided.", nameof(migration.UserId));

        _logger.LogInformation("Adding migration: {Name}", migration.Name);

        return await _migrationRepository.AddMigrationAsync(migration);
    }
}