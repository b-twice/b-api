using B.API.Models;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models {
    public partial class ApiDbContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            // prevent self referential loops
            modelBuilder.Entity<BookAuthor>().Ignore(c => c.Book);
            modelBuilder.Entity<BookStatus>().Ignore(c => c.Book);
            modelBuilder.Entity<BookCategory>().Ignore(c => c.Book);
            modelBuilder.Entity<Users>().Ignore(c => c.TransactionRecord);
            modelBuilder.Entity<Users>().Ignore(c => c.Earning);
            modelBuilder.Entity<Bank>().Ignore(c => c.TransactionRecord);
            modelBuilder.Entity<TransactionCategory>().Ignore(c => c.TransactionRecord);
        }
    }
}
