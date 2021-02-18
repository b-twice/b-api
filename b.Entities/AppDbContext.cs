using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace b.Entities
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

        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<BookStatus> BookStatuses { get; set; }
        public virtual DbSet<CryptoCoin> CryptoCoins { get; set; }
        public virtual DbSet<CryptoHolding> CryptoHoldings { get; set; }
        public virtual DbSet<CryptoPrice> CryptoPrices { get; set; }
        public virtual DbSet<CryptoSale> CryptoSales { get; set; }
        public virtual DbSet<Debt> Debts { get; set; }
        public virtual DbSet<Earning> Earnings { get; set; }
        public virtual DbSet<Investment> Investments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostGroup> PostGroups { get; set; }
        public virtual DbSet<TransactionCategory> TransactionCategories { get; set; }
        public virtual DbSet<TransactionRecord> TransactionRecords { get; set; }
        public virtual DbSet<TransactionRecordTag> TransactionRecordTags { get; set; }
        public virtual DbSet<TransactionTag> TransactionTags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VCryptoInvestment> VCryptoInvestments { get; set; }
        public virtual DbSet<VCryptoAnnualInvestmentSummary> VCryptoAnnualInvestmentSummaries { get; set; }
        public virtual DbSet<VLatestCryptoCoinPrice> VLatestCryptoCoinPrices { get; set; }
        public virtual DbSet<YearlyPlannedExpense> YearlyPlannedExpenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.ToTable("Asset");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Year).IsRequired();
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("Bank");

                entity.HasIndex(e => e.Name, "IX_Bank_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.HasIndex(e => new { e.Name, e.BookAuthorId, e.BookCategoryId, e.BookStatusId, e.ReadDate }, "IX_Book_Name_BookAuthorId_BookCategoryId_BookStatusId_ReadDate")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.ReadDate).IsRequired();

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

            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.ToTable("BookAuthor");

                entity.HasIndex(e => e.Name, "IX_BookAuthor_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.ToTable("BookCategory");

                entity.HasIndex(e => e.Name, "IX_BookCategory_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<BookStatus>(entity =>
            {
                entity.ToTable("BookStatus");

                entity.HasIndex(e => e.Name, "IX_BookStatus_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Keyword).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<CryptoCoin>(entity =>
            {
                entity.ToTable("CryptoCoin");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<CryptoHolding>(entity =>
            {
                entity.ToTable("CryptoHolding");

                entity.Property(e => e.PurchaseDate).IsRequired();

                entity.HasOne(d => d.CryptoCoin)
                    .WithMany(p => p.CryptoHoldings)
                    .HasForeignKey(d => d.CryptoCoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CryptoPrice>(entity =>
            {
                entity.ToTable("CryptoPrice");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.QueryDate).IsRequired();

                entity.HasOne(d => d.CryptoCoin)
                    .WithMany(p => p.CryptoPrices)
                    .HasForeignKey(d => d.CryptoCoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CryptoSale>(entity =>
            {
                entity.ToTable("CryptoSale");

                entity.Property(e => e.SellDate).IsRequired();

                entity.HasOne(d => d.CryptoHolding)
                    .WithMany(p => p.CryptoSales)
                    .HasForeignKey(d => d.CryptoHoldingId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Debt>(entity =>
            {
                entity.ToTable("Debt");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Year).IsRequired();
            });

            modelBuilder.Entity<Earning>(entity =>
            {
                entity.ToTable("Earning");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Year).IsRequired();
            });

            modelBuilder.Entity<Investment>(entity =>
            {
                entity.ToTable("Investment");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Year).IsRequired();
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.HasIndex(e => new { e.PostGroupId, e.Title, e.Date, e.Path }, "IX_Post_PostGroupId_Title_Date_Path")
                    .IsUnique();

                entity.Property(e => e.Date).IsRequired();

                entity.Property(e => e.Path).IsRequired();

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.PostGroup)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PostGroup>(entity =>
            {
                entity.ToTable("PostGroup");

                entity.HasIndex(e => e.Name, "IX_PostGroup_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<TransactionCategory>(entity =>
            {
                entity.ToTable("TransactionCategory");

                entity.HasIndex(e => e.Name, "IX_TransactionCategory_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<TransactionRecord>(entity =>
            {
                entity.ToTable("TransactionRecord");

                entity.HasIndex(e => new { e.BankId, e.UserId, e.Date, e.Description, e.Amount }, "IX_TransactionRecord_BankId_UserId_Date_Description_Amount")
                    .IsUnique();

                entity.Property(e => e.Date).IsRequired();

                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.TransactionRecords)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TransactionRecords)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TransactionRecords)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TransactionRecordTag>(entity =>
            {
                entity.ToTable("TransactionRecordTag");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TransactionRecordTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TransactionRecord)
                    .WithMany(p => p.TransactionRecordTags)
                    .HasForeignKey(d => d.TransactionRecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TransactionTag>(entity =>
            {
                entity.ToTable("TransactionTag");

                entity.HasIndex(e => e.Name, "IX_TransactionTag_Name")
                    .IsUnique();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "IX_User_Email")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.Phone).IsRequired();
            });

            modelBuilder.Entity<VCryptoInvestment>(entity =>
            {
                entity.HasNoKey();

                modelBuilder.Entity<VCryptoInvestment>().Property(e => e.PurchasePrice).HasConversion<double>();
                modelBuilder.Entity<VCryptoInvestment>().Property(e => e.Quantity).HasConversion<double>();
                modelBuilder.Entity<VCryptoInvestment>().Property(e => e.MarketValue).HasConversion<double>();
                modelBuilder.Entity<VCryptoInvestment>().Property(e => e.SellValue).HasConversion<double>();
                modelBuilder.Entity<VCryptoInvestment>().Property(e => e.SellPrice).HasConversion<double>();
                modelBuilder.Entity<VCryptoInvestment>().Property(e => e.PurchaseValue).HasConversion<double>();
                modelBuilder.Entity<VCryptoInvestment>().Property(e => e.NetGain).HasConversion<double>();

                entity.ToView("vCryptoInvestment");
            });

            modelBuilder.Entity<VLatestCryptoCoinPrice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vLatestCryptoCoinPrice");
            });

            modelBuilder.Entity<VCryptoAnnualInvestmentSummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vCryptoAnnualInvestmentSummary");
            });

            modelBuilder.Entity<YearlyPlannedExpense>(entity =>
            {
                entity.ToTable("YearlyPlannedExpense");

                entity.HasIndex(e => new { e.Date, e.CategoryId }, "IX_YearlyPlannedExpense_Date_CategoryId")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Date).IsRequired();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.YearlyPlannedExpenses)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
