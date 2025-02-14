using System.Text.Json.Nodes;

namespace CMSJet.Core.Models;

public class MigrationBatch
{
    public Guid Id { get; set; }
    public Guid MigrationId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } 
    public string SourceType { get; set; } = null!;
    public string TargetType { get; set; } = null!;
    public JsonObject Configurations { get; set; } = new();
    public List<FieldMapping> FieldMappings { get; set; } = new();
    public List<MigrationRule> Rules { get; set; } = new();
    public int Priority { get; set; }
    public bool Enabled { get; set; } = true;
    public bool StopOnFailure { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}