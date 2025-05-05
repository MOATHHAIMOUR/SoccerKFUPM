using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.AuthDTOs;

namespace SoccerKFUPM.Application.Features.AuthFeature.Queries.GetAllRoles;

public record GetAllRolesQuery : IRequest<ApiResponse<List<RoleDTO>>>;