namespace SoccerKFUPM.Domain.Entities.Enums;

/// <summary>
/// Types of requests in the system
/// </summary>
public enum RequestType
{
    /// <summary>Request to join a team (send value 1)</summary>
    JoinTeam = 1,
    /// <summary>Request to leave a team (send value 2)</summary>
    LeaveTeam = 2,
}