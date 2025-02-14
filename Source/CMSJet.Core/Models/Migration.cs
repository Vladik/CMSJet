using System;
using System.Collections.Generic;

namespace CMSJet.Core.Models;
public class Migration
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public Guid SourceConnectionId { get; set; }
    public Guid TargetConnectionId { get; set; }
        
    // Enum for Migration Status
    public MigrationStatus Status { get; set; } = MigrationStatus.Pending;

    // Auto-generated timestamps
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property for batches
    public List<MigrationBatch>? Batches { get; set; } 
}

// Enum for Status
public enum MigrationStatus
{
    Pending,        // Migration is created but not started
    InProgress,     // Migration is currently running
    Completed,      // Migration finished successfully
    Failed,         // Migration failed
    Canceled        // Migration was manually stopped
}