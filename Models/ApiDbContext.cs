using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace B.API.Models
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

        public virtual DbSet<Asset> Asset { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookAuthor> BookAuthor { get; set; }
        public virtual DbSet<BookCategory> BookCategory { get; set; }
        public virtual DbSet<BookStatus> BookStatus { get; set; }
        public virtual DbSet<CategoryMap> CategoryMap { get; set; }
        public virtual DbSet<Earning> Earning { get; set; }
        public virtual DbSet<Investment> Investment { get; set; }
        public virtual DbSet<TransactionCategory> TransactionCategory { get; set; }
        public virtual DbSet<TransactionRecord> TransactionRecord { get; set; }
        public virtual DbSet<TransactionsStaging> TransactionsStaging { get; set; }
        public virtual DbSet<TransactionsStagingView> TransactionsStagingView { get; set; }
        public virtual DbSet<Users> Users { get; set; }

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
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Auto).HasDefaultValueSql("0");

                entity.Property(e => e.Home).HasDefaultValueSql("0");

                entity.Property(e => e.Hsa).HasDefaultValueSql("0");

                entity.Property(e => e.Retirement).HasDefaultValueSql("0");

                entity.Property(e => e.Saving).HasDefaultValueSql("0");

                entity.Property(e => e.Stock).HasDefaultValueSql("0");

                entity.Property(e => e.Year).IsRequired();
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.BookAuthorId, e.BookCategoryId, e.BookStatusId, e.ReadDate })
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.ReadDate).IsRequired();

                entity.HasOne(d => d.BookAuthor)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.BookAuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.BookCategory)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.BookCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.BookStatus)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.BookStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<BookStatus>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Keyword).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<CategoryMap>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<Earning>(entity =>
            {
                entity.HasIndex(e => new { e.Year, e.UserId })
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Gross).HasDefaultValueSql("0");

                entity.Property(e => e.Taxable).HasDefaultValueSql("0");

                entity.Property(e => e.Taxed).HasDefaultValueSql("0");

                entity.Property(e => e.Year).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Earning)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Investment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Hsa).HasDefaultValueSql("0");

                entity.Property(e => e.Ira).HasDefaultValueSql("0");

                entity.Property(e => e.Roth).HasDefaultValueSql("0");

                entity.Property(e => e.Saving).HasDefaultValueSql("0");

                entity.Property(e => e.Stock).HasDefaultValueSql("0");

                entity.Property(e => e.Year).IsRequired();
            });

            modelBuilder.Entity<TransactionCategory>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<TransactionRecord>(entity =>
            {
                entity.Property(e => e.TransactionRecordId).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasDefaultValueSql("0");

                entity.Property(e => e.Date).IsRequired();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Wedding).HasDefaultValueSql("0");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.TransactionRecord)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TransactionRecord)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TransactionRecord)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TransactionsStaging>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Amount).HasDefaultValueSql("0");

                entity.Property(e => e.Automap).HasDefaultValueSql("1");

                entity.Property(e => e.Bank).IsRequired();

                entity.Property(e => e.Wedding).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TransactionsStagingView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("TransactionsStagingView");
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
