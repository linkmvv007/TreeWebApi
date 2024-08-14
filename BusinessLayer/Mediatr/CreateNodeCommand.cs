using BusinessLayer.Commands;
using BusinessLayer.Exceptions;
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
public class CreateNodeCommandHandler : IRequestHandler<CreateNodeCommand, Unit>
{
    private readonly TreeContext _dbContext;

    public CreateNodeCommandHandler(TreeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(CreateNodeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var treeNode = _dbContext.TreeNodes.FirstOrDefault(x => x.Name == request.Data.treeName.ToLower());
            if (treeNode is null)
            {
                throw new NotFoundException(NotFoundException.NotFoundError(request.Data.treeName));
            }

            var parentNode = _dbContext.TreeNodes.FirstOrDefault(x => x.Id == request.Data.parentNodeId && x.Code == treeNode.Code);
            if (parentNode is null)
            {
                throw new NotFoundException(NotFoundException.NotFoundError(request.Data.parentNodeId));
            }


            _dbContext.TreeNodes.Add(new TreeNode
            {
                Name = request.Data.nodeName.ToLower(),
                ParentNode = parentNode,
                Code = treeNode.Code,
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