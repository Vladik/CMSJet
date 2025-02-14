using System.Text.Json.Nodes;

namespace CMSJet.Core.Models;

public class FieldMapping
{
    public Guid Id { get; set; }
    public Guid BatchId { get; set; }
    public string SourceField { get; set; } = null!;
    public string TargetField { get; set; }  = null!;
    public JsonObject Configurations { get; set; } = new();
    public int SortOrder { get; set; }
}