namespace BusinessLayer.Json;

/// <summary>
/// GetSingleQuery API metod: json response journal record format. 
/// </summary>
/// <param name="text"></param>
/// <param name="id"></param>
/// <param name="eventid"></param>
/// <param name="created"></param>
public record JournalJson(string? text, int id, int eventid, DateTime created);

/// <summary>
/// GetRange API metod: json response journal records format.
/// </summary>
/// <param name="skip"></param>
/// <param name="count"></param>
/// <param name="items"></param>
public record JournalsJson(int skip, int count, List<JournalInfo> items);
public record JournalInfo(int id, int eventid, DateTime created);
