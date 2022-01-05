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



        public IQueryable<Cookbook> Order(IQueryable<Cookbook> books, string sortName) 
        {
            // TODO: tranlsate this to a generic method using IQueryable? 
            // May not be worth it, this is clear and conscise as is
            switch(sortName) {
                case "id_asc":
                    books = books.OrderBy(b => b.Id);
                    break;
                case "id_desc":
                    books = books.OrderByDescending(b => b.Id);
                    break;
                case "name_asc":
                    books = books.OrderBy(b => b.Name);
                    break;
                case "name_desc":
                    books = books.OrderByDescending(b => b.Name);
                    break;
                case "bookAuthor_asc":
                    books = books.OrderBy(b => b.CookbookAuthor.Name);
                    break;
                case "bookAuthor_desc":
                    books = books.OrderByDescending(b => b.CookbookAuthor.Name);
                    break;
                default:
                    break;
            }
            return books;
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
