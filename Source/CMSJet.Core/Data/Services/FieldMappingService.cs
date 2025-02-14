using CMSJet.Core.Data.Repositories;
using CMSJet.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public class FieldMappingService : IFieldMappingService
    {
        private readonly FieldMappingRepository _fieldMappingRepository;
        private readonly ILogger<FieldMappingService> _logger;

        public FieldMappingService(FieldMappingRepository fieldMappingRepository, ILogger<FieldMappingService> logger)
        {
            _fieldMappingRepository = fieldMappingRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<FieldMapping>> GetAllFieldMappingsAsync()
        {
            _logger.LogInformation("Fetching all field mappings...");
            return await _fieldMappingRepository.GetAllFieldMappingsAsync();
        }

        public async Task<IEnumerable<FieldMapping>> GetFieldMappingsByBatchIdAsync(Guid batchId)
        {
            _logger.LogInformation("Fetching field mappings for batch ID: {BatchId}", batchId);
            return await _fieldMappingRepository.GetFieldMappingsByBatchIdAsync(batchId);
        }

        public async Task<FieldMapping?> GetFieldMappingByIdAsync(Guid id)
        {
            var fieldMapping = await _fieldMappingRepository.GetFieldMappingByIdAsync(id);
            if (fieldMapping == null)
            {
                _logger.LogWarning("Field mapping not found: {Id}", id);
            }
            return fieldMapping;
        }

        public async Task<Guid> AddFieldMappingAsync(FieldMapping fieldMapping)
        {
            if (string.IsNullOrWhiteSpace(fieldMapping.SourceField) || string.IsNullOrWhiteSpace(fieldMapping.TargetField))
                throw new ArgumentException("SourceField and TargetField cannot be empty.", nameof(fieldMapping));

            _logger.LogInformation("Adding new field mapping: {SourceField} -> {TargetField}", fieldMapping.SourceField, fieldMapping.TargetField);
            return await _fieldMappingRepository.AddFieldMappingAsync(fieldMapping);
        }

        public async Task<bool> UpdateFieldMappingAsync(FieldMapping fieldMapping)
        {
            if (string.IsNullOrWhiteSpace(fieldMapping.SourceField) || string.IsNullOrWhiteSpace(fieldMapping.TargetField))
                throw new ArgumentException("SourceField and TargetField cannot be empty.", nameof(fieldMapping));

            _logger.LogInformation("Updating field mapping: {SourceField} -> {TargetField}", fieldMapping.SourceField, fieldMapping.TargetField);
            return await _fieldMappingRepository.UpdateFieldMappingAsync(fieldMapping);
        }

        public async Task<bool> DeleteFieldMappingAsync(Guid id)
        {
            _logger.LogInformation("Deleting field mapping with ID: {Id}", id);
            return await _fieldMappingRepository.DeleteFieldMappingAsync(id);
        }
    }
}
