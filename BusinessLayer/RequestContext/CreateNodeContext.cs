using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.RequestContext;

/// <summary>
///  Create node metod API parameters
/// </summary>
public record CreateNodeContext
{
    [Required]
    public string treeName { get; init; }
    [Required]
    public int parentNodeId { get; init; }
    [Required]
    public string nodeName { get; init; }
}