using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Repository
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
            return mealPlans
                .Include(b => b.User)
                .Include(b => b.MealPlanRecipes.OrderBy(mr => mr.Recipe.Name))
                    .ThenInclude(r => r.Recipe)
                .Include(b => b.MealPlanNotes);
        }



        public IQueryable<MealPlan> Order(IQueryable<MealPlan> items, string sortName) 
        {
            items = sortName switch 
            {
                "id_asc" => items.OrderBy(b => b.Id),
                "id_desc" => items.OrderByDescending(b => b.Id),
                "userId_asc" => items.OrderBy(b => b.User.FirstName),
                "userId_desc" => items.OrderByDescending(b => b.User.FirstName),
                "name_asc" => items.OrderBy(b => b.Name),
                "name_desc" => items.OrderByDescending(b => b.Name),
                "date_asc" => items.OrderBy(o => o.Date),
                "date_desc" => items.OrderByDescending(o => o.Date),
                _ => items
            };
            return items;
        }
 
        public IQueryable<MealPlan> Filter(IQueryable<MealPlan> mealPlans, List<long> users, List<long> recipes, string name, List<string> years, List<string> months)
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
            if (years?.Any() == true) {
                mealPlans = mealPlans.Where(b => years.Contains(b.Date.Substring(0,4)));
            }
            if (months?.Any() == true) {
                mealPlans = mealPlans.Where(b => months.Contains(b.Date.Substring(0,4)));
            }
            return mealPlans;
        }

  }
}
