namespace SoccerKFUPM.Domain.Entities;

public class MatchRecord
{
    public int MatchRecordId { get; set; }
    public int MatchScheduleId { get; set; }
    public int TournamentTeamId { get; set; }
    public int GoalsFor { get; set; }
    public int GoalAgainst { get; set; }
    public double AcquisitionRate { get; set; }
    public bool IsWin { get; set; }
    public int? BestPlayer { get; set; } // Nullable
}