namespace BusinessLayer.Json;

/// <summary>
/// TreeAllNodes API metod: json response tree nodes record format. 
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Children"></param>
public record NodesJson(int Id, string Name, List<NodeInfo>? Children = null);
public record NodeInfo(int Id, int? ParentNodeId, string Name);
