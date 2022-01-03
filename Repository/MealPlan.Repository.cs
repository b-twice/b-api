using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Database
{

    public class MealPlanRepository
    {
        private readonly AppDbContext _context;

        public MealPlanRepository(AppDbContext context)
        {
            _context = context;
        }

        public MealPlan Find(long id) 
        {
            return Include(_context.MealPlans.AsNoTracking()).First(b => b.Id == id);
        }

        
        public IQueryable<MealPlan> FindAll() 
        {
            return Include(_context.MealPlans).AsNoTracking();
        }

        public IQueryable<MealPlan> Include(IQueryable<MealPlan> mealPlans) 
        {
            return mealPlans.Include(b => b.User)
            .Include(b => b.MealPlanRecipes).ThenInclude(r => r.Recipe);
        }



        public IQueryable<MealPlan> Order(IQueryable<MealPlan> mealPlans, string sortName) 
        {
            switch(sortName) {
                case "id_asc":
                    mealPlans = mealPlans.OrderBy(b => b.Id);
                    break;
                case "id_desc":
                    mealPlans = mealPlans.OrderByDescending(b => b.Id);
                    break;
                case "user_asc":
                    mealPlans = mealPlans.OrderBy(b => b.User.FirstName);
                    break;
                case "user_desc":
                    mealPlans = mealPlans.OrderByDescending(b => b.User.FirstName);
                    break;
                case "days_asc":
                    mealPlans = mealPlans.OrderBy(b => b.Days);
                    break;
                case "days_desc":
                    mealPlans = mealPlans.OrderByDescending(b => b.Days);
                    break;
                case "name_asc":
                    mealPlans = mealPlans.OrderBy(b => b.Name);
                    break;
                case "name_desc":
                    mealPlans = mealPlans.OrderByDescending(b => b.Name);
                    break;
                case "notes_asc":
                    mealPlans = mealPlans.OrderBy(b => b.Notes);
                    break;
                case "notes_desc":
                    mealPlans = mealPlans.OrderByDescending(b => b.Notes);
                    break;
             default:
                    break;
            }
            return mealPlans;
        }
 
        public IQueryable<MealPlan> Filter(IQueryable<MealPlan> mealPlans, List<long> users, List<long> recipes, string name)
        {
            if (users?.Any() == true) {
                mealPlans = mealPlans.Where(b => users.Contains(b.User.Id));
            }
            if (recipes?.Any() == true) {
                mealPlans = mealPlans.Where(b => b.MealPlanRecipes.Any(r => recipes.Any(t => t == r.RecipeId)));
            }
            if (!string.IsNullOrEmpty(name)) {
                mealPlans = mealPlans.Where(b => b.Name.ToLower().Contains(name.ToLower()));
            }
            return mealPlans;
        }

  }
}
