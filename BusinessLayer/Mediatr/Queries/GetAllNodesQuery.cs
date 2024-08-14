using BusinessLayer.Exceptions;
using BusinessLayer.Json;
using DataLayer.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace BusinessLayer.Mediatr.Queries;

public record GetAllNodesQuery(string TreeName) : IRequest<NodesJson>
{ }


/// <summary>
/// Handler for <see cref="GetAllNodesQueryHandler"/>
/// </summary>
public class GetAllNodesQueryHandler : IRequestHandler<GetAllNodesQuery, NodesJson>
{
    private readonly TreeContext _dbContext;


    public GetAllNodesQueryHandler(TreeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<NodesJson> Handle(GetAllNodesQuery request, CancellationToken cancellationToken)
    {
        int rootNodeId;

        var treeName = request.TreeName.ToLower();
        var tree = await _dbContext.TreeNodes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == treeName && x.ParentNode == null);

        if (tree is null)
        {
            var rootNode = new TreeNode
            {
                Name = treeName,
                Code = Guid.NewGuid()
            };
            _dbContext.TreeNodes.Add(rootNode);

            try
            {
                await _dbContext.SaveChangesAsync();
            }

            catch (DbUpdateException ex)
            {
                if (ex.InnerException is Npgsql.PostgresException sqlEx && sqlEx.SqlState == DublicateNameException.PostgresDublicateErrorCode)
                {
                    throw new DublicateNameException(DublicateNameException.GetErrorMessage(request.TreeName), ex);
                }

                throw;
            }

            rootNodeId = rootNode.Id;

            return new NodesJson(rootNodeId, rootNode.Name);
        }


        rootNodeId = tree.Id;

        var nodeWithChildren = await GetTreeAsync(rootNodeId);

        var children = nodeWithChildren.Select(x => new NodeInfo(x.Id, x.ParentNodeId, x.Name)).ToList();

        return new NodesJson(tree.Id, tree.Name, children);
    }

    private async Task<List<TreeNode>> GetTreeAsync(int rootId)
    {
        var rootNode = await _dbContext.TreeNodes
             .Include(n => n.Childrens)
             .FirstOrDefaultAsync(n => n.Id == rootId);

        var allNodes = new List<TreeNode>();

        if (rootNode != null)
        {
            allNodes.Add(rootNode);
            await LoadChildrenAsync(rootNode, allNodes);
        }

        return allNodes;
    }

    private async Task LoadChildrenAsync(TreeNode node, List<TreeNode> allNodes)
    {
        foreach (var child in node.Childrens)
        {
            allNodes.Add(child);
            await _dbContext.Entry(child).Collection(c => c.Childrens).LoadAsync();
            await LoadChildrenAsync(child, allNodes);
        }
    }
}
