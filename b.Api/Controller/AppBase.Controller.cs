using System;
using b.Api.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace b.Api.Controller
{
    public class AppBaseController<T> : ControllerBase
    {
        private readonly ILogger _logger;
        public AppBaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        protected IActionResult Delete(long id, Action<long> delete)
        {
            try
            {
                delete(id);
            }
            #pragma warning disable CS0168
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(LoggingEvents.DeleteItemNotFound, $"DELETE({id}) NOT FOUND.");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.DeleteItemApplicationError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status200OK);
        }

    }
}
