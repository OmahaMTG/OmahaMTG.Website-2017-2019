using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OmahaMTG.Data
{
    public static class QueryableExtensions
    {
        public static async Task<SkipTakeSet<T>> AsSkipTakeSet<T>(this IQueryable<T> query, int skip, int take)
        {
            var result = new SkipTakeSet<T>
            {
                TotalRecords = query.Count(),
                Records = await query.Skip(skip).Take(take).ToListAsync(),
                Skipped = skip,
                Taken = take
            };
            return result;
        }

        public static async Task<SkipTakeSet<TOut>> AsSkipTakeSet<TIn, TOut>(this IQueryable<TIn> query, int skip, int take, Func<TIn, TOut> mappingFunction)
        {
            var records = await query.Skip(skip).Take(take).ToListAsync();
            var result = new SkipTakeSet<TOut>
            {
                TotalRecords = query.Count(),
                
                Records = records.Select(mappingFunction),
                Skipped = skip,
                Taken = take
            };
            return result;
        }
    }
}
