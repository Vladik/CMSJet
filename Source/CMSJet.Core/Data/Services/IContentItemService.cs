using CMSJet.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSJet.Core.Data.Services
{
    public interface IContentItemService
    {
        Task<IEnumerable<ContentItem>> GetAllContentItemsAsync();
        Task<IEnumerable<ContentItem>> GetContentItemsByBatchIdAsync(Guid batchId);
        Task<ContentItem?> GetContentItemByIdAsync(Guid id);
        Task<Guid> AddContentItemAsync(ContentItem contentItem);
        Task<bool> UpdateContentItemAsync(ContentItem contentItem);
        Task<bool> DeleteContentItemAsync(Guid id);
    }
}