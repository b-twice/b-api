using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Repository
{

    public class TransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public TransactionRecord Find(long id) 
        {
            return Include(_context.TransactionRecords.AsNoTracking()).First(b => b.Id == id);
        }

        
        public IQueryable<TransactionRecord> FindAll() 
        {
            return Include(_context.TransactionRecords).AsNoTracking();
        }

        public IQueryable<TransactionRecord> Include(IQueryable<TransactionRecord> items) 
        {
            return items.Include(o => o.Bank).Include(o => o.Category).Include(o => o.User).Include(o => o.TransactionRecordTags).ThenInclude(o => o.Tag);
        }



        public IQueryable<TransactionRecord> Order(IQueryable<TransactionRecord> items, string sortName) 
        {
            items = sortName switch 
            {
                "id_asc" => items.OrderBy(o => o.Id),
                "id_desc" => items.OrderByDescending(o => o.Id),
                "date_asc" => items.OrderBy(o => o.Date),
                "date_desc" => items.OrderByDescending(o => o.Date),
                "userId_asc" => items.OrderBy(o => o.User.FirstName),
                "userId_desc" => items.OrderByDescending(o => o.User.FirstName),
                "bankId_asc" => items.OrderBy(o => o.Bank.Name),
                "bankId_desc" => items.OrderByDescending(o => o.Bank.Name),
                "categoryId_asc" => items.OrderBy(o => o.Category.Name),
                "categoryId_desc" => items.OrderByDescending(o => o.Category.Name),
                "amount_asc" => items.OrderBy(o => o.Amount),
                "amount_desc" => items.OrderByDescending(o => o.Amount),
                "description_asc" => items.OrderBy(o => o.Description),
                "description_desc" => items.OrderByDescending(o => o.Description),
                 _ => items
            };
            return items;
        }
 
 
        public IQueryable<TransactionRecord> Filter(IQueryable<TransactionRecord> items, string description, List<long> categories, List<long> tags, List<long> banks, List<long> users, List<string> years, List<string> months)
        {
            if (!string.IsNullOrEmpty(description)) {
                items = items.Where(o => o.Description.ToLower().Contains(description.ToLower()));
            }
            if (categories?.Any() == true) {
                items = items.Where(o => categories.Contains(o.Category.Id));
            }
            if (tags?.Any() == true) {
                bool findUntagged = tags.Any(t => t == 0);
                items = items.Where(o => (findUntagged && o.TransactionRecordTags.Count() == 0) ||  o.TransactionRecordTags.Any(r => tags.Any(t => t == r.TagId)));
            }
            if (banks?.Any() == true) {
                items = items.Where(o => banks.Contains(o.Bank.Id));
            }
            if (users?.Any() == true) {
                items = items.Where(o => users.Contains(o.User.Id));
            }
            if (years?.Any() == true) {
                items = items.Where(b => years.Contains(b.Date.Substring(0,4)));
            }
            if (months?.Any() == true) {
                items = items.Where(b => months.Contains(b.Date.Substring(0,4)));
            }
            return items;
        }

  }
}
