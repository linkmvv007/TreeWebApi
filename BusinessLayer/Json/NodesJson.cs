namespace BusinessLayer.Json;

public record NodesJson(int Id, string Name, List<NodeInfo>? Children = null);
public record NodeInfo(int Id, int? ParentNodeId, string Name);
