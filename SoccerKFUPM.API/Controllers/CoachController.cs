using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerKFUPM.API.Controllers.Base;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.CoachDTOs;
using SoccerKFUPM.Application.DTOs.TeamDTOs;
using SoccerKFUPM.Application.Features.CoachFeature.Commands.AddCoach;
using SoccerKFUPM.Application.Features.CoachFeature.Commands.AssignCoachIntoTeam;
using SoccerKFUPM.Application.Features.CoachFeature.Queries.GetAllCoaches;
using Swashbuckle.AspNetCore.Annotations;

namespace SoccerKFUPM.API.Controllers;

[ApiController]
[Route("api/coaches")]
public class CoachController : AppController
{
    public CoachController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Add a new coach (AddCoachDTO)",
        Description = "Creates a new coach with the provided details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<bool>>> AddCoach([FromBody] AddCoachDTO dto)
    {
        var result = await _mediator.Send(new AddCoachCommand(dto));
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all coaches",
        Description = "Retrieves a paginated list of all coaches")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet()]
    public async Task<ActionResult<ApiResponse<List<CoachViewDTO>>>> GetAllCoaches(
    [FromQuery] string? kfupmId,
    [FromQuery] string? firstName,
    [FromQuery] string? secondName,
    [FromQuery] string? thirdName,
    [FromQuery] string? lastName,
    [FromQuery] DateTime? dateOfBirth,
    [FromQuery] int? nationalityId,
    [FromQuery] string? teamName,
    [FromQuery] bool? isActive,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
    {
        var query = new GetAllCoachesQuery(
            KFUPMId: kfupmId,
            FirstName: firstName,
            SecondName: secondName,
            ThirdName: thirdName,
            LastName: lastName,
            DateOfBirth: dateOfBirth,
            NationalityId: nationalityId,
            TeamName: teamName,
            IsActive: isActive,
            PageNumber: pageNumber,
            PageSize: pageSize
        );

        var result = await _mediator.Send(query);
        return StatusCode((int)result.StatusCode, result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost("assign-coach")]
    [SwaggerOperation(Summary = "Assign coach to a team (AssignCoachIntoTeamDTO)", Description = "Assigns a coach to the specified team")]
    public async Task<ActionResult<ApiResponse<bool>>> AssignCoachToTeam([FromBody] AssignCoachIntoTeamDTO dto)
    {
        var result = await _mediator.Send(new AssignCoachIntoTeamCommand(dto));
        return StatusCode((int)result.StatusCode, result);
    }
}