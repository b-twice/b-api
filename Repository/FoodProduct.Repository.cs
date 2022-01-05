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



        public IQueryable<FoodProduct> Order(IQueryable<FoodProduct> foodProducts, string sortName) 
        {
            // TODO: tranlsate this to a generic method using IQueryable? 
            // May not be worth it, this is clear and conscise as is
            switch(sortName) {
                case "id_asc":
                    foodProducts = foodProducts.OrderBy(b => b.Id);
                    break;
                case "id_desc":
                    foodProducts = foodProducts.OrderByDescending(b => b.Id);
                    break;
                case "name_asc":
                    foodProducts = foodProducts.OrderBy(b => b.Name);
                    break;
                case "name_desc":
                    foodProducts = foodProducts.OrderByDescending(b => b.Name);
                    break;
                case "foodUnit_asc":
                    foodProducts = foodProducts.OrderBy(b => b.FoodUnit.Name);
                    break;
                case "foodUnit_desc":
                    foodProducts = foodProducts.OrderByDescending(b => b.FoodUnit.Name);
                    break;
                case "dirty_asc":
                    foodProducts = foodProducts.OrderBy(b => b.Dirty);
                    break;
                case "dirty_desc":
                    foodProducts = foodProducts.OrderByDescending(b => b.Dirty);
                    break;
                case "supermarket_asc":
                    foodProducts = foodProducts.OrderBy(b => b.Supermarket.Name);
                    break;
                case "supermarket_desc":
                    foodProducts = foodProducts.OrderByDescending(b => b.Supermarket.Name);
                    break;
                case "measurement_asc":
                    foodProducts = foodProducts.OrderBy(b => b.Measurement);
                    break;
                case "measurement_desc":
                    foodProducts = foodProducts.OrderByDescending(b => b.Measurement);
                    break;
                case "foodQuantityType_asc":
                    foodProducts = foodProducts.OrderBy(b => b.FoodQuantityType.Name);
                    break;
                case "foodQuantityType_desc":
                    foodProducts = foodProducts.OrderByDescending(b => b.FoodQuantityType.Name);
                    break;
                default:
                    break;
            }
            return foodProducts;
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
