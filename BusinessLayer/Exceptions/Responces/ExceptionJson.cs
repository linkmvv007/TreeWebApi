namespace BusinessLayer.Exceptions.Responces;

public enum ExceptionTypes { Exception, Secure }

/// <summary>
/// Response in case of errors
/// </summary>
/// <param name="Type">exception type</param>
/// <param name="Id">record id</param>
/// <param name="Data"> error message </param>
public record ExceptionJson(ExceptionTypes Type, int Id, MessageBody Data);

public record MessageBody(string message);