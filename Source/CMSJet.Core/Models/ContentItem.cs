namespace CMSJet.Core.Models;

public class ContentItem
{
    public Guid Id { get; set; }
    public Guid BatchId { get; set; } // Reference to the batch it belongs to
    public string Name { get; set; } = string.Empty;
    public Dictionary<string, object> Fields { get; set; } = new(); // Holds field name & value
    public bool IsSelected { get; set; } = true; // User can manually deselect items
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}