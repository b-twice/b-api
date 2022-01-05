using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using B.API.Models;
using Microsoft.Extensions.Logging;

namespace B.API.Controller
{

    [Route("v1/food/banks")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BankController: LookupControllerBase<Bank>
    {

        public BankController(AppDbContext context, ILogger<BankController> logger,  LookupRepository lookupRepository)
        : base(context, context.Banks, logger, lookupRepository)
        {
       }
    }
}