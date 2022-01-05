using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace B.API.Controller
{

    [Route("v1/food/mealPlanNotes")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MealPlanNoteController: AppControllerBase
    {
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public MealPlanNoteController(AppDbContext context, ILogger<MealPlanNoteController> logger): base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<MealPlanNote> Create([FromBody] MealPlanNote item)
        {
            // Without this EF Core will not bind the FK to these entities
            _context.Entry(item.MealPlan).State = EntityState.Unchanged;
            return Create<MealPlanNote>(item, nameof(Create));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] MealPlanNote item)
        {
            item.MealPlanId = item?.MealPlan?.Id ?? default(int);
            return Update<MealPlanNote>(id, item);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<MealPlanNote>(id);

        }

   }
}
