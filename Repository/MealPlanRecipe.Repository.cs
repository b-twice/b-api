using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Repository
{

    public class MealPlanRecipeRepository
    {
        private readonly AppDbContext _context;

        public MealPlanRecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        public MealPlanRecipe Find(long id) 
        {
            return Include(_context.MealPlanRecipes.AsNoTracking()).First(b => b.Id == id);
        }

        
        public IQueryable<MealPlanRecipe> FindAll() 
        {
            return Include(_context.MealPlanRecipes).AsNoTracking();
        }

        public IQueryable<MealPlanRecipe> Include(IQueryable<MealPlanRecipe> mealPlanRecipes) 
        {
            return mealPlanRecipes.Include(b => b.MealPlan).Include(b => b.Recipe);
        }



        public IQueryable<MealPlanRecipe> Order(IQueryable<MealPlanRecipe> items, string sortName) 
        {
            items = sortName switch 
            {
                "id_asc" => items.OrderBy(b => b.Id),
                "id_desc" => items.OrderByDescending(b => b.Id),
                "mealPlanId_asc" => items.OrderBy(b => b.MealPlan.Name),
                "mealPlanId_desc" => items.OrderByDescending(b => b.MealPlan.Name),
                "recipeId_asc" => items.OrderBy(b => b.Recipe.Name),
                "recipeId_desc" => items.OrderByDescending(b => b.Recipe.Name),
                "count_asc" => items.OrderBy(b => b.Count),
                "count_desc" => items.OrderByDescending(b => b.Count),
                _ => items
            };
            return items;

        }
 
        public IQueryable<MealPlanRecipe> Filter(IQueryable<MealPlanRecipe> mealPlanRecipes, List<long> mealPlans, List<long> recipes)
        {
            if (mealPlans?.Any() == true) {
                mealPlanRecipes = mealPlanRecipes.Where(b => mealPlans.Contains(b.MealPlan.Id));
            }
            if (recipes?.Any() == true) {
                mealPlanRecipes = mealPlanRecipes.Where(b => recipes.Contains(b.Recipe.Id));
            }
            return mealPlanRecipes;
        }

  }
}
