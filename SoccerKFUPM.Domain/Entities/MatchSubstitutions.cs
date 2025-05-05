using SoccerKFUPM.Domain.Entities.Enums;

namespace SoccerKFUPM.Domain.Entities;

public class MatchSubstitution
{
    public int MatchSubstitutionId { get; set; }
    public int MatchRecordId { get; set; }
    public int PlayerInTeamId  { get; set; }
    public int PlayerTeamOutId { get; set; }
    public int TimeMinute { get; set; }
    public SubstitutionReason Reason { get; set; }
}