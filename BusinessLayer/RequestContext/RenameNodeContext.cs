using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.RequestContext;

/// <summary>
/// Rename node metod API parameters
/// </summary>
public record RenameNodeContext
{
    [Required]
    public string treeName { get; init; }
    [Required]
    public int nodeId { get; init; }
    [Required]
    public string newNodeName { get; init; }
};
