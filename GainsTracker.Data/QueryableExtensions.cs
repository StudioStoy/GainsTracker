using System;
using System.Linq;

namespace GainsTracker.Data;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyIncludes<T>(this IQueryable<T> query,
        Func<IQueryable<T>, IQueryable<T>>? includes = null) where T : class
    {
        if (includes != null) query = includes(query);
        return query;
    }
}
