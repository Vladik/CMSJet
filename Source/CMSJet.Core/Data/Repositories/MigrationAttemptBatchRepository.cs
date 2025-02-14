using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Repositories
{
    public class MigrationAttemptBatchRepository
    {
        private readonly List<MigrationAttemptBatch> _attemptBatches = new();

        public Task<IEnumerable<MigrationAttemptBatch>> GetAllAttemptBatchesAsync() =>
            Task.FromResult<IEnumerable<MigrationAttemptBatch>>(_attemptBatches);

        public Task<IEnumerable<MigrationAttemptBatch>> GetAttemptBatchesByAttemptIdAsync(Guid attemptId) =>
            Task.FromResult<IEnumerable<MigrationAttemptBatch>>(_attemptBatches.Where(ab => ab.AttemptId == attemptId));

        public Task<MigrationAttemptBatch?> GetAttemptBatchByIdAsync(Guid id) =>
            Task.FromResult(_attemptBatches.FirstOrDefault(ab => ab.Id == id));

        public Task<Guid> AddAttemptBatchAsync(MigrationAttemptBatch attemptBatch)
        {
            attemptBatch.Id = Guid.NewGuid();
            attemptBatch.StartedAt = DateTime.UtcNow;
            _attemptBatches.Add(attemptBatch);
            return Task.FromResult(attemptBatch.Id);
        }

        public Task<bool> UpdateAttemptBatchAsync(MigrationAttemptBatch attemptBatch)
        {
            var existingAttemptBatch = _attemptBatches.FirstOrDefault(ab => ab.Id == attemptBatch.Id);
            if (existingAttemptBatch == null) return Task.FromResult(false);

            existingAttemptBatch.Status = attemptBatch.Status;
            existingAttemptBatch.StartedAt = attemptBatch.StartedAt;
            existingAttemptBatch.CompletedAt = attemptBatch.CompletedAt;

            return Task.FromResult(true);
        }

        public Task<bool> DeleteAttemptBatchAsync(Guid id)
        {
            var attemptBatch = _attemptBatches.FirstOrDefault(ab => ab.Id == id);
            if (attemptBatch == null) return Task.FromResult(false);

            _attemptBatches.Remove(attemptBatch);
            return Task.FromResult(true);
        }
    }
}
