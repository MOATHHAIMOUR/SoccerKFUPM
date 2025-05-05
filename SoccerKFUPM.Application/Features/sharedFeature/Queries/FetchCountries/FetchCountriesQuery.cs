using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.sharedDTOs;

namespace SoccerKFUPM.Application.Features.sharedFeature.Queries.FetchCountries
{
    public class FetchCountriesQuery : IRequest<ApiResponse<List<CountryDTO>>>
    {

    }
}