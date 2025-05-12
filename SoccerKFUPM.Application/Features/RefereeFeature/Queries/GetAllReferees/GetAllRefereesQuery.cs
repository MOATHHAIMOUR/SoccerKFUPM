using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.RefereeDTOs;

namespace SoccerKFUPM.Application.Features.RefereeFeature.Queries.GetAllReferees;

public record GetAllRefereesQuery : IRequest<ApiResponse<List<RefereeViewDTO>>>;
