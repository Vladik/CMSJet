using CMSJet.Core.Data.Repositories;
using CMSJet.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public class MigrationLogService : IMigrationLogService
    {
        private readonly MigrationLogRepository _logRepository;
        private readonly ILogger<MigrationLogService> _logger;

        public MigrationLogService(MigrationLogRepository logRepository, ILogger<MigrationLogService> logger)
        {
            _logRepository = logRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MigrationLog>> GetLogsByMigrationAttemptIdAsync(Guid attemptId)
        {
            _logger.LogInformation("Fetching logs for migration attempt ID: {AttemptId}", attemptId);
            return await _logRepository.GetLogsByMigrationAttemptIdAsync(attemptId);
        }

        public async Task<MigrationLog?> GetLogByIdAsync(Guid id)
        {
            var log = await _logRepository.GetLogByIdAsync(id);
            if (log == null)
            {
                _logger.LogWarning("Migration log not found: {Id}", id);
            }
            return log;
        }

        public async Task<Guid> AddLogAsync(MigrationLog log)
        {
            log.Id = Guid.NewGuid();
            log.Timestamp = DateTime.UtcNow;

            _logger.LogInformation("Adding new migration log for attempt ID: {AttemptId}", log.MigrationAttemptId);
            return await _logRepository.AddLogAsync(log);
        }
    }
}