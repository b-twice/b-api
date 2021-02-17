using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using b.Data.Crypto.Read;
using b.Data.Models;
using b.Api.Models;
using Microsoft.AspNetCore.Authorization;
using b.Data.Crypto.Write;
using b.Api.Extensions;

namespace b.Api.Controller
{
  [Authorize]
  [Route("v1/cryptoholdings")]
  [ApiController]
  [ApiConventionType(typeof(DefaultApiConventions))]
  public class CryptoController : AppBaseController<CryptoController>
  {
    private CryptoInvestmentRds _cryptoInvestmentRds;
    private CryptoHoldingWds _cryptoHoldingWds;

    private readonly ILogger _logger;
    public CryptoController(CryptoInvestmentRds cryptoInvestmentRds, CryptoHoldingWds cryptoHoldingWds, ILogger<CryptoController> logger) : base(logger)
    {
      _cryptoInvestmentRds = cryptoInvestmentRds;
      _logger = logger;
      _cryptoHoldingWds = cryptoHoldingWds;
    }

    [HttpGet("lookups")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<CryptoHoldingLookupsDto> GetLookups()
    {
        return Ok(_cryptoInvestmentRds.InvestmentLookups());
    }

    [HttpGet("page")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Find))]
    public ActionResult<PaginationResponse<CryptoInvestmentDto>> GetCryptoPage(
        [FromQuery] int? offset,
        [FromQuery] int? limit,
        [FromQuery] string sort,
        [FromQuery] IList<string> coins,
        [FromQuery] IList<int> yearsSold,
        [FromQuery] string holdingStatus
    )
    {
      offset ??= 0;
      limit = Math.Min(limit ?? 10, 500); // default to 10, limit max results to 500
      var investments = _cryptoInvestmentRds.SearchInvestments(new SearchHoldingsParams(sort, coins, yearsSold, holdingStatus));  
      return Ok(investments.Page(new PaginationInfo { offset = offset.Value, limit = limit.Value }));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) => Delete(id, _cryptoHoldingWds.DeleteHolding);
  }
}
