using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Database
{

    public class BookRepository
    {
        private readonly ApiDbContext _context;

        public BookRepository(ApiDbContext context)
        {
            _context = context;
        }

        public Books Find(int id) 
        {
            return Include(_context.Books).First(b => b.Id == id);
        }

        
        public IQueryable<Books> FindAll() 
        {
            return Include(_context.Books);
        }

        public IQueryable<Books> Include(IQueryable<Books> books) 
        {
            // return books.AsNoTracking().Include(b => b.BookCategory).Include(b => b.BookAuthor).Include(b => b.BookStatus);
            return books.Include(b => b.BookCategory).Include(b => b.BookAuthor).Include(b => b.BookStatus);
            // return books;
        }



        public IQueryable<Books> Order(IQueryable<Books> books, string sortName) 
        {
            // TODO: tranlsate this to a generic method using IQueryable? 
            // May not be worth it, this is clear and conscise as is
            switch(sortName) {
                case nameof(Books.Id) + "_asc":
                    books = books.OrderBy(b => b.Id);
                    break;
                case nameof(Books.Id) + "_desc":
                    books = books.OrderByDescending(b => b.Id);
                    break;
                case nameof(Books.Name) + "_asc":
                    books = books.OrderBy(b => b.Name);
                    break;
                case nameof(Books.Name) + "_desc":
                    books = books.OrderByDescending(b => b.Name);
                    break;
                case nameof(Books.BookAuthor) + "_asc":
                    books = books.OrderBy(b => b.BookAuthor.Name);
                    break;
                case nameof(Books.BookAuthor) + "_desc":
                    books = books.OrderByDescending(b => b.BookAuthor.Name);
                    break;
                case nameof(Books.ReadYear) + "_asc":
                    books = books.OrderBy(b => b.ReadYear);
                    break;
                case nameof(Books.ReadYear) + "_desc":
                    books = books.OrderByDescending(b => b.ReadYear);
                    break;
                case nameof(Books.BookCategory) + "_asc":
                    books = books.OrderBy(b => b.BookCategory.Name);
                    break;
                case nameof(Books.BookCategory) + "_desc":
                    books = books.OrderByDescending(b => b.BookCategory.Name);
                    break;
                default:
                    break;
            }
            return books;
        }
 
        public IQueryable<Books> Filter(IQueryable<Books> books, string bookName, List<long> bookAuthors, List<long> bookCategories, List<long> bookStatuses, List<string> readYears)
        {
            if (!string.IsNullOrEmpty(bookName)) {
                books = books.Where(b => b.Name.Contains(bookName));
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
                books = books.Where(b => readYears.Contains(b.ReadYear));
            }
           return books;
        }

  }
}
