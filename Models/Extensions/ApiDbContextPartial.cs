using B.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace B.API.Models {
    public partial class AppDbContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            // prevent self referential loops
            modelBuilder.Entity<PostGroup>().Ignore(c => c.Post);
            modelBuilder.Entity<BookAuthor>().Ignore(c => c.Book);
            modelBuilder.Entity<BookStatus>().Ignore(c => c.Book);
            modelBuilder.Entity<BookCategory>().Ignore(c => c.Book);
            modelBuilder.Entity<User>().Ignore(c => c.TransactionRecord);
            modelBuilder.Entity<Bank>().Ignore(c => c.TransactionRecord);
            modelBuilder.Entity<TransactionCategory>().Ignore(c => c.TransactionRecord);
            modelBuilder.Entity<TransactionRecordTag>().Ignore(c => c.TransactionRecord);
            modelBuilder.Entity<TransactionTag>().Ignore(c => c.TransactionRecordTag);


            modelBuilder.Entity<TransactionRecord>().Property(t => t.Amount).HasColumnType("decimal");

        }
    }
}
