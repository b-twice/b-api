using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace B.API.Controller
{

    [Route("v1/finance/banks")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BankController: AppControllerBase
    {
        private readonly LookupRepository _lookupRepository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public BankController(AppDbContext context, ILogger<BankController> logger,  LookupRepository lookupRepository): base(context, logger)
        {
            _lookupRepository = lookupRepository;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<Bank>> GetBanks()
        {
            return Ok(_context.Banks.AsNoTracking().OrderBy(c => c.Name));
        }

        [Authorize]
        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<Bank>> GetBanksPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]string name
        ) 
        {
            var items = _lookupRepository.OrderBy<Bank>(_lookupRepository.Filter<Bank>(_context.Banks.AsNoTracking(), name), sortName);
            return Ok(_lookupRepository.Paginate(items, pageNumber, pageSize));
        }
        [Authorize]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<Bank> GetBank(int id)
        {
            return Ok(_context.Banks.AsNoTracking().First(o => o.Id == id));
        }
        [Authorize]
        [HttpPost]
        public ActionResult<Bank> CreateBank([FromBody] Bank item)
        {
            return Create<Bank>(item, nameof(CreateBank));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateBank(int id, [FromBody] Bank item)
        {
            return Update<Bank>(id, item);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteBank (int id)
        {
            return Delete<Bank>(id);
        }



    }

}