using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.RequestContext;

/// <summary>
/// Remove node metod API parameters
/// </summary>
public record DeleteNodeContext
{
    [Required]
    public string treeName { get; init; }
    [Required]
    public int nodeId { get; init; }
};
