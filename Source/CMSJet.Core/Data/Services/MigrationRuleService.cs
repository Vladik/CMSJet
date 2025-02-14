using CMSJet.Core.Data.Repositories;
using CMSJet.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public class MigrationRuleService : IMigrationRuleService
    {
        private readonly MigrationRuleRepository _migrationRuleRepository;
        private readonly ILogger<MigrationRuleService> _logger;

        public MigrationRuleService(MigrationRuleRepository migrationRuleRepository, ILogger<MigrationRuleService> logger)
        {
            _migrationRuleRepository = migrationRuleRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MigrationRule>> GetAllRulesAsync()
        {
            _logger.LogInformation("Fetching all migration rules.");
            return await _migrationRuleRepository.GetAllRulesAsync();
        }

        public async Task<IEnumerable<MigrationRule>> GetRulesByBatchIdAsync(Guid batchId)
        {
            _logger.LogInformation("Fetching rules for batch: {BatchId}", batchId);
            return await _migrationRuleRepository.GetRulesByBatchIdAsync(batchId);
        }

        public async Task<MigrationRule?> GetRuleByIdAsync(Guid id)
        {
            var rule = await _migrationRuleRepository.GetRuleByIdAsync(id);
            if (rule == null)
            {
                _logger.LogWarning("Migration rule not found: {RuleId}", id);
            }
            return rule;
        }

        public async Task<Guid> AddRuleAsync(MigrationRule rule)
        {
            rule.Id = Guid.NewGuid();
            _logger.LogInformation("Adding new rule: {RuleId} for batch {BatchId}", rule.Id, rule.BatchId);
            return await _migrationRuleRepository.AddRuleAsync(rule);
        }

        public async Task<bool> UpdateRuleAsync(MigrationRule rule)
        {
            var existingRule = await _migrationRuleRepository.GetRuleByIdAsync(rule.Id);
            if (existingRule == null)
            {
                _logger.LogWarning("Cannot update. Rule not found: {RuleId}", rule.Id);
                return false;
            }

            _logger.LogInformation("Updating rule: {RuleId}", rule.Id);
            return await _migrationRuleRepository.UpdateRuleAsync(rule);
        }

        public async Task<bool> DeleteRuleAsync(Guid id)
        {
            var rule = await _migrationRuleRepository.GetRuleByIdAsync(id);
            if (rule == null)
            {
                _logger.LogWarning("Cannot delete. Rule not found: {RuleId}", id);
                return false;
            }

            _logger.LogInformation("Deleting rule: {RuleId}", id);
            return await _migrationRuleRepository.DeleteRuleAsync(id);
        }
    }
}
