namespace CMSJet.Core.Models;

public class FieldMapping
{
    public Guid Id { get; set; }
    public Guid BatchId { get; set; } // Reference to the batch it belongs to
    public string SourceField { get; set; } = string.Empty; // Field in source CMS
    public List<string> TargetFields { get; set; } = new(); // Can map to multiple target fields
    public List<string> Transformations { get; set; } = new(); // Optional transformations (replace, remove, etc.)
    public bool IsRequired { get; set; } = false; // Whether this mapping is required
}