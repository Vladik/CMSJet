using CMSJet.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public interface IMigrationRuleService
    {
        Task<IEnumerable<MigrationRule>> GetAllRulesAsync();
        Task<IEnumerable<MigrationRule>> GetRulesByBatchIdAsync(Guid batchId);
        Task<MigrationRule?> GetRuleByIdAsync(Guid id);
        Task<Guid> AddRuleAsync(MigrationRule rule);
        Task<bool> UpdateRuleAsync(MigrationRule rule);
        Task<bool> DeleteRuleAsync(Guid id);
    }
}