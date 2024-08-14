using BusinessLayer.Exceptions;
using BusinessLayer.RequestContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Mediatr;

public record DeleteNodeCommand(DeleteNodeContext context) : IRequest<Unit>;


/// <summary>
/// Handler for <see cref="DeleteNodeCommand"/>
/// </summary>
public class DeleteNodeCommandHandler : BaseTreeNodeHandler, IRequestHandler<DeleteNodeCommand, Unit>
{

    public DeleteNodeCommandHandler(TreeContext dbContext)
        : base(dbContext)
    {

    }

    public async Task<Unit> Handle(DeleteNodeCommand request, CancellationToken cancellationToken)
    {
        var rootNode = await GetRootTreeNode(request.context.treeName);

        try
        {
            await _dbContext.TreeNodes.
                Where(x => x.Id == request.context.nodeId && x.Code == rootNode.Code).ExecuteDeleteAsync();

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