using System.Text.Json.Nodes;

namespace CMSJet.Core.Models;
public class Migration
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid SourceConnectionId { get; set; }
    public Guid TargetConnectionId { get; set; }
    public JsonObject Configurations { get; set; } = new();
    public MigrationStatus Status { get; set; } = MigrationStatus.Pending;
    public List<MigrationBatch> Batches { get; set; } = new();
    public List<MigrationAttempt> Attempts { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}