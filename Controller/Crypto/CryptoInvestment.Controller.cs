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
    
    [Route("v1/cryptoInvestments")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CryptoInvestmentController : AppControllerBase
    {
        private readonly ILogger _logger;

        private readonly AppDbContext _context;
        private readonly CryptoInvestmentRepository _repository;
        public CryptoInvestmentController(AppDbContext context, CryptoInvestmentRepository repository, ILogger<CryptoInvestmentController> logger) : base(context, logger)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        // [HttpGet("lookups")]
        // [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        // public ActionResult<CryptoHoldingLookupsDto> GetLookups()
        // {
        //     return Ok(_cryptoInvestmentRds.InvestmentLookups());
        // }

        [HttpGet("summary")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<CryptoInvestment>> GetInvestments()
        {
            return Ok(_repository.FindAll());
        }


        [HttpGet("page")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<PaginatedResult<CryptoInvestment>> GetPage(
            [FromQuery]string sortName,
            [FromQuery]int pageNumber,
            [FromQuery]int pageSize,
            [FromQuery]List<string> coins,
            [FromQuery]List<string> yearsSold,
            [FromQuery]string holdingStatus
        ) 
        {
            var items = _repository.Filter(_repository.FindAll(), coins, yearsSold, holdingStatus);
            items = _repository.Order(items, sortName);
            var paginatedList = PaginatedList<CryptoInvestment>.Create(items, pageNumber, pageSize);
            return Ok(new PaginatedResult<CryptoInvestment>(paginatedList, paginatedList.TotalCount));
        }




        // [HttpGet("page")]
        // [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        // public ActionResult<PaginationResponse<CryptoInvestmentDto>> GetPage
        // (
        //     [FromQuery] int? offset,
        //     [FromQuery] int? limit,
        //     [FromQuery] string sort,
        //     [FromQuery] IReadOnlyList<string> coins,
        //     [FromQuery] IReadOnlyList<int> yearsSold,
        //     [FromQuery] string holdingStatus
        // )
        // {
        //     offset ??= 0;
        //     limit = Math.Min(limit ?? 10, 500); // default to 10, limit max results to 500
        //     var investments = _cryptoInvestmentRds.SearchInvestments(new SearchHoldingsParams(sort, coins, yearsSold, holdingStatus));
        //     return Ok(investments.Page(new PaginationInfo { offset = offset.Value, limit = limit.Value }));
        // }

        // [HttpDelete("{id}")]
        // public IActionResult Delete(int id) => Delete(id, _cryptoHoldingWds.DeleteHolding);
    }
}