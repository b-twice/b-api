using System.Linq;
using Budget.API.Entities;
using Microsoft.Extensions.Logging;
using Budget.API.Models.Common;

namespace Budget.API.Database
{

    public class LookupRepository
    {
        private readonly DatabaseContext _context;

        public LookupRepository(DatabaseContext context)
        {
            _context = context;
        }
        public IQueryable<AppLookup> OrderBy(IQueryable<AppLookup> items, string sortName) 
        {
            switch(sortName) {
                case nameof(AppLookup.id) + "_asc":
                    items = items.OrderBy(b => b.id);
                    break;
                case nameof(AppLookup.id) + "_desc":
                    items = items.OrderByDescending(b => b.id);
                    break;
                case nameof(AppLookup.name) + "_asc":
                    items = items.OrderBy(b => b.name);
                    break;
                case nameof(AppLookup.name) + "_desc":
                    items = items.OrderByDescending(b => b.name);
                    break;
               default:
                    break;
            }
            return items;
        }
        public IQueryable<AppLookup> Filter(IQueryable<AppLookup> items, string name)
        {
            if (!string.IsNullOrEmpty(name)) {
                items = items.Where(o => o.name.Contains(name));
            }
            return items;
        }



        public PaginatedResult<AppLookup> Paginate(IQueryable<AppLookup> items, int pageNumber, int pageSize) {
          var paginatedList = PaginatedList<AppLookup>.Create(items, pageNumber, pageSize);
          return new PaginatedResult<AppLookup>(paginatedList, paginatedList.TotalCount);
        }

  }
}
