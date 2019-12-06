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

        public Book Find(int id) 
        {
            return Include(_context.Book).First(b => b.Id == id);
        }

        
        public IQueryable<Book> FindAll() 
        {
            return Include(_context.Book);
        }

        public IQueryable<Book> Include(IQueryable<Book> books) 
        {
            // return books.AsNoTracking().Include(b => b.BookCategory).Include(b => b.BookAuthor).Include(b => b.BookStatus);
            return books.Include(b => b.BookCategory).Include(b => b.BookAuthor).Include(b => b.BookStatus);
            // return books;
        }



        public IQueryable<Book> Order(IQueryable<Book> books, string sortName) 
        {
            // TODO: tranlsate this to a generic method using IQueryable? 
            // May not be worth it, this is clear and conscise as is
            switch(sortName) {
                case nameof(Book.Id) + "_asc":
                    books = books.OrderBy(b => b.Id);
                    break;
                case nameof(Book.Id) + "_desc":
                    books = books.OrderByDescending(b => b.Id);
                    break;
                case nameof(Book.Name) + "_asc":
                    books = books.OrderBy(b => b.Name);
                    break;
                case nameof(Book.Name) + "_desc":
                    books = books.OrderByDescending(b => b.Name);
                    break;
                case nameof(Book.BookAuthor) + "_asc":
                    books = books.OrderBy(b => b.BookAuthor.Name);
                    break;
                case nameof(Book.BookAuthor) + "_desc":
                    books = books.OrderByDescending(b => b.BookAuthor.Name);
                    break;
                case nameof(Book.ReadYear) + "_asc":
                    books = books.OrderBy(b => b.ReadYear);
                    break;
                case nameof(Book.ReadYear) + "_desc":
                    books = books.OrderByDescending(b => b.ReadYear);
                    break;
                case nameof(Book.BookCategory) + "_asc":
                    books = books.OrderBy(b => b.BookCategory.Name);
                    break;
                case nameof(Book.BookCategory) + "_desc":
                    books = books.OrderByDescending(b => b.BookCategory.Name);
                    break;
                default:
                    break;
            }
            return books;
        }
 
        public IQueryable<Book> Filter(IQueryable<Book> books, string bookName, List<long> bookAuthors, List<long> bookCategories, List<long> bookStatuses, List<string> readYears)
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
