using System.Text.Json.Nodes;

namespace CMSJet.Core.Models;

public class MigrationAttemptBatch
{
    public Guid Id { get; set; }
    public Guid AttemptId { get; set; }
    public Guid BatchId { get; set; }
    public string BatchName { get; set; } = null!;
    public JsonObject Configurations { get; set; } = new();
    public MigrationBatchStatus Status { get; set; } = MigrationBatchStatus.Pending;
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
}