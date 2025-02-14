using CMSJet.Core.Data.Repositories;
using CMSJet.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public class MigrationAttemptBatchService : IMigrationAttemptBatchService
    {
        private readonly MigrationAttemptBatchRepository _attemptBatchRepository;
        private readonly ILogger<MigrationAttemptBatchService> _logger;

        public MigrationAttemptBatchService(MigrationAttemptBatchRepository attemptBatchRepository, ILogger<MigrationAttemptBatchService> logger)
        {
            _attemptBatchRepository = attemptBatchRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MigrationAttemptBatch>> GetAllAttemptBatchesAsync()
        {
            _logger.LogInformation("Fetching all migration attempt batches...");
            return await _attemptBatchRepository.GetAllAttemptBatchesAsync();
        }

        public async Task<IEnumerable<MigrationAttemptBatch>> GetAttemptBatchesByAttemptIdAsync(Guid attemptId)
        {
            _logger.LogInformation("Fetching migration attempt batches for attempt ID: {AttemptId}", attemptId);
            return await _attemptBatchRepository.GetAttemptBatchesByAttemptIdAsync(attemptId);
        }

        public async Task<MigrationAttemptBatch?> GetAttemptBatchByIdAsync(Guid id)
        {
            var attemptBatch = await _attemptBatchRepository.GetAttemptBatchByIdAsync(id);
            if (attemptBatch == null)
            {
                _logger.LogWarning("Migration attempt batch not found: {Id}", id);
            }
            return attemptBatch;
        }

        public async Task<Guid> AddAttemptBatchAsync(MigrationAttemptBatch attemptBatch)
        {
            if (attemptBatch.BatchId == Guid.Empty || attemptBatch.AttemptId == Guid.Empty)
                throw new ArgumentException("AttemptId and BatchId cannot be empty.", nameof(attemptBatch));

            _logger.LogInformation("Adding new migration attempt batch for batch ID: {BatchId}", attemptBatch.BatchId);
            return await _attemptBatchRepository.AddAttemptBatchAsync(attemptBatch);
        }

        public async Task<bool> UpdateAttemptBatchAsync(MigrationAttemptBatch attemptBatch)
        {
            if (attemptBatch.BatchId == Guid.Empty || attemptBatch.AttemptId == Guid.Empty)
                throw new ArgumentException("AttemptId and BatchId cannot be empty.", nameof(attemptBatch));

            _logger.LogInformation("Updating migration attempt batch ID: {Id}", attemptBatch.Id);
            return await _attemptBatchRepository.UpdateAttemptBatchAsync(attemptBatch);
        }

        public async Task<bool> DeleteAttemptBatchAsync(Guid id)
        {
            _logger.LogInformation("Deleting migration attempt batch with ID: {Id}", id);
            return await _attemptBatchRepository.DeleteAttemptBatchAsync(id);
        }
    }
}
