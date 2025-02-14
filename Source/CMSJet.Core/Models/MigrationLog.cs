namespace CMSJet.Core.Models;

public class MigrationLog
{
    public Guid Id { get; set; }
    public Guid MigrationAttemptId { get; set; }
    public Guid? AttemptBatchId { get; set; } 
    public string? Message { get; set; }
    public MigrationLogSeverity Severity { get; set; } = MigrationLogSeverity.Info;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}