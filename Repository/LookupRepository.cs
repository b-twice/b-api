using System.Linq;
using B.API.Models;

namespace B.API.Database
{

    public class LookupRepository
    {
        private readonly AppDbContext _context;

        public LookupRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> OrderBy<T>(IQueryable<T> items, string sortName) where T: IAppLookup
        {
            switch(sortName) {
                case "id_asc":
                    items = items.OrderBy(b => b.Id);
                    break;
                case "id_desc":
                    items = items.OrderByDescending(b => b.Id);
                    break;
                case "name_asc":
                    items = items.OrderBy(b => b.Name);
                    break;
                case "name_desc":
                    items = items.OrderByDescending(b => b.Name);
                    break;
               default:
                    break;
            }
            return (IQueryable<T>)items;
        }
        public IQueryable<T> Filter<T>(IQueryable<T> items, string name) where T: IAppLookup
        {
            if (!string.IsNullOrEmpty(name)) {
                items = items.Where(o => o.Name.Contains(name));
            }
            return items;
        }



        public PaginatedResult<T> Paginate<T>(IQueryable<T> items, int pageNumber, int pageSize) {
          var paginatedList = PaginatedList<T>.Create(items, pageNumber, pageSize);
          return new PaginatedResult<T>(paginatedList, paginatedList.TotalCount);
        }

  }
}
