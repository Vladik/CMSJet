using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Repositories
{
    public class FieldMappingRepository
    {
        private readonly List<FieldMapping> _fieldMappings = new();

        public Task<IEnumerable<FieldMapping>> GetAllFieldMappingsAsync() =>
            Task.FromResult<IEnumerable<FieldMapping>>(_fieldMappings);

        public Task<IEnumerable<FieldMapping>> GetFieldMappingsByBatchIdAsync(Guid batchId) =>
            Task.FromResult<IEnumerable<FieldMapping>>(_fieldMappings.Where(fm => fm.BatchId == batchId));

        public Task<FieldMapping?> GetFieldMappingByIdAsync(Guid id) =>
            Task.FromResult(_fieldMappings.FirstOrDefault(fm => fm.Id == id));

        public Task<Guid> AddFieldMappingAsync(FieldMapping fieldMapping)
        {
            fieldMapping.Id = Guid.NewGuid();
            _fieldMappings.Add(fieldMapping);
            return Task.FromResult(fieldMapping.Id);
        }

        public Task<bool> UpdateFieldMappingAsync(FieldMapping fieldMapping)
        {
            var existingFieldMapping = _fieldMappings.FirstOrDefault(fm => fm.Id == fieldMapping.Id);
            if (existingFieldMapping == null) return Task.FromResult(false);

            existingFieldMapping.SourceField = fieldMapping.SourceField;
            existingFieldMapping.TargetField = fieldMapping.TargetField;
            existingFieldMapping.SortOrder = fieldMapping.SortOrder;
            existingFieldMapping.Configurations = fieldMapping.Configurations;

            return Task.FromResult(true);
        }

        public Task<bool> DeleteFieldMappingAsync(Guid id)
        {
            var fieldMapping = _fieldMappings.FirstOrDefault(fm => fm.Id == id);
            if (fieldMapping == null) return Task.FromResult(false);

            _fieldMappings.Remove(fieldMapping);
            return Task.FromResult(true);
        }
    }
}