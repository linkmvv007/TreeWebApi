using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.RequestContext.Journal;

/// <summary>
/// GetRange metod API parameters
/// </summary>
/// <param name="skip"></param>
/// <param name="take"></param>
/// <param name="filter"></param>
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

public static class FilterExtensions
{
    public static (DateTime? from, DateTime? to) GetValidFromToDate(this Filter filter)
    {
        return new(
            filter?.from > DateTime.MinValue ? filter.from : (DateTime?)null,
            filter?.to > DateTime.MinValue ? filter.to : (DateTime?)null
            );
    }
}
