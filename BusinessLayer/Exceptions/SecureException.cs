using Microsoft.Extensions.Logging;

namespace BusinessLayer.Exceptions;
public class SecureException : ApplicationException
{
    public EventId EventId { get; private init; }

    public SecureException(EventId eventId, string? message, Exception? innerException)
        : base(message, innerException)
    {
        EventId = eventId;
    }
}