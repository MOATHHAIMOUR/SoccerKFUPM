using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.sharedDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.sharedFeature.Queries.FetchCountries
{
    public class FetchCountriesQueryHandler : IRequestHandler<FetchCountriesQuery, ApiResponse<List<CountryDTO>>>
    {
        private readonly ISharedServices _sharedServices;
        public FetchCountriesQueryHandler(ISharedServices sharedServices)
        {
            _sharedServices = sharedServices;
        }

        public async Task<ApiResponse<List<CountryDTO>>> Handle(FetchCountriesQuery request, CancellationToken cancellationToken)
        {
            var countries = await  _sharedServices.GetAllCountriesAsync();

            return ApiResponseHandler.Success(countries.Value??[]);
        }
    }
}