using CMSJet.Core.Data.Repositories;
using CMSJet.Core.Models;
using Microsoft.Extensions.Logging;

namespace CMSJet.Core.Data.Services;

public class MigrationBatchService : IMigrationBatchService
{
    private readonly MigrationBatchRepository _batchRepository;
    private readonly ILogger<MigrationBatchService> _logger;

    public MigrationBatchService(MigrationBatchRepository batchRepository, ILogger<MigrationBatchService> logger)
    {
        _batchRepository = batchRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<MigrationBatch>> GetBatchesByMigrationIdAsync(Guid migrationId)
    {
        _logger.LogInformation("Fetching all batches for migration: {MigrationId}", migrationId);
        return await _batchRepository.GetBatchesByMigrationIdAsync(migrationId);
    }

    public async Task<MigrationBatch?> GetBatchByIdAsync(Guid batchId)
    {
        return await _batchRepository.GetBatchByIdAsync(batchId);
    }

    public async Task<Guid> AddBatchAsync(MigrationBatch batch)
    {
        return await _batchRepository.AddBatchAsync(batch);
    }

    public async Task<bool> UpdateBatchAsync(MigrationBatch batch)
    {
        return await _batchRepository.UpdateBatchAsync(batch);
    }

    public async Task<bool> DeleteBatchAsync(Guid batchId)
    {
        return await _batchRepository.DeleteBatchAsync(batchId);
    }
}