using Microsoft.AspNetCore.Mvc;
using B.API.Database;
using B.API.Models;
using Microsoft.Extensions.Logging;

namespace B.API.Controller
{

    [Route("v1/food/transactionTags")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TransactionTagController: LookupControllerBase<TransactionTag>
    {

        public TransactionTagController(AppDbContext context, ILogger<TransactionTagController> logger,  LookupRepository lookupRepository)
        : base(context, context.TransactionTags, logger, lookupRepository)
        {
       }
    }
}