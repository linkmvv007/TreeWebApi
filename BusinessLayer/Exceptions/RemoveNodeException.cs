namespace BusinessLayer.Exceptions;

public class RemoveNodeException : SecureException
{
    private const string ChildrenFoundError = "You have to delete all children nodes first";
    public const string PostgresRemoveErrorCode = "23503";
    public RemoveNodeException(string message = ChildrenFoundError, Exception? innerException = null)
        : base(EventIds.DatabaseError, message, innerException)
    {
    }
}
