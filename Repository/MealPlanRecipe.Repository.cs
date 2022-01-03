using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Database
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



        public IQueryable<MealPlanRecipe> Order(IQueryable<MealPlanRecipe> mealPlanRecipes, string sortName) 
        {
            switch(sortName) {
                case "id_asc":
                    mealPlanRecipes = mealPlanRecipes.OrderBy(b => b.Id);
                    break;
                case "id_desc":
                    mealPlanRecipes = mealPlanRecipes.OrderByDescending(b => b.Id);
                    break;
                case "mealPlan_asc":
                    mealPlanRecipes = mealPlanRecipes.OrderBy(b => b.MealPlan.Name);
                    break;
                case "mealPlan_desc":
                    mealPlanRecipes = mealPlanRecipes.OrderByDescending(b => b.MealPlan.Name);
                    break;
                case "recipe_asc":
                    mealPlanRecipes = mealPlanRecipes.OrderBy(b => b.Recipe.Name);
                    break;
                case "recipe_desc":
                    mealPlanRecipes = mealPlanRecipes.OrderByDescending(b => b.Recipe.Name);
                    break;
                case "count_asc":
                    mealPlanRecipes = mealPlanRecipes.OrderBy(b => b.Count);
                    break;
                case "count_desc":
                    mealPlanRecipes = mealPlanRecipes.OrderByDescending(b => b.Count);
                    break;
            default:
                    break;
            }
            return mealPlanRecipes;
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
