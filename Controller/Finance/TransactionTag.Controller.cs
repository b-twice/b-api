using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using B.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(TransactionTag))]
        new public ActionResult<TransactionTag>  Update(int id, [FromBody] TransactionTag item)
        {
            return base.Update(id, item);
        }

    }
}