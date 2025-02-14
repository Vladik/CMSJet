using System.Text.Json.Nodes;

namespace CMSJet.Core.Models;

public class MigrationRule
{
    public Guid Id { get; set; }
    public Guid BatchId { get; set; }
    public string? Description { get; set; } 
    public string Condition { get; set; } = null!; 
    public string Action { get; set; } = null!;
    public JsonObject Configurations { get; set; } = new();
    public int SortOrder { get; set; } 
}