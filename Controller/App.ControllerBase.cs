using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace B.API 
{

   public class AppControllerBase: ControllerBase
    {
        private readonly DbContext  _context;

        private readonly ILogger _logger;
        public AppControllerBase(DbContext context, ILogger<AppControllerBase> logger)
        {
            _context = context;
            _logger = logger;
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
        protected ActionResult<T> Create<T>(T item, string actionName, Func<long, T> fetch) where T : class {
           try {
                _context.Set<T>().Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex){
                _logger.LogWarning(LoggingEvents.InsertItemApplicationError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var itemId = (long)item.GetType().GetProperty("Id").GetValue(item, null);
            return CreatedAtAction(actionName, new { id = itemId}, fetch(itemId));
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        protected ActionResult<T> Update<T>(long id, T item, Func<long, T> fetch) where T : class {
            var itemId = (long)item.GetType().GetProperty("Id").GetValue(item, null);
            if (item == null || itemId != id) {
                _logger.LogWarning(LoggingEvents.UpdateItemBadRequest, $"UPDATE({id}) BAD REQUEST");
                return BadRequest();
            }
            var evt = _context.Set<T>().Find(id);
            if (evt == null)
            {
                _logger.LogWarning(LoggingEvents.UpdateItemNotFound, $"Update(id) NOT FOUND");
                return NotFound();
            }
            try {
                _context.Entry(evt).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
            catch (Exception ex){
                _logger.LogError(LoggingEvents.UpdateItemApplicationError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(fetch(itemId));
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        protected IActionResult Delete<T>(long id) where T : class {
            var evt = _context.Set<T>().Find(id);
            if (evt == null)
            {
                _logger.LogWarning(LoggingEvents.DeleteItemNotFound, $"DELETE(id) NOT FOUND");
                return NotFound();
            }
            try {
                _context.Remove(evt);
                _context.SaveChanges();
            }
            catch (Exception ex){
                _logger.LogError(LoggingEvents.DeleteItemApplicationError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status200OK);
        }

    }
}
