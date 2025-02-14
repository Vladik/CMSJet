using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Repositories
{
    public class MigrationRuleRepository
    {
        private readonly List<MigrationRule> _rules = new();

        public Task<IEnumerable<MigrationRule>> GetAllRulesAsync() =>
            Task.FromResult<IEnumerable<MigrationRule>>(_rules.OrderBy(r => r.SortOrder));

        public Task<IEnumerable<MigrationRule>> GetRulesByBatchIdAsync(Guid batchId) =>
            Task.FromResult<IEnumerable<MigrationRule>>(_rules.Where(r => r.BatchId == batchId).OrderBy(r => r.SortOrder));

        public Task<MigrationRule?> GetRuleByIdAsync(Guid id) =>
            Task.FromResult(_rules.FirstOrDefault(r => r.Id == id));

        public Task<Guid> AddRuleAsync(MigrationRule rule)
        {
            rule.Id = Guid.NewGuid();
            _rules.Add(rule);
            return Task.FromResult(rule.Id);
        }

        public Task<bool> UpdateRuleAsync(MigrationRule rule)
        {
            var existingRule = _rules.FirstOrDefault(r => r.Id == rule.Id);
            if (existingRule == null) return Task.FromResult(false);

            existingRule.Description = rule.Description;
            existingRule.Condition = rule.Condition;
            existingRule.Action = rule.Action;
            existingRule.SortOrder = rule.SortOrder;
            existingRule.Configurations = rule.Configurations;

            return Task.FromResult(true);
        }

        public Task<bool> DeleteRuleAsync(Guid id)
        {
            var rule = _rules.FirstOrDefault(r => r.Id == id);
            if (rule == null) return Task.FromResult(false);

            _rules.Remove(rule);
            return Task.FromResult(true);
        }
    }
}