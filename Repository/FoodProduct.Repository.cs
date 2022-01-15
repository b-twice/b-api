using System.Linq;
using Microsoft.EntityFrameworkCore;
using B.API.Models;
using System.Collections.Generic;

namespace B.API.Repository
{

    public class FoodProductRepository
    {
        private readonly AppDbContext _context;

        public FoodProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public FoodProduct Find(long id) 
        {
            return Include(_context.FoodProducts.AsNoTracking()).First(b => b.Id == id);
        }

        
        public IQueryable<FoodProduct> FindAll() 
        {
            return Include(_context.FoodProducts).AsNoTracking();
        }

        public IQueryable<FoodProduct> Include(IQueryable<FoodProduct> foodProducts) 
        {
            return foodProducts.Include(b => b.FoodCategory).Include(b => b.Supermarket).Include(b => b.FoodQuantityType).Include(b => b.FoodUnit);
        }



        public IQueryable<FoodProduct> Order(IQueryable<FoodProduct> items, string sortName) 
        {
            items = sortName switch
            {
                "id_asc" => items.OrderBy(b => b.Id),
                "id_desc" => items.OrderByDescending(b => b.Id),
                "name_asc" => items.OrderBy(b => b.Name),
                "name_desc" => items.OrderByDescending(b => b.Name),
                "foodUnitId_asc" => items.OrderBy(b => b.FoodUnit.Name),
                "foodUnitId_desc" => items.OrderByDescending(b => b.FoodUnit.Name),
                "dirty_asc" => items.OrderBy(b => b.Dirty),
                "dirty_desc" => items.OrderByDescending(b => b.Dirty),
                "supermarketId_asc" => items.OrderBy(b => b.Supermarket.Name),
                "supermarketId_desc" => items.OrderByDescending(b => b.Supermarket.Name),
                "measurement_asc" => items.OrderBy(b => b.Measurement),
                "measurement_desc" => items.OrderByDescending(b => b.Measurement),
                "foodQuantityTypeId_asc" => items.OrderBy(b => b.FoodQuantityType.Name),
                "foodQuantityTypeId_desc" => items.OrderByDescending(b => b.FoodQuantityType.Name),
              _ => items
            };
            return items;
        }
 
        public IQueryable<FoodProduct> Filter(IQueryable<FoodProduct> foodProducts, string foodProductName, List<long> categories, List<long> supermarkets)
        {
            if (!string.IsNullOrEmpty(foodProductName)) {
                foodProducts = foodProducts.Where(b => b.Name.ToLower().Contains(foodProductName.ToLower()));
            }
            if (categories?.Any() == true) {
                foodProducts = foodProducts.Where(b => categories.Contains(b.FoodCategory.Id));
            }
            if (supermarkets?.Any() == true) {
                foodProducts = foodProducts.Where(b => supermarkets.Contains(b.Supermarket.Id));
            }
            return foodProducts;
        }

  }
}
