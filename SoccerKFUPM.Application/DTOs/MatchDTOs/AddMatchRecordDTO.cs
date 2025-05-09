using SoccerKFUPM.Domain.Entities.Enums;

namespace SoccerKFUPM.Application.DTOs.MatchDTOs;

public class AddMatchRecordDTO
{
    public int MatchScheduleId { get; set; }

    public MatchStatus UpdatedMatchStatus { get; set; }
    // Team A Result
    public TeamMatchRecordDTO TeamARecord { get; set; } = null!;

    // Team B Result
    public TeamMatchRecordDTO TeamBRecord { get; set; } = null!;
}

public class TeamMatchRecordDTO
{
    public int TournamentTeamId { get; set; }
    public int GoalsFor { get; set; }
    public int GoalAgainst { get; set; }
    public double AcquisitionRate { get; set; }
    public bool IsWin { get; set; }
    public int? BestPlayer { get; set; }
    public List<ShotOnGoalDTO> ShotsOnGoal { get; set; } = new();
    public List<CardViolationDTO> CardsViolations { get; set; } = new();
    public List<MatchSubstitutionDTO> matchSubstitutionDTOs { get; set; } = new();
}

public class ShotOnGoalDTO
{
    public TimeSpan Time { get; set; }
    public int PlayerTeamId { get; set; }
    public int GoalkeeperTeamId { get; set; }
    public ShotType ShotType { get; set; }
    public bool IsGoal { get; set; }
}