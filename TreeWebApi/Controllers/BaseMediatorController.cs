using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TreeWebApi.Controllers;

public class BaseMediatorController : ControllerBase
{
    protected readonly IMediator _mediator;
    protected BaseMediatorController(IMediator mediator)
    {
        _mediator = mediator;
    }

}
