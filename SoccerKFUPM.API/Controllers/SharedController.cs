using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerKFUPM.API.Controllers.Base;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.sharedDTOs;
using SoccerKFUPM.Application.DTOs.SharedDTOs;
using SoccerKFUPM.Application.Features.sharedFeature.Queries.FetchCountries;
using SoccerKFUPM.Application.Features.sharedFeature.Queries.FetchDepartments;
using Swashbuckle.AspNetCore.Annotations;

namespace SoccerKFUPM.API.Controllers
{
    [ApiController]
    [Route("api/shared")]
    public class SharedController : AppController
    {
        public SharedController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("countries")]
        [SwaggerOperation(Summary = "Fetch all countries", Description = "Retrieve a list of all countries available in the system.")]
        public async Task<ActionResult<ApiResponse<List<CountryDTO>>>> FetchCountries()
        {
            var result = await _mediator.Send(new FetchCountriesQuery());
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("departments")]
        [SwaggerOperation(Summary = "Fetch all departments", Description = "Retrieve a list of all departments available in the system.")]
        public async Task<ActionResult<ApiResponse<List<DepartmentDTO>>>> FetchDepartments()
        {
            var result = await _mediator.Send(new FetchDepartmentsQuery());
            return StatusCode((int)result.StatusCode, result);
        }
    }
}