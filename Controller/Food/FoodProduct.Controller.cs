using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace B.API.Controller
{

    [Route("v1/food/products")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FoodProductController: AppControllerBase
    {
        private readonly FoodProductRepository _foodProductRepository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public FoodProductController(AppDbContext context, ILogger<FoodProductController> logger, FoodProductRepository foodProductRepository): base(context, logger)
        {
            _foodProductRepository = foodProductRepository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<FoodProduct>> GetPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]string foodProductName,
            [FromQuery]List<long> categories,
            [FromQuery]List<long> supermarkets
        ) 
        {
            var foodProducts = _foodProductRepository.Filter(_context.FoodProducts, foodProductName, categories, supermarkets);
            foodProducts = _foodProductRepository.Include(_foodProductRepository.Order(foodProducts, sortName));
            var paginatedList = PaginatedList<FoodProduct>.Create(foodProducts, pageNumber, pageSize);
            return Ok(new PaginatedResult<FoodProduct>(paginatedList, paginatedList.TotalCount));
        }


        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<FoodProduct>> GetAll(
            [FromQuery]int size 
        ) 
        {
            var foodProducts =_foodProductRepository.FindAll().OrderByDescending(b => b.Name);
            if (size > 0) {
                return Ok(foodProducts.Take(size));
            }
            return Ok(foodProducts);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<FoodProduct> Get(long id)
        {
            return Ok(_foodProductRepository.Find(id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult<FoodProduct> Create([FromBody] FoodProduct item)
        {
            // Without this EF Core will not bind the FK to these entities
            _context.Entry(item.FoodCategory).State = EntityState.Unchanged;
            _context.Entry(item.FoodQuantityType).State = EntityState.Unchanged;
            _context.Entry(item.FoodUnit).State = EntityState.Unchanged;
            _context.Entry(item.Supermarket).State = EntityState.Unchanged;
 
            return Create<FoodProduct>(item, nameof(Create));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] FoodProduct item)
        {
            item.FoodCategoryId = item?.FoodCategory?.Id ?? default(int);
            item.FoodQuantityTypeId = item?.FoodQuantityType?.Id ?? default(int);
            item.FoodUnitId = item?.FoodUnit?.Id ?? default(int);
            item.SupermarketId = item?.Supermarket?.Id ?? null;
 
            return Update<FoodProduct>(id, item);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<FoodProduct>(id);

        }

   }
}
