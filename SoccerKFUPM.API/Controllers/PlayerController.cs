using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerKFUPM.API.Controllers.Base;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.PlayerDTOs;
using SoccerKFUPM.Application.Features.PlayerFeature.Commands.AddPlayer;
using Swashbuckle.AspNetCore.Annotations;

namespace SoccerKFUPM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : AppController
    {
        public PlayerController(IMediator mediator) : base(mediator)
        {
        }



        [HttpPost("")]
        [SwaggerOperation(Summary = "Create a new player (AddPlayerDTO)", Description = "Send a valid AddPlayerDTO to register a new player in the system.")]
        public async Task<ActionResult<ApiResponse<bool>>> CreatePlayer([FromBody] AddPlayerDTO playerDTO)
        {
            var result = await _mediator.Send(new AddPlayerCommand(playerDTO));
            return StatusCode((int)result.StatusCode, result);
        }





        //    [HttpGet("fetch-all")]
        //    [SwaggerOperation(Summary = "Fetch all players", Description = "Retrieve a list of all players available in the system.")]
        //    public async Task<ActionResult<ApiResponse<List<PlayerDTO>>>> FetchAllPlayers([FromQuery] int? playerId,
        //[FromQuery] string? kfupmId,
        //[FromQuery] int pageNumber = 1,
        //[FromQuery] int pageSize = 10)
        //    {
        //        var result = await _mediator.Send(new FetchPlayersQuery(playerId, kfupmId, pageNumber, pageSize));
        //        return StatusCode((int)result.StatusCode, result);
        //    }




        //    [HttpGet("fetch/{id}")]
        //    [SwaggerOperation(Summary = "Fetch player by ID", Description = "Retrieve details of a player by their ID.")]
        //    public async Task<ActionResult<ApiResponse<PlayerDTO>>> FetchPlayerById(int id)
        //    {
        //        var result = await _mediator.Send(new FetchPlayerByIdQuery(id));
        //        return StatusCode((int)result.StatusCode, result);
        //    }


    }
}