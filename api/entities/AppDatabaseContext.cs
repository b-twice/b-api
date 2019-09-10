using Microsoft.EntityFrameworkCore;
using Budget.API.Models.Core;
using Budget.API.Models.Finance;
using Budget.API.Models.Food;
using Budget.API.Models.Book;

namespace Budget.API.Entities
{
    // DB Context gets registered in configure services
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options)
            : base(options)
        { }

        // BOOKS
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookStatus> BookStatuses { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Book> Books { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // BOOKS
            bookCreating(modelBuilder);
        }

        private void bookCreating(ModelBuilder modelBuilder) {
            var builder = modelBuilder.Entity<Book>();
            modelBuilder.Entity<Book>(t =>
            {
                t.Property<int>("BookCategoryId").HasColumnName("book_category_id");
                t.Property<int>("BookAuthorId").HasColumnName("book_author_id");
                t.Property<int>("BookStatusId").HasColumnName("book_status_id");
            });

            builder
                .HasOne<BookCategory>(s => s.bookCategory)
                .WithMany()
                .HasForeignKey("BookCategoryId")
                .HasPrincipalKey(u => u.id);
            builder
                .HasOne<BookAuthor>(s => s.bookAuthor)
                .WithMany()
                .HasForeignKey("BookAuthorId")
                .HasPrincipalKey(u => u.id);
            builder
                .HasOne<BookStatus>(s => s.bookStatus)
                .WithMany()
                .HasForeignKey("BookStatusId")
                .HasPrincipalKey(u => u.id);
        }


   }
}
