using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.CoachDTOs;

namespace SoccerKFUPM.Application.Features.CoachFeature.Queries.GetAllCoaches;

public record GetAllCoachesQuery(
    string? KFUPMId = null,
    string? FirstName = null,
    string? SecondName = null,
    string? ThirdName = null,
    string? LastName = null,
    DateTime? DateOfBirth = null,
    int? NationalityId = null,
    string? TeamName = null,
    bool? IsActive = null,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<ApiResponse<List<CoachViewDTO>>>;
