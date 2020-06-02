using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Database
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
            return Include(_context.Book.AsNoTracking()).First(b => b.Id == id);
        }

        
        public IQueryable<Book> FindAll() 
        {
            return Include(_context.Book).AsNoTracking();
        }

        public IQueryable<Book> Include(IQueryable<Book> books) 
        {
            return books.Include(b => b.BookCategory).Include(b => b.BookAuthor).Include(b => b.BookStatus);
        }



        public IQueryable<Book> Order(IQueryable<Book> books, string sortName) 
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
                    books = books.OrderBy(b => b.BookAuthor.Name);
                    break;
                case "bookAuthor_desc":
                    books = books.OrderByDescending(b => b.BookAuthor.Name);
                    break;
                case "readDate_asc":
                    books = books.OrderBy(b => b.ReadDate);
                    break;
                case "readDate_desc":
                    books = books.OrderByDescending(b => b.ReadDate);
                    break;
                case "bookCategory_asc":
                    books = books.OrderBy(b => b.BookCategory.Name);
                    break;
                case "bookCategory_desc":
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
                books = books.Where(b => readYears.Contains(b.ReadDate.Substring(0,4)));
            }
           return books;
        }

  }
}
