using System;
using System.Collections.Generic;
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

        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<BookStatus> BookStatuses { get; set; }
        public virtual DbSet<Cookbook> Cookbooks { get; set; }
        public virtual DbSet<CookbookAuthor> CookbookAuthors { get; set; }
        public virtual DbSet<CryptoAnnualInvestmentSummary> CryptoAnnualInvestmentSummaries { get; set; }
        public virtual DbSet<CryptoCoin> CryptoCoins { get; set; }
        public virtual DbSet<CryptoHolding> CryptoHoldings { get; set; }
        public virtual DbSet<CryptoInvestment> CryptoInvestments { get; set; }
        public virtual DbSet<CryptoPrice> CryptoPrices { get; set; }
        public virtual DbSet<CryptoSale> CryptoSales { get; set; }
        public virtual DbSet<Debt> Debts { get; set; }
        public virtual DbSet<Earning> Earnings { get; set; }
        public virtual DbSet<FoodCategory> FoodCategories { get; set; }
        public virtual DbSet<FoodProduct> FoodProducts { get; set; }
        public virtual DbSet<FoodQuantityType> FoodQuantityTypes { get; set; }
        public virtual DbSet<FoodUnit> FoodUnits { get; set; }
        public virtual DbSet<Investment> Investments { get; set; }
        public virtual DbSet<LatestCryptoCoinPrice> LatestCryptoCoinPrices { get; set; }
        public virtual DbSet<MealPlan> MealPlans { get; set; }
        public virtual DbSet<MealPlanGrocery> MealPlanGroceries { get; set; }
        public virtual DbSet<MealPlanNote> MealPlanNotes { get; set; }
        public virtual DbSet<MealPlanRecipe> MealPlanRecipes { get; set; }
        public virtual DbSet<MealPlanRecipesView> MealPlanRecipesViews { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostGroup> PostGroups { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeCategory> RecipeCategories { get; set; }
        public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual DbSet<RecipeIngredientsView> RecipeIngredientsViews { get; set; }
        public virtual DbSet<RecipeNote> RecipeNotes { get; set; }
        public virtual DbSet<Supermarket> Supermarkets { get; set; }
        public virtual DbSet<TransactionCategory> TransactionCategories { get; set; }
        public virtual DbSet<TransactionRecord> TransactionRecords { get; set; }
        public virtual DbSet<TransactionRecordTag> TransactionRecordTags { get; set; }
        public virtual DbSet<TransactionTag> TransactionTags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<YearlyPlannedExpense> YearlyPlannedExpenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=../b-database/app.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

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
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<BookStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Cookbook>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CookbookAuthor)
                    .WithMany(p => p.Cookbooks)
                    .HasForeignKey(d => d.CookbookAuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CookbookAuthor>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<CryptoAnnualInvestmentSummary>(entity =>
            {
                entity.ToView("CryptoAnnualInvestmentSummary");
            });

            modelBuilder.Entity<CryptoCoin>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<CryptoHolding>(entity =>
            {
                entity.HasOne(d => d.CryptoCoin)
                    .WithMany(p => p.CryptoHoldings)
                    .HasForeignKey(d => d.CryptoCoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CryptoInvestment>(entity =>
            {
                entity.ToView("CryptoInvestment");
            });

            modelBuilder.Entity<CryptoPrice>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CryptoCoin)
                    .WithMany(p => p.CryptoPrices)
                    .HasForeignKey(d => d.CryptoCoinId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CryptoSale>(entity =>
            {
                entity.HasOne(d => d.CryptoHolding)
                    .WithMany(p => p.CryptoSales)
                    .HasForeignKey(d => d.CryptoHoldingId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Debt>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Earning>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<FoodCategory>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<FoodProduct>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.FoodCategory)
                    .WithMany(p => p.FoodProducts)
                    .HasForeignKey(d => d.FoodCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.FoodQuantityType)
                    .WithMany(p => p.FoodProducts)
                    .HasForeignKey(d => d.FoodQuantityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.FoodUnit)
                    .WithMany(p => p.FoodProducts)
                    .HasForeignKey(d => d.FoodUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FoodQuantityType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<FoodUnit>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Investment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<LatestCryptoCoinPrice>(entity =>
            {
                entity.ToView("LatestCryptoCoinPrice");
            });

            modelBuilder.Entity<MealPlan>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MealPlans)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MealPlanGrocery>(entity =>
            {
                entity.ToView("MealPlanGroceries");
            });

            modelBuilder.Entity<MealPlanNote>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.MealPlan)
                    .WithMany(p => p.MealPlanNotes)
                    .HasForeignKey(d => d.MealPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MealPlanRecipe>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Count).HasDefaultValueSql("1");

                entity.HasOne(d => d.MealPlan)
                    .WithMany(p => p.MealPlanRecipes)
                    .HasForeignKey(d => d.MealPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.MealPlanRecipes)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MealPlanRecipesView>(entity =>
            {
                entity.ToView("MealPlanRecipesView");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasOne(d => d.PostGroup)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.PageNumber).HasDefaultValueSql("0");

                entity.HasOne(d => d.Cookbook)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.CookbookId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.RecipeCategory)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.RecipeCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<RecipeCategory>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<RecipeIngredient>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Count).HasDefaultValueSql("1");

                entity.Property(e => e.Weight).HasDefaultValueSql("0");

                entity.HasOne(d => d.FoodProduct)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.FoodProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<RecipeIngredientsView>(entity =>
            {
                entity.ToView("RecipeIngredientsView");
            });

            modelBuilder.Entity<RecipeNote>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeNotes)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Supermarket>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TransactionRecord>(entity =>
            {
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<YearlyPlannedExpense>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

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
