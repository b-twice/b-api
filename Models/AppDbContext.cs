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
        public virtual DbSet<Debt> Debts { get; set; }
        public virtual DbSet<Earning> Earnings { get; set; }
        public virtual DbSet<FoodCategory> FoodCategories { get; set; }
        public virtual DbSet<FoodProduct> FoodProducts { get; set; }
        public virtual DbSet<FoodQuantityType> FoodQuantityTypes { get; set; }
        public virtual DbSet<FoodUnit> FoodUnits { get; set; }
        public virtual DbSet<Investment> Investments { get; set; }
        public virtual DbSet<MealPlan> MealPlans { get; set; }
        public virtual DbSet<MealPlanGrocery> MealPlanGroceries { get; set; }
        public virtual DbSet<MealPlanRecipe> MealPlanRecipes { get; set; }
        public virtual DbSet<MealPlanRecipesView> MealPlanRecipesViews { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostGroup> PostGroups { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeCategory> RecipeCategories { get; set; }
        public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual DbSet<RecipeIngredientsView> RecipeIngredientsViews { get; set; }
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

            modelBuilder.Entity<Cookbook>(entity =>
            {
                entity.ToTable("Cookbook");

                entity.HasIndex(e => e.Name, "IX_Cookbook_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.CookbookAuthor)
                    .WithMany(p => p.Cookbooks)
                    .HasForeignKey(d => d.CookbookAuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CookbookAuthor>(entity =>
            {
                entity.ToTable("CookbookAuthor");

                entity.HasIndex(e => e.Name, "IX_CookbookAuthor_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
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

            modelBuilder.Entity<FoodCategory>(entity =>
            {
                entity.ToTable("FoodCategory");

                entity.HasIndex(e => e.Name, "IX_FoodCategory_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<FoodProduct>(entity =>
            {
                entity.ToTable("FoodProduct");

                entity.HasIndex(e => e.Name, "IX_FoodProduct_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FoodQuantityTypeId).HasColumnType("INTERGER");

                entity.Property(e => e.Name).IsRequired();

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

                entity.HasOne(d => d.Supermarket)
                    .WithMany(p => p.FoodProducts)
                    .HasForeignKey(d => d.SupermarketId);
            });

            modelBuilder.Entity<FoodQuantityType>(entity =>
            {
                entity.ToTable("FoodQuantityType");

                entity.HasIndex(e => e.Name, "IX_FoodQuantityType_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<FoodUnit>(entity =>
            {
                entity.ToTable("FoodUnit");

                entity.HasIndex(e => e.Name, "IX_FoodUnit_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Investment>(entity =>
            {
                entity.ToTable("Investment");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Year).IsRequired();
            });

            modelBuilder.Entity<MealPlan>(entity =>
            {
                entity.ToTable("MealPlan");

                entity.HasIndex(e => e.Name, "IX_MealPlan_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Days).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MealPlans)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MealPlanGrocery>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("MealPlanGroceries");
            });

            modelBuilder.Entity<MealPlanRecipe>(entity =>
            {
                entity.ToTable("MealPlanRecipe");

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
                entity.HasNoKey();

                entity.ToView("MealPlanRecipesView");
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

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("Recipe");

                entity.HasIndex(e => e.Name, "IX_Recipe_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();

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
                entity.ToTable("RecipeCategory");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<RecipeIngredient>(entity =>
            {
                entity.ToTable("RecipeIngredient");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Cost).HasDefaultValueSql("0");

                entity.Property(e => e.CostOrganic).HasDefaultValueSql("0");

                entity.Property(e => e.CostSeasonal).HasDefaultValueSql("0");

                entity.Property(e => e.Count).HasDefaultValueSql("1");

                entity.Property(e => e.Measurement).IsRequired();

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
                entity.HasNoKey();

                entity.ToView("RecipeIngredientsView");
            });

            modelBuilder.Entity<Supermarket>(entity =>
            {
                entity.ToTable("Supermarket");

                entity.HasIndex(e => e.Name, "IX_Supermarket_Name")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Code).IsRequired();

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
