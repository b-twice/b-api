using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Database
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
            // TODO: tranlsate this to a generic method using IQueryable? 
            // May not be worth it, this is clear and conscise as is
            switch(sortName) {
                case "id_asc":
                    items = items.OrderBy(o => o.Id);
                    break;
                case "id_desc":
                    items = items.OrderByDescending(o => o.Id);
                    break;
                case "date_asc":
                    items = items.OrderBy(o => o.Date);
                    break;
                case "date_desc":
                    items = items.OrderByDescending(o => o.Date);
                    break;
                case "user_asc":
                    items = items.OrderBy(o => o.User.FirstName);
                    break;
                case "user_desc":
                    items = items.OrderByDescending(o => o.User.FirstName);
                    break;
                case "bank_asc":
                    items = items.OrderBy(o => o.Bank.Name);
                    break;
                case "bank_desc":
                    items = items.OrderByDescending(o => o.Bank.Name);
                    break;
                case "category_asc":
                    items = items.OrderBy(o => o.Category.Name);
                    break;
                case "category_desc":
                    items = items.OrderByDescending(o => o.Category.Name);
                    break;
                case "amount_asc":
                    items = items.OrderBy(o => o.Amount);
                    break;
                case "amount_desc":
                    items = items.OrderByDescending(o => o.Amount);
                    break;
                case "description_asc":
                    items = items.OrderBy(o => o.Description); break;
                case "description_desc":
                    items = items.OrderByDescending(o => o.Description);
                    break;
                default:
                    break;
            }
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
