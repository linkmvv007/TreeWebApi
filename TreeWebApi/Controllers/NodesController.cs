using BusinessLayer.RequestContext;
using BusinessLayer.Mediatr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace TreeWebApi.Controllers;


[ApiController]
[Route("api.user.tree.node")]
[SwaggerTag("Represents tree node API")]
public class NodesController : BaseMediatorController
{
    public NodesController(IMediator mediator)
        : base(mediator)
    {

    }


    /// <summary>
    /// Create node
    /// </summary>
    /// <param name="createNodeContext"></param>
    /// <returns></returns>
    [HttpPost("create")]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Description = "Create a new node in your tree. You must to specify a parent node ID that belongs to your tree. A new node name must be unique across all siblings.")]
    public async Task<IActionResult> CreateNode([FromQuery] CreateNodeContext context, CancellationToken cancellationToken)
    {
        //return CreatedAtAction(
        //    nameof(Create),
        //    new
        //    {
        //        id = await _mediator.Send(
        //            new CreateNodeCommand(new CreateNodeContext(context)), cancellationToken)
        //    },
        //    null
        //    );
        return Ok(await _mediator.Send(new CreateNodeCommand(context), cancellationToken));
    }

    [HttpPost("delete")]
    //[HttpDelete]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerOperation(Description = "Delete an existing node in your tree. You must specify a node ID that belongs your tree.")]

    public async Task<IActionResult> DeleteNode(
       [FromQuery] DeleteNodeContext context,
       CancellationToken cancellationToken)
    {
        //return NoContent(await _mediator.Send(

        return Ok(await _mediator.Send(
                    new DeleteNodeCommand(context), cancellationToken));
    }

    [HttpPost("rename")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Description = "Rename an existing node in your tree.You must specify a node ID that belongs your tree.A new name of the node must be unique across all siblings.")]

    public async Task<IActionResult> RenameNode([FromQuery] RenameNodeContext context, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(
                    new RenameNodeCommand(context), cancellationToken));
    }
}