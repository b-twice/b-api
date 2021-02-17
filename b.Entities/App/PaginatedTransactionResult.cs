using System.Collections.Generic;

namespace b.Entities
{
  public class PaginatedTransactionResult : PaginatedResult<TransactionRecord>
  {
    public decimal AmountTotal { get; private set; }

    public PaginatedTransactionResult(List<TransactionRecord> items, int count, decimal amountTotal) : base(items, count)
    {
      AmountTotal = amountTotal;
    }
  }
}