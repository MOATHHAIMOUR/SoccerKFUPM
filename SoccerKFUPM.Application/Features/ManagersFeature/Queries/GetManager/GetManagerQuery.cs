using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.ManagerDTOs;

namespace SoccerKFUPM.Application.Features.ManagersFeature.Queries.GetManager;

public record GetManagerQuery(int ManagerId) : IRequest<ApiResponse<ManagerDTO>>;