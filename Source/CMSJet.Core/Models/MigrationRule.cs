namespace CMSJet.Core.Models;

public class MigrationRule
{
    public Guid Id { get; set; }
    public Guid BatchId { get; set; } // Reference to the batch it belongs to
    public string Field { get; set; } = string.Empty; // Field to apply rule on
    public string Condition { get; set; } = string.Empty; // Example: "contains", "equals", "regex"
    public string Value { get; set; } = string.Empty; // Value to check against
    public string Action { get; set; } = string.Empty; // Example: "exclude", "transform", "modify"
}