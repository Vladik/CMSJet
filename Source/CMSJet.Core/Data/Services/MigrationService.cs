using CMSJet.Core.Data.Repositories;
using CMSJet.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
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
            _logger.LogInformation("Fetching all migrations.");
            return await _migrationRepository.GetAllMigrationsAsync();
        }

        public async Task<Migration?> GetMigrationByIdAsync(Guid id)
        {
            var migration = await _migrationRepository.GetMigrationByIdAsync(id);
            if (migration == null)
            {
                _logger.LogWarning("Migration not found: {MigrationId}", id);
            }
            return migration;
        }

        public async Task<Guid> AddMigrationAsync(Migration migration)
        {
            migration.Id = Guid.NewGuid();
            migration.CreatedAt = DateTime.UtcNow;
            migration.UpdatedAt = DateTime.UtcNow;
            migration.Status = MigrationStatus.Pending;

            _logger.LogInformation("Adding new migration: {MigrationName}", migration.Name);
            return await _migrationRepository.AddMigrationAsync(migration);
        }

        public async Task<bool> UpdateMigrationAsync(Migration migration)
        {
            var existingMigration = await _migrationRepository.GetMigrationByIdAsync(migration.Id);
            if (existingMigration == null)
            {
                _logger.LogWarning("Cannot update. Migration not found: {MigrationId}", migration.Id);
                return false;
            }

            migration.UpdatedAt = DateTime.UtcNow;
            _logger.LogInformation("Updating migration: {MigrationId}", migration.Id);
            return await _migrationRepository.UpdateMigrationAsync(migration);
        }

        public async Task<bool> DeleteMigrationAsync(Guid id)
        {
            var migration = await _migrationRepository.GetMigrationByIdAsync(id);
            if (migration == null)
            {
                _logger.LogWarning("Cannot delete. Migration not found: {MigrationId}", id);
                return false;
            }

            _logger.LogInformation("Deleting migration: {MigrationId}", id);
            return await _migrationRepository.DeleteMigrationAsync(id);
        }
    }
}