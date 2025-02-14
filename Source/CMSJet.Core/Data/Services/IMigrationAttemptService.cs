using CMSJet.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public interface IMigrationAttemptService
    {
        Task<IEnumerable<MigrationAttempt>> GetAllAttemptsAsync();
        Task<IEnumerable<MigrationAttempt>> GetAttemptsByMigrationIdAsync(Guid migrationId);
        Task<MigrationAttempt?> GetAttemptByIdAsync(Guid id);
        Task<Guid> AddAttemptAsync(MigrationAttempt attempt);
        Task<bool> UpdateAttemptAsync(MigrationAttempt attempt);
        Task<bool> DeleteAttemptAsync(Guid id);
        Task<bool> CancelAttemptAsync(Guid id);
    }
}