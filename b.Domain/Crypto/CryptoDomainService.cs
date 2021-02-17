using System;
using b.Data.Crypto.Read;
using b.Entities;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace b.Domain.Crypto
{
  public class CryptoDomainService
  {
    //private CryptoReadDataService _cryptoRds;
    private IMapper _mapper;
    private CryptoInvestmentManager cryptoInvestmentManager;

    public CryptoDomainService(IMapper mapper)
    {
      //_cryptoRds = cryptoRds;
      _mapper = mapper;
      cryptoInvestmentManager = new CryptoInvestmentManager();
    }

    //public IEnumerable<CryptoInvestmentDto> PriceCryptoHoldings(IEnumerable<CryptoHolding> holdings)
    //{
    //  var prices = _cryptoRds.LatestPrices();
    //  var latestPrices = _mapper.Map<IEnumerable<CoinPrice>>(prices);
    //  var investments = _mapper.Map<IEnumerable<Investment>>(holdings);
    //  var pricedInvestments = cryptoInvestmentManager.PriceCryptoHoldings(investments, latestPrices);
    //  return _mapper.Map<IEnumerable<CryptoInvestmentDto>>(pricedInvestments);
    //}

    //public IEnumerable<CryptoInvestmentDto> CollectCryptoSales(IEnumerable<CryptoHolding> holdings)
    //{
    //  var prices = _cryptoRds.LatestPrices();
    //  var latestPrices = _mapper.Map<IEnumerable<CoinPrice>>(prices);
    //  var investments = _mapper.Map<IEnumerable<Investment>>(holdings);
    //  var pricedInvestments = cryptoInvestmentManager.PriceCryptoHoldings(investments, latestPrices);
    //  return _mapper.Map<IEnumerable<CryptoInvestmentDto>>(pricedInvestments);
    //}
  }
}
