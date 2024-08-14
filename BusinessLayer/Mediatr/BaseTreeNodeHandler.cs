using BusinessLayer.Exceptions;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Mediatr;

public abstract class BaseTreeNodeHandler
{

    protected readonly TreeContext _dbContext;

    protected BaseTreeNodeHandler(TreeContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected async Task<TreeNode> GetRootTreeNode(string treeName)
    {
        var tree = await _dbContext.TreeNodes.FirstOrDefaultAsync(x => x.Name == treeName.ToLower() && x.ParentNode == null);
        if (tree is null)
        {
            throw new NotFoundException(NotFoundException.NotFoundError(treeName));
        }

        return tree;
    }
}
