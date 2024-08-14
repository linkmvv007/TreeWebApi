namespace BusinessLayer.Exceptions;
public class NotFoundException : SecureException
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public NotFoundException(string message, Exception? innerException = null)
        : base(EventIds.DatabaseError, message, innerException)
    {
    }

    internal static string NotFoundError(int id) => $"Object with id='{id}' not found";
    internal static string NotFoundError(string name) => $"Object with name='{name}' not found";
}
