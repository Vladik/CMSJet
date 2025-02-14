using CMSJet.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public interface IMigrationAttemptBatchService
    {
        Task<IEnumerable<MigrationAttemptBatch>> GetAllAttemptBatchesAsync();
        Task<IEnumerable<MigrationAttemptBatch>> GetAttemptBatchesByAttemptIdAsync(Guid attemptId);
        Task<MigrationAttemptBatch?> GetAttemptBatchByIdAsync(Guid id);
        Task<Guid> AddAttemptBatchAsync(MigrationAttemptBatch attemptBatch);
        Task<bool> UpdateAttemptBatchAsync(MigrationAttemptBatch attemptBatch);
        Task<bool> DeleteAttemptBatchAsync(Guid id);
    }
}