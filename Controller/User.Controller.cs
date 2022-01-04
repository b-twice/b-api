using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace B.API.Controller
{

    [Authorize]
    [Route("v1/admin/users")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UserController: AppControllerBase
    {
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public UserController(AppDbContext context, ILogger<UserController> logger): base(context, logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<User>> GetAll() 
        {
            return Ok(_context.Users);
        }


        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<TransactionRecord> Get(long id)
        {
            return Ok(_context.Users.First(o => o.Id == id));
        }



   }
}