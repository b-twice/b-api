using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using B.API.Models;
using Microsoft.Extensions.Logging;

namespace B.API.Controller
{

    [Route("v1/food/transactionCategories")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TransactionCategoryController: LookupControllerBase<TransactionCategory>
    {

        public TransactionCategoryController(AppDbContext context, ILogger<TransactionCategoryController> logger,  LookupRepository lookupRepository)
        : base(context, context.TransactionCategories, logger, lookupRepository)
        {
       }
    }
}