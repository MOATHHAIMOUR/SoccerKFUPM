using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerKFUPM.API.Controllers.Base;
using SoccerKFUPM.Application.Features.FieldFeature.Queries.GetAllFields;
using Swashbuckle.AspNetCore.Annotations;

namespace SoccerKFUPM.API.Controllers;

public class FieldController : AppController
{
    public FieldController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("get-all")]
    [SwaggerOperation(
        Summary = "Get all fields",
        Description = "Retrieves all fields with pagination support",
        OperationId = "Fields.GetAll"
    )]
    public async Task<IActionResult> GetAllFields()
    {
        var query = new GetAllFieldsQuery();
        var result = await _mediator.Send(query);
        return StatusCode((int)result.StatusCode, result);
    }
}