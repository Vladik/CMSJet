using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Repositories
{
    public class MigrationBatchRepository
    {
        private readonly List<MigrationBatch> _batches = new();

        public Task<IEnumerable<MigrationBatch>> GetAllBatchesAsync() =>
            Task.FromResult<IEnumerable<MigrationBatch>>(_batches.OrderBy(b => b.Priority));

        public Task<IEnumerable<MigrationBatch>> GetBatchesByMigrationIdAsync(Guid migrationId) =>
            Task.FromResult<IEnumerable<MigrationBatch>>(_batches.Where(b => b.MigrationId == migrationId).OrderBy(b => b.Priority));

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
            existingBatch.Priority = batch.Priority;
            existingBatch.Enabled = batch.Enabled;
            existingBatch.StopOnFailure = batch.StopOnFailure;
            existingBatch.FieldMappings = batch.FieldMappings;
            existingBatch.Rules = batch.Rules;
            existingBatch.Configurations = batch.Configurations;
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