namespace SoccerKFUPM.Domain.Entities;

using SoccerKFUPM.Domain.Enums;

public class ShotOnGoal
{
    public int ShotOnGoalId { get; set; }
    public int MatchRecordId { get; set; }
    public TimeSpan Time { get; set; }
    public int PlayerId { get; set; }
    public int GoalkeeperId { get; set; }
    public ShotType ShotType { get; set; } 
    public bool IsGoal { get; set; }
}