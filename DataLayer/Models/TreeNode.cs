using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DataLayer.Models;

[Index("Name", IsUnique = false)]
[Index("ParentNodeId", "Code", IsUnique = false, Name = "ParentNodeId_Code_Index")]
[Index("Name", "Code", IsUnique = true)] // uniqueness of tree node names
public record TreeNode
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }      //  auto generation id

    [NotNull]
    public Guid Code { get; set; } // tree node code guid

    [MaxLength(64)]
    [NotNull]
    public required string Name { get; set; } // name node or tree 
    public int? ParentNodeId { get; set; } // parent node

    public TreeNode? ParentNode { get; set; } // parent node

    public List<TreeNode> Childrens { get; } = new List<TreeNode>();

};