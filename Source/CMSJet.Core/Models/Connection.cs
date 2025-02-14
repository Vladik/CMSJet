using System.Text.Json;
namespace CMSJet.Core.Models;

public class Connection
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ConnectorId { get; set; }
    public string Name { get; set; }
    public JsonDocument  Details { get; set; }
    public DateTime? LastSync  { get; set; }
}