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

    [Route("v1/food/mealplans")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MealPlanController: AppControllerBase
    {
        private readonly MealPlanRepository _mealPlanRepository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public MealPlanController(AppDbContext context, ILogger<MealPlanController> logger, MealPlanRepository mealPlanRepository): base(context, logger)
        {
            _mealPlanRepository = mealPlanRepository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<MealPlan>> GetPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]string name,
            [FromQuery]List<long> users,
            [FromQuery]List<long> recipes,
            [FromQuery]List<string> years,
            [FromQuery]List<string> months
        ) 
        {
            var mealPlans = _mealPlanRepository.Filter(_context.MealPlans, users, recipes, name, years, months);
            mealPlans = _mealPlanRepository.Include(_mealPlanRepository.Order(mealPlans, sortName));
            var paginatedList = PaginatedList<MealPlan>.Create(mealPlans, pageNumber, pageSize);
            return Ok(new PaginatedResult<MealPlan>(paginatedList, paginatedList.TotalCount));
        }


        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<MealPlan>> GetAll(
            [FromQuery]int size 
        ) 
        {
            var mealPlans =_mealPlanRepository.FindAll().OrderByDescending(b => b.Name);
            if (size > 0) {
                return Ok(mealPlans.Take(size));
            }
            return Ok(mealPlans);
        }


        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<MealPlan> Get(long id)
        {
            return Ok(_mealPlanRepository.Find(id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult<MealPlan> Create([FromBody] MealPlan item)
        {
            return Create<MealPlan>(item, nameof(Create), (long id) => _mealPlanRepository.Find(id));
        }
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(MealPlan))]
        public ActionResult<MealPlan> Update(long id, [FromBody] MealPlan item)
        {
            return Update<MealPlan>(id, item, (long id) => _mealPlanRepository.Find(id));
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<MealPlan>(id);

        }

   }
}
