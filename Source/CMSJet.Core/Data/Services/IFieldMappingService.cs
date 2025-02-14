using CMSJet.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public interface IFieldMappingService
    {
        Task<IEnumerable<FieldMapping>> GetAllFieldMappingsAsync();
        Task<IEnumerable<FieldMapping>> GetFieldMappingsByBatchIdAsync(Guid batchId);
        Task<FieldMapping?> GetFieldMappingByIdAsync(Guid id);
        Task<Guid> AddFieldMappingAsync(FieldMapping fieldMapping);
        Task<bool> UpdateFieldMappingAsync(FieldMapping fieldMapping);
        Task<bool> DeleteFieldMappingAsync(Guid id);
    }
}