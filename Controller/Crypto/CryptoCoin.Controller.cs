using Microsoft.AspNetCore.Mvc;
using B.API.Repository;
using B.API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace B.API.Controller
{

    [Route("v1/food/cryptoCoins")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CryptoCoinController: LookupControllerBase<CryptoCoin>
    {

        public CryptoCoinController(AppDbContext context, ILogger<CryptoCoinController> logger,  LookupRepository lookupRepository)
        : base(context, context.CryptoCoins, logger, lookupRepository)
        {
       }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(CryptoCoin))]
        new public ActionResult<CryptoCoin>  Update(int id, [FromBody] CryptoCoin item)
        {
            return base.Update(id, item);
        }
    }
}