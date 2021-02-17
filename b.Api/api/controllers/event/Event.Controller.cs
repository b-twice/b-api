using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using b.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using b.Entities.Event;
using Microsoft.Extensions.Logging;
using Budget.Core;
using System.Collections.Generic;

namespace b.Api
{

  // [Authorize]
  [Route("event")]
  [ApiController]
  public class EventController : AppControllerBase
  {
    private readonly EventDatabaseContext _context;
    private readonly ILogger _logger;

    public EventController(EventDatabaseContext context, ILogger<EventController> logger) : base(context, logger)
    {
      _context = context;
      _logger = logger;
    }

    [HttpGet("events")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<IEnumerable<Event>> GetEvents()
    {
      return Ok(_context.Events.Include(o => o.eventUser).ToList());
    }

    [HttpGet("reoccuringTypes")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<IEnumerable<ReoccuringType>> GetReoccuringTypes()
    {
      return Ok(_context.ReoccuringTypes.ToList());
    }


    [HttpGet("events/{id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<Event> GetEvent(int id)
    {
      return Ok(_context.Events.First(o => o.id == id));
    }


    [HttpPost("events")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public ActionResult<Event> CreateEvent(Event item)
    {
      // The user's ID is available in the NameIdentifier claim
      string userAuthId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
      EventUser eventUser = _context.EventUsers.First(o => o.authId == userAuthId);

      if (eventUser == null)
      {
        _logger.LogWarning(LoggingEvents.InsertItemForbidError, $"Insert FORBIDDEN");
        return Forbid();
      }

      item.eventUser = eventUser;
      return Create<Event>(item, nameof(CreateEvent));
    }

    [HttpPut("events/{id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public IActionResult UpdateEvent(int id, Event item)
    {
      return Update<Event>(id, item);
    }

    [HttpDelete("events/{id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public IActionResult DeleteEvent(int id)
    {
      return Delete<Event>(id);

    }
  }
}
