using System;
using AutoMapper;
using b.Entities;
using System.Linq;
using System.Collections.Generic;
using b.Data.Crypto.Write.Accessor;
using b.Data.Models;

namespace b.Data.Crypto.Write
{
    public class CryptoHoldingWds : IWriteDataService
    {
        private readonly DeleteHoldingAccessor _deleteHoldingAccessor;

        public CryptoHoldingWds(DeleteHoldingAccessor deleteHoldingAccessor, IMapper mapper)
        {
            _deleteHoldingAccessor = deleteHoldingAccessor;
        }

        public void DeleteHolding(long id) => _deleteHoldingAccessor.Execute(id);
    }
}
