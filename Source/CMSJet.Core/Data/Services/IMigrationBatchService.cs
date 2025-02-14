using CMSJet.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public interface IMigrationBatchService
    {
        Task<IEnumerable<MigrationBatch>> GetAllBatchesAsync();
        Task<IEnumerable<MigrationBatch>> GetBatchesByMigrationIdAsync(Guid migrationId);
        Task<MigrationBatch?> GetBatchByIdAsync(Guid id);
        Task<Guid> AddBatchAsync(MigrationBatch batch);
        Task<bool> UpdateBatchAsync(MigrationBatch batch);
        Task<bool> DeleteBatchAsync(Guid id);
        Task<bool> DisableBatchAsync(Guid id);
        Task<bool> EnableBatchAsync(Guid id);
    }
}