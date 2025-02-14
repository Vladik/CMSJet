using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Repositories
{
    public class MigrationRepository
    {
        private readonly List<Migration> _migrations = new()
        {
            new Migration
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Demo Migration 1",
                SourceConnectionId = Guid.NewGuid(),
                TargetConnectionId = Guid.NewGuid(),
                Status = MigrationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Migration
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Demo Migration 2",
                SourceConnectionId = Guid.NewGuid(),
                TargetConnectionId = Guid.NewGuid(),
                Status = MigrationStatus.InProgress,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        public Task<IEnumerable<Migration>> GetAllMigrationsAsync() =>
            Task.FromResult<IEnumerable<Migration>>(_migrations.OrderByDescending(m => m.CreatedAt));

        public Task<Migration?> GetMigrationByIdAsync(Guid id) =>
            Task.FromResult(_migrations.FirstOrDefault(m => m.Id == id));

        public Task<Guid> AddMigrationAsync(Migration migration)
        {
            migration.Id = Guid.NewGuid();
            migration.CreatedAt = DateTime.UtcNow;
            migration.UpdatedAt = DateTime.UtcNow;
            _migrations.Add(migration);
            return Task.FromResult(migration.Id);
        }

        public Task<bool> UpdateMigrationAsync(Migration migration)
        {
            var existingMigration = _migrations.FirstOrDefault(m => m.Id == migration.Id);
            if (existingMigration == null) return Task.FromResult(false);

            existingMigration.Name = migration.Name;
            existingMigration.SourceConnectionId = migration.SourceConnectionId;
            existingMigration.TargetConnectionId = migration.TargetConnectionId;
            existingMigration.Status = migration.Status;
            existingMigration.UpdatedAt = DateTime.UtcNow;

            return Task.FromResult(true);
        }
    }
}
