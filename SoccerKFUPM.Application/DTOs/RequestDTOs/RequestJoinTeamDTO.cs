using SoccerKFUPM.Domain.Enums;

namespace SoccerKFUPM.Application.DTOs.RequestDTOs;

public class RequestJoinTeamDTO
{
    public required int PlayerId { get; set; }
    public required int TeamId { get; set; }
    public required PlayerPosition PreferredPosition { get; set; }
}