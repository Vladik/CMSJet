using System.Text.Json.Nodes;

namespace CMSJet.Core.Models;

public class ContentItem
{
    public Guid Id { get; set; }
    public Guid BatchId { get; set; }
    public string Name { get; set; } = null!;
    public JsonObject Data { get; set; } = new();
}