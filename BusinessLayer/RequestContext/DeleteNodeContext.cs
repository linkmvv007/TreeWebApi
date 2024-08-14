using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.RequestContext;

public record DeleteNodeContext
{
    [Required]
    public string treeName { get; init; }
    [Required]
    public int nodeId { get; init; }
};
