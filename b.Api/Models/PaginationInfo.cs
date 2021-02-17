using System;
namespace b.Api.Models
{
    public record PaginationInfo
    {
        public int limit { get; init; }
        public int offset { get; init; }
    }
}
