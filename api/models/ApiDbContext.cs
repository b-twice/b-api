using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace B.api.models
{
    public partial class ApiDbContext : DbContext
    {
        public ApiDbContext()
        {
        }

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assets> Assets { get; set; }
        public virtual DbSet<Banks> Banks { get; set; }
        public virtual DbSet<BookAuthors> BookAuthors { get; set; }
        public virtual DbSet<BookCategories> BookCategories { get; set; }
        public virtual DbSet<BookStatuses> BookStatuses { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Earnings> Earnings { get; set; }
        public virtual DbSet<Investments> Investments { get; set; }
        public virtual DbSet<TransactionCategories> TransactionCategories { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        // Unable to generate entity type for table 'TransactionsStaging'. Please see the warning messages.
        // Unable to generate entity type for table 'CategoryMap'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("DataSource=app.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Assets>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Auto)
                    .HasColumnName("auto")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Home)
                    .HasColumnName("home")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Hsa)
                    .HasColumnName("hsa")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Retirement)
                    .HasColumnName("retirement")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Saving)
                    .HasColumnName("saving")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Stock)
                    .HasColumnName("stock")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasColumnName("year");
            });

            modelBuilder.Entity<Banks>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<BookAuthors>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<BookCategories>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<BookStatuses>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Keyword)
                    .IsRequired()
                    .HasColumnName("keyword");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.BookAuthorId, e.BookCategoryId, e.BookStatusId, e.ReadYear })
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.BookAuthorId).HasColumnName("book_author_id");

                entity.Property(e => e.BookCategoryId).HasColumnName("book_category_id");

                entity.Property(e => e.BookStatusId).HasColumnName("book_status_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.ReadYear)
                    .IsRequired()
                    .HasColumnName("read_year");

                entity.HasOne(d => d.BookAuthor)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.BookAuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.BookCategory)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.BookCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.BookStatus)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.BookStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Earnings>(entity =>
            {
                entity.HasIndex(e => new { e.Year, e.UserId })
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Gross)
                    .HasColumnName("gross")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Taxable)
                    .HasColumnName("taxable")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Taxed)
                    .HasColumnName("taxed")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasColumnName("year");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Earnings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Investments>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Hsa)
                    .HasColumnName("hsa")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Ira)
                    .HasColumnName("ira")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Roth)
                    .HasColumnName("roth")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Saving)
                    .HasColumnName("saving")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Stock)
                    .HasColumnName("stock")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasColumnName("year");
            });

            modelBuilder.Entity<TransactionCategories>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("transactions");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("transaction_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.BankId).HasColumnName("bank_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Wedding)
                    .HasColumnName("wedding")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => new { e.FirstName, e.LastName })
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name");
            });
        }
    }
}
