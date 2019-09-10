using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Budget.API.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Budget.API 
{

    [Authorize]
    [Route("api/core")]
    public class CoreController: ControllerBase
    {
        private readonly DatabaseContext _context;

        public CoreController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("fiscal-years")]
        public IActionResult GetFiscalYears()
        {
            return Ok(_context.FiscalYears.ToList());
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return Ok(_context.Users.ToList());
        }
    }
}
