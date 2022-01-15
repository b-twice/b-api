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

    [Route("v1/crypto/sales")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CryptoSaleController: AppControllerBase
    {
        private readonly CryptoSaleRepository _saleRepository;
        private readonly AppDbContext _context;

        private readonly ILogger _logger;
        public CryptoSaleController(AppDbContext context, ILogger<CryptoSaleController> logger, CryptoSaleRepository saleRepository): base(context, logger)
        {
            _saleRepository = saleRepository;
            _context = context;
            _logger = logger;
        }


        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<CryptoSale>> GetPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize
        ) 
        {
            var sales = _saleRepository.Include(_saleRepository.Order(_context.CryptoSales.AsNoTracking(), sortName));
            var paginatedList = PaginatedList<CryptoSale>.Create(sales, pageNumber, pageSize);
            return Ok(new PaginatedResult<CryptoSale>(paginatedList, paginatedList.TotalCount));
        }


        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<CryptoSale>> GetAll(
            [FromQuery]int size 
        ) 
        {
            var sales =_saleRepository.FindAll().OrderByDescending(b => b.SellDate);
            if (size > 0) {
                return Ok(sales.Take(size));
            }
            return Ok(sales);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<CryptoSale> Get(long id)
        {
            return Ok(_saleRepository.Find(id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult<CryptoSale> Create([FromBody] CryptoSale item)
        {

            return Create<CryptoSale>(item, nameof(Create), (long id) => _saleRepository.Find(id));
        }
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(CryptoSale))]
        public ActionResult<CryptoSale> Update(long id, [FromBody] CryptoSale item)
        {
            return Update<CryptoSale>(id, item, (long id) => _saleRepository.Find(id));
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return Delete<CryptoSale>(id);

        }

   }
}


