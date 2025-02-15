using System.Text.Json.Nodes;

namespace CMSJet.Core.Models;

public class Connection
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ConnectorId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public JsonObject Configurations { get; set; } = new();
    public DateTime? LastSync { get; set; }
}