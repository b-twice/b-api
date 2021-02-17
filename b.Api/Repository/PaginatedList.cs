using System;
using System.Collections.Generic;
using System.Linq;

namespace b.Api.Database
{
  // Pulled from: https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-2.2#add-paging-to-index-method 
  public class PaginatedList<T> : List<T>
  {
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }

    // the count is all possible results beyond the current page of items
    public int TotalCount { get; private set; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
      TotalCount = count;
      PageIndex = pageIndex;
      TotalPages = (int)Math.Ceiling(count / (double)pageSize);

      this.AddRange(items);
    }

    public bool HasPreviousPage
    {
      get
      {
        return (PageIndex > 1);
      }
    }

    public bool HasNextPage
    {
      get
      {
        return (PageIndex < TotalPages);
      }
    }

    public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
    {
      var count = source.Count();
      var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
      return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
  }
}