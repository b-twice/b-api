using System;
using System.Collections.Generic;

namespace b.Api.Models
{
    public record PaginationResponse<T>
    {
        public IEnumerable<T> Items { get; init; }
        public int Count { get; init; }
    }
}
