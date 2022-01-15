using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Repository
{

    public class BookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public Book Find(long id) 
        {
            return Include(_context.Books.AsNoTracking()).First(b => b.Id == id);
        }

        
        public IQueryable<Book> FindAll() 
        {
            return Include(_context.Books).AsNoTracking();
        }

        public IQueryable<Book> Include(IQueryable<Book> books) 
        {
            return books.Include(b => b.BookCategory).Include(b => b.BookAuthor).Include(b => b.BookStatus);
        }



        public IQueryable<Book> Order(IQueryable<Book> items, string sortName) 
        {
            items = sortName switch
            {
                "id_asc" => items.OrderBy(e => e.Id),
                "id_desc" => items.OrderByDescending(e => e.Id),
                "name_asc" => items.OrderBy(e => e.Name),
                "name_desc" => items.OrderByDescending(e => e.Name),
                "bookAuthorId_asc" => items.OrderBy(e => e.BookAuthor.Name),
                "bookAuthorId_desc" => items.OrderByDescending(e => e.BookAuthor.Name),
                "readDate_asc" => items.OrderBy(e => e.ReadDate),
                "readDate_desc" => items.OrderByDescending(e => e.ReadDate),
                "bookCategoryId_asc" => items.OrderBy(e => e.BookCategory.Name),
                "bookCategoryId_desc" => items.OrderByDescending(e => e.BookCategory.Name),
                "bookStatusId_asc" => items.OrderBy(e => e.BookStatus.Name),
                "bookStatusId_desc" => items.OrderByDescending(e => e.BookStatus.Name),
               _ => items
            };
            return items;
        }
 
        public IQueryable<Book> Filter(IQueryable<Book> books, string bookName, List<long> bookAuthors, List<long> bookCategories, List<long> bookStatuses, List<string> readYears)
        {
            if (!string.IsNullOrEmpty(bookName)) {
                books = books.Where(b => b.Name.ToLower().Contains(bookName.ToLower()));
            }
            if (bookAuthors?.Any() == true) {
                books = books.Where(b => bookAuthors.Contains(b.BookAuthor.Id));
            }
            if (bookStatuses?.Any() == true) {
                books = books.Where(b => bookStatuses.Contains(b.BookStatus.Id));
            }
            if (bookCategories?.Any() == true) {
                books = books.Where(b => bookCategories.Contains(b.BookCategory.Id));
            }
            if (readYears?.Any() == true) {
                books = books.Where(b => readYears.Contains(b.ReadDate.Substring(0,4)));
            }
           return books;
        }

  }
}
