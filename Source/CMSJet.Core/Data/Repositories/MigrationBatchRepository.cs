using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Repositories
{
    public class MigrationBatchRepository
    {
        private readonly List<MigrationBatch> _batches = new()
        {
            new MigrationBatch
            {
                Id = Guid.NewGuid(),
                MigrationId = Guid.NewGuid(),
                Name = "Batch 1",
                SourceType = "Article",
                TargetType = "Blog Post",
                Status = "pending",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new MigrationBatch
            {
                Id = Guid.NewGuid(),
                MigrationId = Guid.NewGuid(),
                Name = "Batch 2",
                SourceType = "Product",
                TargetType = "Catalog Item",
                Status = "pending",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        public Task<IEnumerable<MigrationBatch>> GetAllBatchesAsync() =>
            Task.FromResult<IEnumerable<MigrationBatch>>(_batches);

        public Task<IEnumerable<MigrationBatch>> GetBatchesByMigrationIdAsync(Guid migrationId) =>
            Task.FromResult<IEnumerable<MigrationBatch>>(_batches.Where(b => b.MigrationId == migrationId));

        public Task<MigrationBatch?> GetBatchByIdAsync(Guid id) =>
            Task.FromResult(_batches.FirstOrDefault(b => b.Id == id));

        public Task<Guid> AddBatchAsync(MigrationBatch batch)
        {
            batch.Id = Guid.NewGuid();
            batch.CreatedAt = DateTime.UtcNow;
            batch.UpdatedAt = DateTime.UtcNow;
            _batches.Add(batch);
            return Task.FromResult(batch.Id);
        }

        public Task<bool> UpdateBatchAsync(MigrationBatch batch)
        {
            var existingBatch = _batches.FirstOrDefault(b => b.Id == batch.Id);
            if (existingBatch == null) return Task.FromResult(false);

            existingBatch.Name = batch.Name;
            existingBatch.SourceType = batch.SourceType;
            existingBatch.TargetType = batch.TargetType;
            existingBatch.Status = batch.Status;
            existingBatch.UpdatedAt = DateTime.UtcNow;

            return Task.FromResult(true);
        }

        public Task<bool> DeleteBatchAsync(Guid id)
        {
            var batch = _batches.FirstOrDefault(b => b.Id == id);
            if (batch == null) return Task.FromResult(false);

            _batches.Remove(batch);
            return Task.FromResult(true);
        }
    }
}
