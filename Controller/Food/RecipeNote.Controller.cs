using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace B.API.Controller
{

    [Route("v1/food/recipeNotes")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class RecipeNoteController: AppControllerBase
    {
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public RecipeNoteController(AppDbContext context, ILogger<RecipeNoteController> logger): base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<RecipeNote> Create([FromBody] RecipeNote item)
        {
            // Without this EF Core will not bind the FK to these entities
            _context.Entry(item.Recipe).State = EntityState.Unchanged;
            return Create<RecipeNote>(item, nameof(Create));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] RecipeNote item)
        {
            item.RecipeId = item?.Recipe?.Id ?? default(int);
            return Update<RecipeNote>(id, item);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<RecipeNote>(id);

        }

   }
}
