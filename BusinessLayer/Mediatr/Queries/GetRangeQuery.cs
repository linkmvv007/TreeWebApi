﻿using BusinessLayer.Json;
using BusinessLayer.RequestContext.Journal;
using DataLayer.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Mediatr.Queries;

public record GetRangeQuery(GetRangeContext context) : IRequest<JournalsJson?>
{ }


/// <summary>
/// Handler for <see cref="GetRangeQuery"/>
/// </summary>
public class GetRangeQueryHandler : IRequestHandler<GetRangeQuery, JournalsJson?>
{
    private readonly LogContext _dbContext;


    public GetRangeQueryHandler(LogContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<JournalsJson?> Handle(GetRangeQuery request, CancellationToken cancellationToken)
    {
        IQueryable<ExceptionLog> query = _dbContext.ExceptionLogs.AsNoTracking();

        if (request.context.filter is not null)
        {
            var searchStr = request.context.filter.search;

            if (!string.IsNullOrEmpty(searchStr))
            {
                query = query.Where(e => e.StackTrace != null
                && EF.Functions.ILike(e.StackTrace, $"%{searchStr}%"));
            }

            var dates = request.context.filter.GetValidFromToDate();

            DateTime? fromDate = dates.from;
            if (fromDate is not null)
            {
                query = query.Where(e => e.CreatedAt >= fromDate);
            }


            DateTime? toDate = dates.to;
            if (toDate is not null)
            {
                query = query.Where(e => e.CreatedAt <= toDate);
            }
        }

        query = query.OrderByDescending(x => x.CreatedAt);

        if (request.context.skip > 0)
        {
            query = query.Skip(request.context.skip);
        }

        if (request.context.take > 0)
        {
            query = query.Take(request.context.take);
        }

        var results = await query
            .Select(e => new JournalInfo(e.Id, e.Id, e.CreatedAt))
            .ToListAsync();


        return new JournalsJson(request.context.skip, results.Count(), results);
    }
}