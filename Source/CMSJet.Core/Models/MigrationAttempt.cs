namespace CMSJet.Core.Models;

public class MigrationAttempt
{
    public Guid Id { get; set; }
    public Guid MigrationId { get; set; }
    public List<MigrationAttemptBatch> AttemptBatches { get; set; } = new();
    public MigrationAttemptStatus Status { get; set; } = MigrationAttemptStatus.Pending;
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
}