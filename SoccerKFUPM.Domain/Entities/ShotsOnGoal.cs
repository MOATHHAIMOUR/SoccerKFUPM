using SoccerKFUPM.Domain.Entities.Enums;

namespace SoccerKFUPM.Domain.Entities;

public class ShotOnGoal
{
    public int ShotOnGoalId { get; set; }
    public int MatchRecoredId { get; set; }
    public TimeSpan Time { get; set; }
    public int PlayerTeamId { get; set; }
    public int GoalkeeperTeamId { get; set; }
    public ShotType ShotType { get; set; }
    public bool IsGoal { get; set; }
}