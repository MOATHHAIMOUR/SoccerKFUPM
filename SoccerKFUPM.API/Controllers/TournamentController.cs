using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerKFUPM.API.Controllers.Base;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TournamentDTOs;
using SoccerKFUPM.Application.Features.TournamentFeature.Commands.AddTournament;
using SoccerKFUPM.Application.Features.TournamentFeature.Commands.DeleteTournament;
using SoccerKFUPM.Application.Features.TournamentFeature.Commands.UpdateTournament;
using SoccerKFUPM.Application.Features.TournamentFeature.Queries.FetchTournamentById;
using SoccerKFUPM.Application.Features.TournamentFeature.Queries.FetchTournaments;
using Swashbuckle.AspNetCore.Annotations;

namespace SoccerKFUPM.API.Controllers
{
    [ApiController]
    [Route("api/Tournament")]
    public class TournamentController : AppController
    {
        public TournamentController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a tournament", Description = "Creates a new tournament.")]
        public async Task<ActionResult<ApiResponse<TournamentDTO>>> Create([FromBody]  AddTournamentDTO addTournamentDTO)
        {
            var result = await _mediator.Send(new AddTournamentCommand(addTournamentDTO));
            return StatusCode((int)result.StatusCode, result);
        }


        [HttpGet]
        [SwaggerOperation(Summary = "Get all tournaments", Description = "Returns a list of all tournaments.")]
        public async Task<ActionResult<ApiResponse<List<TournamentDTO>>>> GetAll(
            [FromQuery] string? tournamentNumber,
            [FromQuery] string? tournamentName,
            [FromQuery] string? startDate,
            [FromQuery] string? endDate,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
        )
        {
            var result = await _mediator.Send(new FetchTournamentsQuery(tournamentNumber, tournamentName, startDate, endDate, pageNumber, pageSize));
            
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get tournament by it's Id", Description = "Returns a single tournament by its ID.")]
        public async Task<ActionResult<ApiResponse<TournamentDTO>>> GetById(int id)
        {
            var result = await _mediator.Send(new FetchTournamentByIdQuery(id));
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a tournament", Description = "Updates an existing tournament.")]
        public async Task<ActionResult<ApiResponse<TournamentDTO>>> Update(int id, [FromBody] UpdateTournamentDTO  updateTournamentDTO)
        {
            var result = await _mediator.Send(new UpdateTournamentCommand(id,updateTournamentDTO));
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a tournament", Description = "Deletes a tournament by ID.")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteTournamentCommand(id));
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
