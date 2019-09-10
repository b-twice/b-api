using System.Linq;
using Microsoft.EntityFrameworkCore;
using Budget.API.Entities;
using Budget.API.Models.Book;
using System.Collections.Generic;

namespace Budget.API.Database
{

    public class BookRepository
    {
        private readonly AppDatabaseContext _context;

        public BookRepository(AppDatabaseContext context)
        {
            _context = context;
        }

        public Book Find(int id) 
        {
            return Include(_context.Books).First(b => b.id == id);
        }

        
        public IQueryable<Book> FindAll() 
        {
            return Include(_context.Books);
        }

        public IQueryable<Book> Include(IQueryable<Book> books) 
        {
            return books.Include(b => b.bookCategory).Include(b => b.bookAuthor).Include(b => b.bookStatus);
        }



        public IQueryable<Book> Order(IQueryable<Book> books, string sortName) 
        {
            // TODO: tranlsate this to a generic method using IQueryable? 
            // May not be worth it, this is clear and conscise as is
            switch(sortName) {
                case nameof(Book.id) + "_asc":
                    books = books.OrderBy(b => b.id);
                    break;
                case nameof(Book.id) + "_desc":
                    books = books.OrderByDescending(b => b.id);
                    break;
                case nameof(Book.name) + "_asc":
                    books = books.OrderBy(b => b.name);
                    break;
                case nameof(Book.name) + "_desc":
                    books = books.OrderByDescending(b => b.name);
                    break;
                case nameof(Book.bookAuthor) + "_asc":
                    books = books.OrderBy(b => b.bookAuthor.name);
                    break;
                case nameof(Book.bookAuthor) + "_desc":
                    books = books.OrderByDescending(b => b.bookAuthor.name);
                    break;
                case nameof(Book.readYear) + "_asc":
                    books = books.OrderBy(b => b.readYear);
                    break;
                case nameof(Book.readYear) + "_desc":
                    books = books.OrderByDescending(b => b.readYear);
                    break;
                case nameof(Book.bookCategory) + "_asc":
                    books = books.OrderBy(b => b.bookCategory.name);
                    break;
                case nameof(Book.bookCategory) + "_desc":
                    books = books.OrderByDescending(b => b.bookCategory.name);
                    break;
                default:
                    break;
            }
            return books;
        }
 
        public IQueryable<Book> Filter(IQueryable<Book> books, string bookName, List<int> bookAuthors, List<int> bookCategories, List<int> bookStatuses, List<string> readYears)
        {
            if (!string.IsNullOrEmpty(bookName)) {
                books = books.Where(b => b.name.Contains(bookName));
            }
            if (bookAuthors?.Any() == true) {
                books = books.Where(b => bookAuthors.Exists(id => id == b.bookAuthor.id));
            }
            if (bookStatuses?.Any() == true) {
                books = books.Where(b => bookStatuses.Exists(id => id == b.bookStatus.id));
            }
            if (bookCategories?.Any() == true) {
                books = books.Where(b => bookCategories.Exists(id => id == b.bookCategory.id));
            }
            if (readYears?.Any() == true) {
                books = books.Where(b => readYears.Exists(year => year == b.readYear));
            }
           return books;
        }

  }
}
