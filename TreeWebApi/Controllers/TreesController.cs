using BusinessLayer.Mediatr.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace TreeWebApi.Controllers;

[ApiController]
[Route("api.user.tree")]
[SwaggerTag("Represents entire tree API")]
public class TreesController : BaseMediatorController
{
    public TreesController(IMediator mediator)
        : base(mediator)
    {

    }


    /// <summary>
    /// Get tree nodes
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>

    [HttpPost("get")]
    [SwaggerOperation(Description = "Returns your entire tree. If your tree doesn't exist it will be created automatically.")]
    public async Task<IActionResult> TreeAllNodes([Required] string treeName, CancellationToken cancellationToken)
    {
        var nodes = await _mediator.Send(
            new GetAllNodesQuery(treeName), cancellationToken
            );

        return Ok(nodes);
    }
}
