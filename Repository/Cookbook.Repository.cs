using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Repository
{

    public class CookbookRepository
    {
        private readonly AppDbContext _context;

        public CookbookRepository(AppDbContext context)
        {
            _context = context;
        }

        public Cookbook Find(long id) 
        {
            return Include(_context.Cookbooks.AsNoTracking()).First(b => b.Id == id);
        }

        
        public IQueryable<Cookbook> FindAll() 
        {
            return Include(_context.Cookbooks).AsNoTracking();
        }

        public IQueryable<Cookbook> Include(IQueryable<Cookbook> books) 
        {
            return books.Include(b => b.CookbookAuthor);
        }



        public IQueryable<Cookbook> Order(IQueryable<Cookbook> items, string sortName) 
        {
            items = sortName switch
            {
                "id_asc" => items.OrderBy(b => b.Id),
                "id_desc" => items.OrderByDescending(b => b.Id),
                "name_asc" => items.OrderBy(b => b.Name),
                "name_desc" => items.OrderByDescending(b => b.Name),
                "bookAuthorId_asc" => items.OrderBy(b => b.CookbookAuthor.Name),
                "bookAuthorId_desc" => items.OrderByDescending(b => b.CookbookAuthor.Name),
               _ => items
            };
            return items;
        }
 
        public IQueryable<Cookbook> Filter(IQueryable<Cookbook> books, string bookName, List<long> bookAuthors)
        {
            if (!string.IsNullOrEmpty(bookName)) {
                books = books.Where(b => b.Name.ToLower().Contains(bookName.ToLower()));
            }
            if (bookAuthors?.Any() == true) {
                books = books.Where(b => bookAuthors.Contains(b.CookbookAuthor.Id));
            }
            return books;
        }

  }
}
