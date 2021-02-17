
using System.Linq;
using b.Api.Models;
using System.Collections.Generic;

namespace b.Api.Extensions
{

    public static class PagingExtensions
    {

        public static PaginationResponse<T> Page<T>(this IEnumerable<T> collection, PaginationInfo info)
        {

           var result = collection.Skip(info.offset * info.limit).Take(info.limit);

            return new PaginationResponse<T> { Items = result, Count = collection.Count() };
        }

        public static PaginationResponse<T> Page<T>(this IQueryable<T> collection, PaginationInfo info)
        {
            var result = collection.Skip(info.offset * info.limit).Take(info.limit);
            return new PaginationResponse<T> { Items = result, Count = collection.Count() };
        }
    }
}
