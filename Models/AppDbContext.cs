using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace B.API.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asset> Asset { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookAuthor> BookAuthor { get; set; }
        public virtual DbSet<BookCategory> BookCategory { get; set; }
        public virtual DbSet<BookStatus> BookStatus { get; set; }
        public virtual DbSet<Debt> Debt { get; set; }
        public virtual DbSet<Earning> Earning { get; set; }
        public virtual DbSet<Investment> Investment { get; set; }
        public virtual DbSet<TransactionCategory> TransactionCategory { get; set; }
        public virtual DbSet<TransactionRecord> TransactionRecord { get; set; }
        public virtual DbSet<TransactionRecordTag> TransactionRecordTag { get; set; }
        public virtual DbSet<TransactionTag> TransactionTag { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<YearlyPlannedExpense> YearlyPlannedExpense { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("DataSource=../b-database/app.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Year).IsRequired();
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.BookAuthorId, e.BookCategoryId, e.BookStatusId, e.ReadDate })
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

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

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<BookStatus>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Keyword).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Debt>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Year).IsRequired();
            });

            modelBuilder.Entity<Earning>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Year).IsRequired();
            });

            modelBuilder.Entity<Investment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Year).IsRequired();
            });

            modelBuilder.Entity<TransactionCategory>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<TransactionRecord>(entity =>
            {
                entity.HasIndex(e => new { e.BankId, e.UserId, e.CategoryId, e.Date, e.Description, e.Amount })
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Date).IsRequired();

                entity.Property(e => e.Description).IsRequired();

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

            modelBuilder.Entity<TransactionRecordTag>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TransactionRecordTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TransactionRecord)
                    .WithMany(p => p.TransactionRecordTag)
                    .HasForeignKey(d => d.TransactionRecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TransactionTag>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.Phone).IsRequired();
            });

            modelBuilder.Entity<YearlyPlannedExpense>(entity =>
            {
                entity.HasIndex(e => new { e.Date, e.CategoryId })
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Date).IsRequired();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.YearlyPlannedExpense)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
