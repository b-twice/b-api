using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B.API.Models.Core;
using B.API.Models.Finance;
using B.API.Models.Food;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace B.API.Entities
{
    // DB Context gets registered in configure services
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }

        // CORE
        public DbSet<FiscalYear> FiscalYears{ get; set; }
        public DbSet<User> Users { get; set; }

        // FINANCES
        public DbSet<TransactionCategory> TransactionCategories{ get; set; }
        public DbSet<ExpenseMonth> ExpenseMonths{ get; set; }
        public DbSet<UserSummary> UserSummaries { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserCategoryGrowth> UserCategoriesGrowth { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionMonthly> TransactionsMonthly { get; set; }
        public DbSet<TransactionsPerDay> TransactionsPerDay { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        // FOOD
        public DbSet<Cookbook> Cookbooks { get; set; }
        public DbSet<AnnualFoodProduct> AnnualFoodProducts { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<FoodCategory> FoodCategories{ get; set; }
        public DbSet<FoodProduct> FoodProducts { get; set; }
        public DbSet<Grocery> Groceries { get; set; }
        public DbSet<GroceryMonthly> GroceriesMonthly { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<MealPlanRecipe> MealPlanRecipes { get; set; }
        public DbSet<MealPlanGrocery> MealPlanGroceries { get; set; }
        public DbSet<Supermarket> Supermarkets { get; set; }
        public DbSet<RecipeCategory> RecipeCategories{ get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            coreModelCreating(modelBuilder);
            financeModelCreating(modelBuilder);
            foodModelCreating(modelBuilder);

        }

        private void coreModelCreating(ModelBuilder modelBuilder) {
            // USER
            modelBuilder.Entity<User>(t =>
            {
                t.HasKey(o => o.id);
            });
            modelBuilder.Entity<User>().ToTable("Users");

            // FISCALYEARS
            modelBuilder.Entity<FiscalYear>(t =>
            {
                t.HasKey(o => o.id);
            });
            modelBuilder.Entity<FiscalYear>().ToTable("FiscalYears");
        }

        private void financeModelCreating(ModelBuilder modelBuilder) {
            // CATEGORIES
            modelBuilder.Entity<TransactionCategory>(t =>
            {
                t.HasKey(o => o.id);
            });
            modelBuilder.Entity<TransactionCategory>()
                .ToTable("TransactionCategories");


            // EXPENSE MONTHS
            modelBuilder.Entity<ExpenseMonth>(t =>
            {
                t.HasKey(o => o.id);
            });
            modelBuilder.Entity<ExpenseMonth>()
                .ToTable("ExpenseMonths");
 
 
            // USER PROFILES
            modelBuilder.Entity<UserProfile>(t =>
            {
                t.HasKey(o => o.name);
            });
            modelBuilder.Entity<UserProfile>()
                .ToTable("UserProfiles");

            // USER SUMMARIES
            modelBuilder.Entity<UserSummary>(t =>
            {
                t.HasKey(o => new {o.fiscalYear, o.userName});
                t.Property(o => o.fiscalYear).HasColumnName("fiscal_year");
                t.Property(o => o.userName).HasColumnName("user_name");
                t.Property(o => o.incomeGrowth).HasColumnName("income_growth");
                t.Property(o => o.incomeTaxable).HasColumnName("income_taxable");
                t.Property(o => o.incomeTaxableGrowth).HasColumnName("income_taxable_growth");
                t.Property(o => o.takeHomePay).HasColumnName("take_home_pay");
                t.Property(o => o.takeHomePayGrowth).HasColumnName("take_home_pay_growth");
                t.Property(o => o.spentGrowth).HasColumnName("spent_growth");
                t.Property(o => o.savedGrowth).HasColumnName("saved_growth");
                t.Property(o => o.retirementContribution).HasColumnName("retirement_contribution");
                t.Property(o => o.retirementContributionGrowth).HasColumnName("retirement_contribution_growth");
                t.Property(o => o.stockContribution).HasColumnName("stock_contribution");
                t.Property(o => o.stockContributionGrowth).HasColumnName("stock_contribution_growth");
                t.Property(o => o.stockGrowth).HasColumnName("stock_growth");
                t.Property(o => o.retirementGrowth).HasColumnName("retirement_growth");
                t.Property(o => o.savingGrowth).HasColumnName("saving_growth");
                t.Property(o => o.taxedGrowth).HasColumnName("taxed_growth");
                t.Property(o => o.debtGrowth).HasColumnName("debt_growth");
            });
            modelBuilder.Entity<UserSummary>()
                .ToTable("UserSummaries");

            // USER PROFILES
            modelBuilder.Entity<UserCategoryGrowth>(t =>
            {
                t.HasKey(o => new {o.fiscalYear, o.userName, o.categoryName});
                t.Property(o => o.fiscalYear).HasColumnName("fiscal_year");
                t.Property(o => o.userName).HasColumnName("user_name");
                t.Property(o => o.categoryGroupName).HasColumnName("category_group_name");
                t.Property(o => o.categoryName).HasColumnName("category_name");
            });
            modelBuilder.Entity<UserCategoryGrowth>()
                .ToTable("UserCategoryGrowth");

            // USER TRANSACTIONS
            modelBuilder.Entity<Transaction>(t =>
            {
                t.HasKey(o => o.id).HasName("transaction_id");
                t.Property(o => o.id).HasColumnName("transaction_id");
                t.Property(o => o.userName).HasColumnName("user_name");
                t.Property(o => o.bankName).HasColumnName("bank_name");
                t.Property(o => o.categoryGroupName).HasColumnName("category_group_name");
                t.Property(o => o.categoryName).HasColumnName("category_name");
            });
            modelBuilder.Entity<Transaction>()
                .ToTable("TransactionsView");
                
            // USER TRANSACTIONS MONTHLY
            modelBuilder.Entity<TransactionMonthly>(t =>
            {
                t.HasKey(o => new {o.user, o.year, o.month, o.category});
            });
            modelBuilder.Entity<TransactionMonthly>()
                .ToTable("TransactionsMonthlyView");

            // Transactions Per Day
            modelBuilder.Entity<TransactionsPerDay>(t =>
            {
                t.HasKey(o => new {o.transactionCount, o.date});
                t.Property(o => o.transactionCount).HasColumnName("transaction_count");
             });
            modelBuilder.Entity<TransactionsPerDay>()
                .ToTable("TransactionsPerDay");




            // USER MONTHLY EXPENSES
            modelBuilder.Entity<Expense>(t =>
            {
                t.HasKey(o => new {o.userName, o.year, o.month, o.categoryName});
                t.Property(o => o.userName).HasColumnName("user_name");
                t.Property(o => o.categoryName).HasColumnName("category_name");
                t.Property(o => o.plannedExpense).HasColumnName("planned_expense");
                t.Property(o => o.actualExpense).HasColumnName("actual_expense");
            });
            modelBuilder.Entity<Expense>()
                .ToTable("Expenses");

        }

        private void foodModelCreating(ModelBuilder modelBuilder) {
            // FOOD PRODUCTS 
            modelBuilder.Entity<FoodProduct>(t =>
            {
                t.HasKey(o => o.id);
                t.Property(o => o.foodUnitId).HasColumnName("food_unit_id");
                t.Property(o => o.quantityTypeId).HasColumnName("food_quantity_type_id");
            });
            modelBuilder.Entity<FoodProduct>().ToTable("FoodProducts");

            // FOOD CATEGORIES
            modelBuilder.Entity<FoodCategory>(t =>
            {
                t.HasKey(o => o.id);
            });
            modelBuilder.Entity<FoodCategory>().ToTable("FoodCategories");

            // RECIPE CATEGORIES
            modelBuilder.Entity<RecipeCategory>(t =>
            {
                t.HasKey(o => o.id);
            });
            modelBuilder.Entity<RecipeCategory>().ToTable("RecipeCategories");

            // SUPERMARKETS
            modelBuilder.Entity<Supermarket>(t =>
            {
                t.HasKey(o => o.id);
            });
            modelBuilder.Entity<Supermarket>().ToTable("Supermarkets");

            // USER GROCERIES
            modelBuilder.Entity<Grocery>(t =>
            {
                t.HasKey(o => o.id);
                t.Property(o => o.unitPrice).HasColumnName("unit_price");
                t.Property(o => o.quantityType).HasColumnName("quantity_type");
            });
            modelBuilder.Entity<Grocery>().ToTable("GroceriesView");

            // USER GROCERIES MONTHLY
            modelBuilder.Entity<GroceryMonthly>(t =>
            {
                t.HasKey(o => new {o.year, o.month, o.category});
            });
            modelBuilder.Entity<GroceryMonthly>().ToTable("GroceriesMonthlyView");

            // MEAL PLANS
            modelBuilder.Entity<MealPlan>(t =>
            {
                t.HasKey(o => o.id);
                t.Property(o => o.costOrganic).HasColumnName("cost_organic");
                t.Property(o => o.costSeasonal).HasColumnName("cost_seasonal");
            });
            modelBuilder.Entity<MealPlan>().ToTable("MealPlansView");

            // MEAL PLAN RECIPES
            modelBuilder.Entity<MealPlanRecipe>(t =>
            {
                t.HasKey(o => o.id);
                t.Property(o => o.recipeId).HasColumnName("recipe_id");
                t.Property(o => o.mealPlanName).HasColumnName("meal_plan_name");
                t.Property(o => o.mealPlanId).HasColumnName("meal_plan_id");
                t.Property(o => o.pageNumber).HasColumnName("page_number");
                t.Property(o => o.servingCost).HasColumnName("serving_cost");
                t.Property(o => o.costOrganic).HasColumnName("cost_organic");
                t.Property(o => o.costSeasonal).HasColumnName("cost_seasonal");
            });
            modelBuilder.Entity<MealPlanRecipe>().ToTable("MealPlanRecipesView");

            // MEAL PLAN RECIPE INGREDIENTS
            modelBuilder.Entity<MealPlanGrocery>(t =>
            {
                t.HasKey(o => new {o.mealPlanName, o.name});
                t.Property(o => o.mealPlanName).HasColumnName("meal_plan_name");
                t.Property(o => o.mealPlanId).HasColumnName("meal_plan_id");
                t.Property(o => o.supermarket).HasColumnName("supermarket");
                t.Property(o => o.supermarketName).HasColumnName("supermarket_name");
            });
            modelBuilder.Entity<MealPlanGrocery>().ToTable("MealPlanGroceries");


            // ANNUAL FOOD PRODUCTS
            modelBuilder.Entity<AnnualFoodProduct>(t =>
            {
                t.HasKey(o => new {o.user, o.category, o.name, o.year, o.unit});
                t.Property(o => o.unitPrice).HasColumnName("unit_price");
            });
            modelBuilder.Entity<AnnualFoodProduct>().ToTable("FoodProductsView");

            // Cookbooks
            modelBuilder.Entity<Cookbook>(t =>
            {
                t.HasKey(o => o.id);
            });
            modelBuilder.Entity<Cookbook>().ToTable("CookbooksView");


            // RECIPES
            modelBuilder.Entity<Recipe>(t =>
            {
                t.HasKey(o => o.id);
                t.Property(o => o.pageNumber).HasColumnName("page_number");
                t.Property(o => o.servingCost).HasColumnName("serving_cost");
                t.Property(o => o.costOrganic).HasColumnName("cost_organic");
                t.Property(o => o.costSeasonal).HasColumnName("cost_seasonal");
            });
            modelBuilder.Entity<Recipe>().ToTable("RecipesView");

            // RECIPE INGREDIENTS
            modelBuilder.Entity<RecipeIngredient>(t =>
            {
                t.HasKey(o => o.id);
                t.Property(o => o.costOrganic).HasColumnName("cost_organic");
                t.Property(o => o.costSeasonal).HasColumnName("cost_seasonal");
            });
            modelBuilder.Entity<RecipeIngredient>().ToTable("RecipeIngredientsView");

        }

   }
}
