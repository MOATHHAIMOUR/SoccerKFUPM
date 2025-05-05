namespace SoccerKFUPM.Domain.Entities;

using SoccerKFUPM.Domain.Entities.Enums;

public class CardViolation
{
    public int ViolationId { get; set; }
    public int MatchRecordId { get; set; }
    public TimeSpan Time { get; set; }
    public int PlayerId { get; set; }
    public CardType CardType { get; set; } // e.g., Red, Yellow
    public string? Notes { get; set; }
}