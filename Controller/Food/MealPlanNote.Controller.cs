using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            return Create<MealPlanNote>(item, nameof(Create), (long id) => _context.MealPlanNotes.AsNoTracking().First(o => o.Id == id));
        }
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(MealPlanNote))]
        public ActionResult<MealPlanNote> Update(long id, [FromBody] MealPlanNote item)
        {
            return Update<MealPlanNote>(id, item, (long id) => _context.MealPlanNotes.AsNoTracking().First(o => o.Id == id));
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<MealPlanNote>(id);

        }

   }
}
