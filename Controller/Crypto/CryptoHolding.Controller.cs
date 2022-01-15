using System.Linq;
using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using B.API.Entity;

namespace B.API.Controller
{

    [Route("v1/crypto/holdings")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CryptoHoldingController: AppControllerBase
    {
        private readonly CryptoHoldingRepository _holdingRepository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public CryptoHoldingController(AppDbContext context, ILogger<CryptoHoldingController> logger, CryptoHoldingRepository holdingRepository): base(context, logger)
        {
            _holdingRepository = holdingRepository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<CryptoHolding>> GetPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize
        ) 
        {
            var holdings = _holdingRepository.Include(_holdingRepository.Order(_context.CryptoHoldings.AsNoTracking(), sortName));
            var paginatedList = PaginatedList<CryptoHolding>.Create(holdings, pageNumber, pageSize);
            return Ok(new PaginatedResult<CryptoHolding>(paginatedList, paginatedList.TotalCount));
        }


        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<CryptoHolding>> GetAll(
            [FromQuery]int size 
        ) 
        {
            var holdings =_holdingRepository.FindAll().OrderByDescending(b => b.PurchaseDate);
            if (size > 0) {
                return Ok(holdings.Take(size));
            }
            return Ok(holdings);
        }

        [HttpGet("available")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<CryptoHolding>> GetAvailable(
            [FromQuery]int size 
        ) 
        {
            var holdings =_holdingRepository.FindAll().Where(o => o.CryptoSales.Count() == 0).OrderBy(b => b.PurchaseDate);
            if (size > 0) {
                return Ok(holdings.Take(size));
            }
            return Ok(holdings);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<CryptoHolding> Get(long id)
        {
            return Ok(_holdingRepository.Find(id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult<CryptoHolding> Create([FromBody] CryptoHolding item)
        {

            return Create<CryptoHolding>(item, nameof(Create), (long id) => _holdingRepository.Find(id));
        }
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(CryptoHolding))]
        public ActionResult<CryptoHolding> Update(long id, [FromBody] CryptoHolding item)
        {
            return Update<CryptoHolding>(id, item, (long id) => _holdingRepository.Find(id));
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<CryptoHolding>(id);

        }

   }
}


