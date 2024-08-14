namespace BusinessLayer.Json;

public record JournalJson(string? text, int id, int eventid, DateTime created);

public record JournalsJson(int skip, int count, List<JournalInfo> items);
public record JournalInfo(int id, int eventid, DateTime created);
