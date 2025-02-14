using CMSJet.Core.Data.Repositories;
using CMSJet.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public class ContentItemService : IContentItemService
    {
        private readonly ContentItemRepository _contentItemRepository;
        private readonly ILogger<ContentItemService> _logger;

        public ContentItemService(ContentItemRepository contentItemRepository, ILogger<ContentItemService> logger)
        {
            _contentItemRepository = contentItemRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ContentItem>> GetAllContentItemsAsync()
        {
            _logger.LogInformation("Fetching all content items...");
            return await _contentItemRepository.GetAllContentItemsAsync();
        }

        public async Task<IEnumerable<ContentItem>> GetContentItemsByBatchIdAsync(Guid batchId)
        {
            _logger.LogInformation("Fetching content items for batch ID: {BatchId}", batchId);
            return await _contentItemRepository.GetContentItemsByBatchIdAsync(batchId);
        }

        public async Task<ContentItem?> GetContentItemByIdAsync(Guid id)
        {
            var contentItem = await _contentItemRepository.GetContentItemByIdAsync(id);
            if (contentItem == null)
            {
                _logger.LogWarning("Content item not found: {Id}", id);
            }
            return contentItem;
        }

        public async Task<Guid> AddContentItemAsync(ContentItem contentItem)
        {
            if (string.IsNullOrWhiteSpace(contentItem.Name))
                throw new ArgumentException("Content item name cannot be empty.", nameof(contentItem.Name));

            _logger.LogInformation("Adding new content item: {Name}", contentItem.Name);
            return await _contentItemRepository.AddContentItemAsync(contentItem);
        }

        public async Task<bool> UpdateContentItemAsync(ContentItem contentItem)
        {
            if (string.IsNullOrWhiteSpace(contentItem.Name))
                throw new ArgumentException("Content item name cannot be empty.", nameof(contentItem.Name));

            _logger.LogInformation("Updating content item: {Name}", contentItem.Name);
            return await _contentItemRepository.UpdateContentItemAsync(contentItem);
        }

        public async Task<bool> DeleteContentItemAsync(Guid id)
        {
            _logger.LogInformation("Deleting content item with ID: {Id}", id);
            return await _contentItemRepository.DeleteContentItemAsync(id);
        }
    }
}
