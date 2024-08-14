using BusinessLayer.Exceptions;
using BusinessLayer.Exceptions.Responces;
using DataLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BusinessLayer.Middleware;

/// <summary>
///     Middleware handle for exceptions
/// </summary>
public class AppMiddlewareException
{
    private readonly IWebHostEnvironment _environment;
    private readonly RequestDelegate _next;

    public AppMiddlewareException(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context, LogContext dbContext)
    {
        try
        {
            await _next(context);
        }

        catch (SecureException ex)
        {
            await HandleExceptionAsync(context, dbContext, ex, ExceptionTypes.Secure, ex.EventId, HttpStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, dbContext, ex, ExceptionTypes.Exception, EventIds.NetworkError, HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    ///     Create error response
    /// </summary>
    private async Task HandleExceptionAsync(HttpContext context, LogContext dbContext, Exception exp, ExceptionTypes type, EventId eventId, HttpStatusCode code)
    {
        // record to db:
        var logRecord = await GetLogInfo(context, exp, eventId);
        dbContext.ExceptionLogs.Add(logRecord);

        await dbContext.SaveChangesAsync();

        var msg = exp is SecureException ? exp.Message : $"Internal server error ID ={logRecord.Id}";

        var resultObject = new ExceptionJson(type, logRecord.Id, new MessageBody(msg));


        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        await context.Response.WriteAsync(resultObject.SerializeToString());
    }

    private async Task<ExceptionLog> GetLogInfo(HttpContext context, Exception exp, EventId eventId)
    {
        string requestBody;
        using (var sr = new StreamReader(context.Request.Body))
        {
            requestBody = await sr.ReadToEndAsync();
        }

        return new ExceptionLog
        {
            RequestBody = requestBody,
            RequestParameters = context.Request?.QueryString.Value,
            StackTrace = exp.StackTrace,
            EventId = eventId.Id
        };
    }
}


