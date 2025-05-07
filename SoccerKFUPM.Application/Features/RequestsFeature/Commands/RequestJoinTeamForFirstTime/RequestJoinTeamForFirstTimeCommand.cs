using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.RequestDTOs;

namespace SoccerKFUPM.Application.Features.RequestsFeature.Commands.RequestJoinTeamForFirstTime;

public record RequestJoinTeamForFirstTimeCommand(RequestJoinTeamDTO RequestJoinTeamDTO) : IRequest<ApiResponse<bool>>;