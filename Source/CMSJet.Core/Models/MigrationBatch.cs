using System.Text.Json;
namespace CMSJet.Core.Models;

public class MigrationBatch
{
    public Guid Id { get; set; } 
    public Guid MigrationId { get; set; }
    public string Name { get; set; } 
    public string SourceType { get; set; } 
    public string TargetType { get; set; } 
    public int Priority { get; set; } 
    public string Status { get; set; } 
    public JsonDocument FieldMappings { get; set; }
    public JsonDocument Rules { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}