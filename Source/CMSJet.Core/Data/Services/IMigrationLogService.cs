using CMSJet.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public interface IMigrationLogService
    {
        Task<IEnumerable<MigrationLog>> GetLogsByMigrationAttemptIdAsync(Guid attemptId);
        Task<MigrationLog?> GetLogByIdAsync(Guid id);
        Task<Guid> AddLogAsync(MigrationLog log);
    }
}