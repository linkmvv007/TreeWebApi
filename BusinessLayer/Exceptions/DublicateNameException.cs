namespace BusinessLayer.Exceptions;

public class DublicateNameException : SecureException
{
    public const string PostgresDublicateErrorCode = "23505";
    internal static string GetErrorMessage(string name) => $"The name '{name}' already exists. Please choose another name.";


    public DublicateNameException(string? message, Exception? innerException)
        : base(EventIds.DatabaseError, message, innerException)
    {
    }
}
