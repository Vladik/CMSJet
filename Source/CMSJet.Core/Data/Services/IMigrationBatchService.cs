using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Services;

public interface IMigrationBatchService
{
    Task<IEnumerable<MigrationBatch>> GetBatchesByMigrationIdAsync(Guid migrationId);
    Task<MigrationBatch?> GetBatchByIdAsync(Guid batchId);
    Task<Guid> AddBatchAsync(MigrationBatch batch);
    Task<bool> UpdateBatchAsync(MigrationBatch batch);
    Task<bool> DeleteBatchAsync(Guid batchId);
}