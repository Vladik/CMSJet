namespace CMSJet.Core.Models;

public class Migration
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public Guid SourceConnectionId { get; set; }
    public Guid TargetConnectionId { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}