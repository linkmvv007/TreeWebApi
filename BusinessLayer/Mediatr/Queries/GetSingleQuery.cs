using BusinessLayer.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Mediatr.Queries;

public record GetSingleQuery(int id) : IRequest<JournalJson?>
{ }


/// <summary>
/// Handler for <see cref="GetSingleQuery"/>
/// </summary>
public class GetSingleQueryHandler : IRequestHandler<GetSingleQuery, JournalJson?>
{
    private readonly LogContext _dbContext;


    public GetSingleQueryHandler(LogContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<JournalJson?> Handle(GetSingleQuery request, CancellationToken cancellationToken)
    {
        var item = await _dbContext.ExceptionLogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.id);

        return item is null ? null : new JournalJson(item.StackTrace, item.Id, item.Id, item.CreatedAt);
    }
}