using B.API.Models;
using Microsoft.EntityFrameworkCore;

namespace B.API.Models {
    public partial class ApiDbContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            // prevent self referential loops
            modelBuilder.Entity<BookAuthors>().Ignore(c => c.Books);
            modelBuilder.Entity<BookStatuses>().Ignore(c => c.Books);
            modelBuilder.Entity<BookCategories>().Ignore(c => c.Books);
            modelBuilder.Entity<Users>().Ignore(c => c.Transactions);
            modelBuilder.Entity<Users>().Ignore(c => c.Earnings);
            modelBuilder.Entity<Banks>().Ignore(c => c.Transactions);
            modelBuilder.Entity<TransactionCategories>().Ignore(c => c.Transactions);
        }
    }
}
