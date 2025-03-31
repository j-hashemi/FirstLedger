
namespace FirstLedger.Service.Responses
{
    /// <summary>
    /// Transactions Query Response.
    /// </summary>
    public class TransactionsQueryResponse
    {
        public List<TransactionQueryResponseItem> Items { get; set; }
        public int TotalCount { get; set; }
    }

    /// <summary>
    /// transaction item details.
    /// </summary>
    public class TransactionQueryResponseItem
    {
        /// <summary>
        /// Transaction Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Transaction Type 
        /// </summary>
        public int TransactionType { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ledger Name 
        /// </summary>
        public string LedgerName { get; set; }

        /// <summary>
        /// Transaction Ledger Id 
        /// </summary>
        public Guid LedgerId { get; set; }

        /// <summary>
        /// Transaction Date time.
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
