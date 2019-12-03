using System.Linq;
using B.API.Entities;
using B.API.Models.Common;
using System.Collections.Generic;

namespace B.API.Database
{

    public class LookupRepository
    {
        private readonly DatabaseContext _context;

        public LookupRepository(DatabaseContext context)
        {
            _context = context;
        }
        public IQueryable<T> OrderBy<T>(IQueryable<T> items, string sortName) 
        {
            switch(sortName) {
                case nameof(AppLookup.Id) + "_asc":
                    items = items.OrderBy(b => typeof(T).GetProperty("Id").GetValue(b));
                    break;
                case nameof(AppLookup.Id) + "_desc":
                    items = items.OrderByDescending(b => typeof(T).GetProperty("Id").GetValue(b));
                    break;
                case nameof(AppLookup.Name) + "_asc":
                    items = items.OrderBy(b => typeof(T).GetProperty("Name").GetValue(b));
                    break;
                case nameof(AppLookup.Name) + "_desc":
                    items = items.OrderByDescending(b => typeof(T).GetProperty("Name").GetValue(b));
                    break;
               default:
                    break;
            }
            return items;
        }
        public IQueryable<T> Filter<T>(IQueryable<T> items, string name)
        {
            if (!string.IsNullOrEmpty(name)) {
                items = items.Where(o => ((string)typeof(T).GetProperty("Name").GetValue(o)).Contains(name));
            }
            return items;
        }



        public PaginatedResult<T> Paginate<T>(IQueryable<T> items, int pageNumber, int pageSize) {
          var paginatedList = PaginatedList<T>.Create(items, pageNumber, pageSize);
          return new PaginatedResult<T>(paginatedList, paginatedList.TotalCount);
        }

  }
}
