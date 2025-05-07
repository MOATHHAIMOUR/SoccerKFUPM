using SoccerKFUPM.Domain.Entities.Enums;
using SoccerKFUPM.Domain.Enums;

namespace SoccerKFUPM.Domain.Entities;

public class Request
{
    public int RequestId { get; set; }
    public int PlayerId { get; set; }
    public int TeamId { get; set; }
    public RequestType RequestType { get; set; }
    public RequestStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? ProcessedByUserId { get; set; }
    public PlayerPosition PreferredPosition { get; set; }
    
    // Navigation properties
    public Player Player { get; set; } = null!;
    public Team Team { get; set; } = null!;
}