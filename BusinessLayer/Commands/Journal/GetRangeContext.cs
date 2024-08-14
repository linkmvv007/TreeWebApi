namespace BusinessLayer.Commands.Journal;

//public record GetRangeContext
//{
//    public int skip { get; init; }
//    public int take { get; init; }
//    public Filter filter { get; init; }
//}
public record GetRangeContext
(
     int skip,
     int take,
     Filter? filter);

public record Filter
{
    public DateTime? from { get; init; }//: "2024-08-11T10:08:57.7379013Z",
    public DateTime? to { get; init; }//: "2024-08-11T10:08:57.7379077Z",
    public string? search { get; init; }
}