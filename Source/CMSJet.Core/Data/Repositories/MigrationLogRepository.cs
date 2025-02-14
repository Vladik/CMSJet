using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Repositories
{
    public class MigrationLogRepository
    {
        private readonly List<MigrationLog> _logs = new();

        public Task<IEnumerable<MigrationLog>> GetAllLogsAsync() =>
            Task.FromResult<IEnumerable<MigrationLog>>(_logs.OrderByDescending(log => log.Timestamp));

        public Task<IEnumerable<MigrationLog>> GetLogsByMigrationAttemptIdAsync(Guid attemptId) =>
            Task.FromResult<IEnumerable<MigrationLog>>(_logs.Where(log => log.MigrationAttemptId == attemptId).OrderByDescending(log => log.Timestamp));

        public Task<MigrationLog?> GetLogByIdAsync(Guid id) =>
            Task.FromResult(_logs.FirstOrDefault(log => log.Id == id));

        public Task<Guid> AddLogAsync(MigrationLog log)
        {
            log.Id = Guid.NewGuid();
            log.Timestamp = DateTime.UtcNow;
            _logs.Add(log);
            return Task.FromResult(log.Id);
        }

        public Task<bool> DeleteLogAsync(Guid id)
        {
            var log = _logs.FirstOrDefault(l => l.Id == id);
            if (log == null) return Task.FromResult(false);

            _logs.Remove(log);
            return Task.FromResult(true);
        }
    }
}