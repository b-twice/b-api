using System.Collections.Generic;

namespace B.API.Models
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; private set; }
        public int Count { get; private set; }

        public PaginatedResult(List<T> items, int count)
        {
          Items = items;
          Count = count; // the count is all possible results beyond the current page of items
       }

        
    }
}