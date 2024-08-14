using BusinessLayer.Exceptions;
using BusinessLayer.RequestContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Mediatr;

public record RenameNodeCommand(RenameNodeContext context) : IRequest<Unit>;

/// <summary>
/// Handler for <see cref="RenameNodeCommand"/>
/// </summary>
public class RenameNodeCommandHandler : IRequestHandler<RenameNodeCommand, Unit>
{
    private readonly TreeContext _dbContext;

    public RenameNodeCommandHandler(TreeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(RenameNodeCommand request, CancellationToken cancellationToken)
    {

        var tree = await _dbContext.TreeNodes.FirstOrDefaultAsync(x => x.Name == request.context.treeName.ToLower() && x.ParentNode == null);
        if (tree is null)
        {
            throw new NotFoundException(NotFoundException.NotFoundError(request.context.treeName));
        }

        var node = await _dbContext.TreeNodes.FirstOrDefaultAsync(x => x.Id == request.context.nodeId && x.Code == tree.Code);

        if (node is null)
        {
            throw new NotFoundException(NotFoundException.NotFoundError(request.context.nodeId));
        }

        node.Name = request.context.newNodeName.ToLower();
        try
        {
            await _dbContext.SaveChangesAsync();

        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is Npgsql.PostgresException sqlEx && sqlEx.SqlState == DublicateNameException.PostgresDublicateErrorCode)
            {
                throw new DublicateNameException(DublicateNameException.GetErrorMessage(request.context.newNodeName), ex);
            }

            throw;
        }

        return Unit.Value;
    }
}
