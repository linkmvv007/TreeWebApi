using Microsoft.Extensions.Logging;

namespace BusinessLayer.Exceptions;

public static class EventIds
{
    public static readonly EventId UnhandledException = new EventId(5000, "UnhandledException");

    public static readonly EventId DatabaseError = new EventId(1001, "DatabaseError");
    public static readonly EventId NetworkError = new EventId(2001, "NetworkError");

}
