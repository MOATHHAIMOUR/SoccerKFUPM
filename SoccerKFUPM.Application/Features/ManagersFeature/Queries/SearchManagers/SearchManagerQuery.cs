
using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.ManagerDTOs;


namespace SoccerKFUPM.Application.Features.ManagersFeature.Queries.SearchManagers
{


    public record SearchManagersQuery(
        string? KFUPMId,
        string? FirstName,
        string? SecondName,
        string? ThirdName,
        string? LastName,
        DateTime? DateOfBirth,
        int? NationalityId,
        string? TeamName,
        int PageNumber = 1,
        int PageSize = 10
    ) : IRequest<ApiResponse<List<ManagerSearchViewDTO>>>;

}
