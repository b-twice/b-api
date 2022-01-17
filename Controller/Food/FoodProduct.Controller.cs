using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using B.API.Entity;

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
            var x = 1;
            var foodProducts =_foodProductRepository.FindAll().OrderBy(b => b.Name);
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
            return Create<FoodProduct>(item, nameof(Create), (long id) => _foodProductRepository.Find(id));
        }
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(FoodProduct))]
        public ActionResult<FoodProduct> Update(long id, [FromBody] FoodProduct item)
        {

            return Update<FoodProduct>(id, item, (long id) => _foodProductRepository.Find(id));
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<FoodProduct>(id);

        }

   }
}
