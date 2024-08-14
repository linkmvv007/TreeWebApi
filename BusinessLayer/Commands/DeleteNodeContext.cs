using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Commands;

public record DeleteNodeContext
{
    [Required]
    public string treeName { get; init; }
    [Required]
    public int nodeId { get; init; }
};
