using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSJet.Core.Models;

namespace CMSJet.Core.Data.Repositories
{
    public class ContentItemRepository
    {
        private readonly List<ContentItem> _contentItems = new();

        public Task<IEnumerable<ContentItem>> GetAllContentItemsAsync() =>
            Task.FromResult<IEnumerable<ContentItem>>(_contentItems);

        public Task<IEnumerable<ContentItem>> GetContentItemsByBatchIdAsync(Guid batchId) =>
            Task.FromResult<IEnumerable<ContentItem>>(_contentItems.Where(c => c.BatchId == batchId));

        public Task<ContentItem?> GetContentItemByIdAsync(Guid id) =>
            Task.FromResult(_contentItems.FirstOrDefault(c => c.Id == id));

        public Task<Guid> AddContentItemAsync(ContentItem contentItem)
        {
            contentItem.Id = Guid.NewGuid();
            _contentItems.Add(contentItem);
            return Task.FromResult(contentItem.Id);
        }

        public Task<bool> UpdateContentItemAsync(ContentItem contentItem)
        {
            var existingContentItem = _contentItems.FirstOrDefault(c => c.Id == contentItem.Id);
            if (existingContentItem == null) return Task.FromResult(false);

            existingContentItem.Name = contentItem.Name;
            existingContentItem.Data = contentItem.Data;

            return Task.FromResult(true);
        }

        public Task<bool> DeleteContentItemAsync(Guid id)
        {
            var contentItem = _contentItems.FirstOrDefault(c => c.Id == id);
            if (contentItem == null) return Task.FromResult(false);

            _contentItems.Remove(contentItem);
            return Task.FromResult(true);
        }
    }
}