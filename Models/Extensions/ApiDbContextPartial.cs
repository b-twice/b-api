using B.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace B.API.Models {
    public partial class AppDbContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            // prevent self referential loops
            // modelBuilder.Entity<PostGroup>().Ignore(c => c.Post);
            modelBuilder.Entity<BookAuthor>().Ignore(c => c.Books);
            modelBuilder.Entity<BookStatus>().Ignore(c => c.Books);
            modelBuilder.Entity<BookCategory>().Ignore(c => c.Books);
            modelBuilder.Entity<User>().Ignore(c => c.TransactionRecords);
            modelBuilder.Entity<User>().Ignore(c => c.Recipes);
            modelBuilder.Entity<User>().Ignore(c => c.MealPlans);
            modelBuilder.Entity<Bank>().Ignore(c => c.TransactionRecords);
            modelBuilder.Entity<TransactionCategory>().Ignore(c => c.TransactionRecords);
            modelBuilder.Entity<TransactionRecordTag>().Ignore(c => c.TransactionRecord);
            modelBuilder.Entity<TransactionTag>().Ignore(c => c.TransactionRecordTags);
            modelBuilder.Entity<Supermarket>().Ignore(c => c.FoodProducts);
            modelBuilder.Entity<Cookbook>().Ignore(c => c.Recipes);
            modelBuilder.Entity<CookbookAuthor>().Ignore(c => c.Cookbooks);
            modelBuilder.Entity<FoodQuantityType>().Ignore(c => c.FoodProducts);
            modelBuilder.Entity<FoodUnit>().Ignore(c => c.FoodProducts);
            modelBuilder.Entity<Recipe>().Ignore(c => c.MealPlanRecipes);
            modelBuilder.Entity<FoodCategory>().Ignore(c => c.FoodProducts);
            modelBuilder.Entity<RecipeIngredient>().Ignore(c => c.Recipe);


            modelBuilder.Entity<TransactionRecord>().Property(t => t.Amount).HasColumnType("decimal");
            modelBuilder.Entity<RecipeIngredient>().Property(t => t.Weight).HasColumnType("decimal").HasConversion<decimal>();
            modelBuilder.Entity<RecipeIngredient>().Property(t => t.Count).HasColumnType("decimal").HasConversion<decimal>();
            // modelBuilder.Entity<RecipeIngredient>().Property(t => t.Weight)
            // modelBuilder.Entity<RecipeIngredient>().Property(t => t.Count).HasConversion<double>();

        }
    }
}
