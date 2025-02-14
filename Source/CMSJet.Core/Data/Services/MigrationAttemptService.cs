using CMSJet.Core.Data.Repositories;
using CMSJet.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public class MigrationAttemptService : IMigrationAttemptService
    {
        private readonly MigrationAttemptRepository _attemptRepository;
        private readonly ILogger<MigrationAttemptService> _logger;

        public MigrationAttemptService(MigrationAttemptRepository attemptRepository, ILogger<MigrationAttemptService> logger)
        {
            _attemptRepository = attemptRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MigrationAttempt>> GetAllAttemptsAsync()
        {
            _logger.LogInformation("Fetching all migration attempts...");
            return await _attemptRepository.GetAllAttemptsAsync();
        }

        public async Task<IEnumerable<MigrationAttempt>> GetAttemptsByMigrationIdAsync(Guid migrationId)
        {
            _logger.LogInformation("Fetching migration attempts for migration ID: {MigrationId}", migrationId);
            return await _attemptRepository.GetAttemptsByMigrationIdAsync(migrationId);
        }

        public async Task<MigrationAttempt?> GetAttemptByIdAsync(Guid id)
        {
            var attempt = await _attemptRepository.GetAttemptByIdAsync(id);
            if (attempt == null)
            {
                _logger.LogWarning("Migration attempt not found: {Id}", id);
            }
            return attempt;
        }

        public async Task<Guid> AddAttemptAsync(MigrationAttempt attempt)
        {
            if (attempt.MigrationId == Guid.Empty)
                throw new ArgumentException("MigrationId cannot be empty.", nameof(attempt));

            attempt.StartedAt = DateTime.UtcNow;
            _logger.LogInformation("Adding new migration attempt for migration ID: {MigrationId}", attempt.MigrationId);
            return await _attemptRepository.AddAttemptAsync(attempt);
        }

        public async Task<bool> UpdateAttemptAsync(MigrationAttempt attempt)
        {
            if (attempt.MigrationId == Guid.Empty)
                throw new ArgumentException("MigrationId cannot be empty.", nameof(attempt));

            _logger.LogInformation("Updating migration attempt ID: {Id}", attempt.Id);
            return await _attemptRepository.UpdateAttemptAsync(attempt);
        }

        public async Task<bool> DeleteAttemptAsync(Guid id)
        {
            _logger.LogInformation("Deleting migration attempt with ID: {Id}", id);
            return await _attemptRepository.DeleteAttemptAsync(id);
        }

        public async Task<bool> CancelAttemptAsync(Guid id)
        {
            var attempt = await _attemptRepository.GetAttemptByIdAsync(id);
            if (attempt == null || attempt.Status != MigrationAttemptStatus.InProgress)
            {
                _logger.LogWarning("Attempt cannot be canceled because it was not found or is not in progress.");
                return false;
            }

            attempt.Status = MigrationAttemptStatus.Canceled;
            attempt.CompletedAt = DateTime.UtcNow;

            _logger.LogInformation("Canceling migration attempt ID: {Id}", id);
            return await _attemptRepository.UpdateAttemptAsync(attempt);
        }
    }
}