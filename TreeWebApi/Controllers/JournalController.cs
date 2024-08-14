using BusinessLayer.Mediatr.Queries;
using BusinessLayer.RequestContext.Journal;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace TreeWebApi.Controllers;

[ApiController]
[Route("api.user.journal")]
[SwaggerTag("Represents journal API")]
public class JournalController : BaseMediatorController
{

    public JournalController(IMediator mediator)
        : base(mediator)
    {

    }

    [HttpPost("getSingle")]
    [SwaggerOperation(Description = "Returns the information about an particular event by ID.")]
    public async Task<IActionResult> GetSingle([Required] int id, CancellationToken cancellationToken)
    {
        var log = await _mediator.Send(
            new GetSingleQuery(id), cancellationToken
            );

        return Ok(log);
    }

    [HttpPost("getRange")]
    [SwaggerOperation(Description = "Provides the pagination API. Skip means the number of items should be skipped by server. Take means the maximum number items should be returned by server. All fields of the filter are optional.")]
    public async Task<IActionResult> GetRange([FromQuery] Pagination pagination, [FromBody] Filter? filter, CancellationToken cancellationToken)
    {
        var logs = await _mediator.Send(
            new GetRangeQuery(
                new GetRangeContext(pagination.skip, pagination.take, filter)
                )
            , cancellationToken
            );

        return Ok(logs);
    }
}
