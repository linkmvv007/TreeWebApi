using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.RequestContext.Journal;

public record GetRangeContext
(
     int skip,
     int take,
     Filter? filter
);

public record Pagination
{
    [Required]
    public int skip { get; init; }
    [Required]
    public int take { get; init; }
}
public record Filter
{
    public DateTime? from { get; init; }//: "2024-08-11T10:08:57.7379013Z",
    public DateTime? to { get; init; }//: "2024-08-11T10:08:57.7379077Z",
    public string? search { get; init; }
}