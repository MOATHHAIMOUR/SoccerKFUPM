namespace SoccerKFUPM.Domain.Entities.Enums;

/// <summary>
/// Phases of a tournament
/// </summary>
public enum TournamentPhase
{
    /// <summary>Finals phase (send value 1)</summary>
    Finals=1,
    /// <summary>Semi-finals phase (send value 2)</summary>
    SemiFinals=2,
    /// <summary>Quarter-finals phase (send value 3)</summary>
    QuarterFinals=3,
    /// <summary>Group stage phase (send value 4)</summary>
    GroupStage=4
}