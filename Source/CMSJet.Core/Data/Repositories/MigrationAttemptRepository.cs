using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Repositories
{
    public class MigrationAttemptRepository
    {
        private readonly List<MigrationAttempt> _attempts = new();

        public Task<IEnumerable<MigrationAttempt>> GetAllAttemptsAsync() =>
            Task.FromResult<IEnumerable<MigrationAttempt>>(_attempts.OrderByDescending(a => a.StartedAt));

        public Task<IEnumerable<MigrationAttempt>> GetAttemptsByMigrationIdAsync(Guid migrationId) =>
            Task.FromResult<IEnumerable<MigrationAttempt>>(_attempts.Where(a => a.MigrationId == migrationId));

        public Task<MigrationAttempt?> GetAttemptByIdAsync(Guid id) =>
            Task.FromResult(_attempts.FirstOrDefault(a => a.Id == id));

        public Task<Guid> AddAttemptAsync(MigrationAttempt attempt)
        {
            attempt.Id = Guid.NewGuid();
            attempt.StartedAt = DateTime.UtcNow;
            _attempts.Add(attempt);
            return Task.FromResult(attempt.Id);
        }

        public Task<bool> UpdateAttemptAsync(MigrationAttempt attempt)
        {
            var existingAttempt = _attempts.FirstOrDefault(a => a.Id == attempt.Id);
            if (existingAttempt == null) return Task.FromResult(false);

            existingAttempt.Status = attempt.Status;
            existingAttempt.StartedAt = attempt.StartedAt;
            existingAttempt.CompletedAt = attempt.CompletedAt;
            existingAttempt.AttemptBatches = attempt.AttemptBatches;

            return Task.FromResult(true);
        }

        public Task<bool> DeleteAttemptAsync(Guid id)
        {
            var attempt = _attempts.FirstOrDefault(a => a.Id == id);
            if (attempt == null) return Task.FromResult(false);

            _attempts.Remove(attempt);
            return Task.FromResult(true);
        }
    }
}