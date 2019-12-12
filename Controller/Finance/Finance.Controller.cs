using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using B.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoMapper;

namespace B.API.Controller
{

    [Route("v1/finance")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FinanceController: AppControllerBase
    {
        private readonly ApiDbContext _context;

        private readonly IMapper _mapper;


        private readonly ILogger _logger;
        public FinanceController(ApiDbContext context, ILogger<FinanceController> logger, IMapper mapper): base(context, logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }


        [HttpGet("summary/{year}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<IEnumerable<User>> GetUserProfile(string year) 
        {
            var asset = _context.Asset.First(o => o.Year.Substring(0,4) == year);
            var debt = _context.Debt.First(o => o.Year.Substring(0,4) == year);
            var investment = _context.Investment.First(o => o.Year.Substring(0,4) == year);
            var earnings = _context.Earning.Where(o => o.Year.Substring(0,4) == year);

            var summary = _mapper.Map<FinancialSummary>(asset);
            summary = _mapper.Map(debt, summary);
            summary = _mapper.Map(investment, summary);
            summary = _mapper.Map(earnings, summary);
            return Ok(summary);
        }


        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
        public ActionResult<TransactionRecord> GetUser(long id)
        {
            return Ok(_context.User.First(o => o.Id == id));
        }



   }
}