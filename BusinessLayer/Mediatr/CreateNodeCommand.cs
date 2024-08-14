using BusinessLayer.Exceptions;
using BusinessLayer.RequestContext;
using DataLayer.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Mediatr;

public record CreateNodeCommand(CreateNodeContext Data) : IRequest<Unit>
{

}

/// <summary>
/// Handler for <see cref="CreateNodeCommand"/>
/// </summary>
public class CreateNodeCommandHandler : BaseTreeNodeHandler, IRequestHandler<CreateNodeCommand, Unit>
{
    public CreateNodeCommandHandler(TreeContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Unit> Handle(CreateNodeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var rootNode = await GetRootTreeNode(request.Data.treeName);

            var parentNode = _dbContext.TreeNodes.FirstOrDefault(x => x.Id == request.Data.parentNodeId && x.Code == rootNode.Code);
            if (parentNode is null)
            {
                throw new NotFoundException(NotFoundException.NotFoundError(request.Data.parentNodeId));
            }


            _dbContext.TreeNodes.Add(new TreeNode
            {
                Name = request.Data.nodeName.ToLower(),
                ParentNode = parentNode,
                Code = rootNode.Code,
            });

            await _dbContext.SaveChangesAsync();

            return Unit.Value;

        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is Npgsql.PostgresException sqlEx && sqlEx.SqlState == DublicateNameException.PostgresDublicateErrorCode)
            {
                throw new DublicateNameException(DublicateNameException.GetErrorMessage(request.Data.nodeName), ex);
            }

            throw;
        }

    }
}