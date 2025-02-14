using CMSJet.Core.Data.Repositories;
using CMSJet.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public class MigrationBatchService : IMigrationBatchService
    {
        private readonly MigrationBatchRepository _batchRepository;
        private readonly ILogger<MigrationBatchService> _logger;

        public MigrationBatchService(MigrationBatchRepository batchRepository, ILogger<MigrationBatchService> logger)
        {
            _batchRepository = batchRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MigrationBatch>> GetAllBatchesAsync()
        {
            _logger.LogInformation("Fetching all migration batches...");
            return await _batchRepository.GetAllBatchesAsync();
        }

        public async Task<IEnumerable<MigrationBatch>> GetBatchesByMigrationIdAsync(Guid migrationId)
        {
            _logger.LogInformation("Fetching migration batches for migration ID: {MigrationId}", migrationId);
            return await _batchRepository.GetBatchesByMigrationIdAsync(migrationId);
        }

        public async Task<MigrationBatch?> GetBatchByIdAsync(Guid id)
        {
            var batch = await _batchRepository.GetBatchByIdAsync(id);
            if (batch == null)
            {
                _logger.LogWarning("Migration batch not found: {Id}", id);
            }
            return batch;
        }

        public async Task<Guid> AddBatchAsync(MigrationBatch batch)
        {
            if (batch.MigrationId == Guid.Empty)
                throw new ArgumentException("MigrationId cannot be empty.", nameof(batch));

            batch.CreatedAt = DateTime.UtcNow;
            batch.UpdatedAt = DateTime.UtcNow;

            _logger.LogInformation("Adding new migration batch for migration ID: {MigrationId}", batch.MigrationId);
            return await _batchRepository.AddBatchAsync(batch);
        }

        public async Task<bool> UpdateBatchAsync(MigrationBatch batch)
        {
            if (batch.MigrationId == Guid.Empty)
                throw new ArgumentException("MigrationId cannot be empty.", nameof(batch));

            batch.UpdatedAt = DateTime.UtcNow;
            _logger.LogInformation("Updating migration batch ID: {Id}", batch.Id);
            return await _batchRepository.UpdateBatchAsync(batch);
        }

        public async Task<bool> DeleteBatchAsync(Guid id)
        {
            _logger.LogInformation("Deleting migration batch with ID: {Id}", id);
            return await _batchRepository.DeleteBatchAsync(id);
        }

        public async Task<bool> DisableBatchAsync(Guid id)
        {
            var batch = await _batchRepository.GetBatchByIdAsync(id);
            if (batch == null)
            {
                _logger.LogWarning("Batch not found: {Id}", id);
                return false;
            }

            batch.Enabled = false;
            batch.UpdatedAt = DateTime.UtcNow;

            _logger.LogInformation("Disabling migration batch ID: {Id}", id);
            return await _batchRepository.UpdateBatchAsync(batch);
        }

        public async Task<bool> EnableBatchAsync(Guid id)
        {
            var batch = await _batchRepository.GetBatchByIdAsync(id);
            if (batch == null)
            {
                _logger.LogWarning("Batch not found: {Id}", id);
                return false;
            }

            batch.Enabled = true;
            batch.UpdatedAt = DateTime.UtcNow;

            _logger.LogInformation("Enabling migration batch ID: {Id}", id);
            return await _batchRepository.UpdateBatchAsync(batch);
        }
    }
}