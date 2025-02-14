using System.Text.Json.Nodes;

namespace CMSJet.Core.Models;

public class Connector
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public JsonObject Details { get; set; } = new();
    public JsonObject Configurations { get; set; } = new();
}
