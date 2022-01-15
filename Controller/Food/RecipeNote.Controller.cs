using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            return Create<RecipeNote>(item, nameof(Create), (long id) => _context.RecipeNotes.AsNoTracking().First(o => o.Id == id));
        }
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(RecipeNote))]
       public ActionResult<RecipeNote> Update(long id, [FromBody] RecipeNote item)
        {
            return Update<RecipeNote>(id, item, (long id) => _context.RecipeNotes.AsNoTracking().First(o => o.Id == id));
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<RecipeNote>(id);

        }

   }
}
