using BusinessLayer.Commands;
using BusinessLayer.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Mediatr;

public record DeleteNodeCommand(DeleteNodeContext context) : IRequest<Unit>;


/// <summary>
/// Handler for <see cref="DeleteNodeCommand"/>
/// </summary>
public class DeleteNodeCommandHandler : IRequestHandler<DeleteNodeCommand, Unit>
{
    private readonly TreeContext _dbContext;

    public DeleteNodeCommandHandler(TreeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteNodeCommand request, CancellationToken cancellationToken)
    {
        var tree = await _dbContext.TreeNodes.FirstOrDefaultAsync(x => x.Name == request.context.treeName.ToLower() && x.ParentNode == null);
        if (tree is null)
        {
            throw new NotFoundException(NotFoundException.NotFoundError(request.context.treeName));
        }

        try
        {
            await _dbContext.TreeNodes.
                Where(x => x.Id == request.context.nodeId && x.Code == tree.Code).ExecuteDeleteAsync();

        }
        catch (System.Data.Common.DbException ex)
        {
            if (ex.SqlState == RemoveNodeException.PostgresRemoveErrorCode)
            {
                throw new RemoveNodeException();
            }

            throw;
        }

        return Unit.Value;
    }
}