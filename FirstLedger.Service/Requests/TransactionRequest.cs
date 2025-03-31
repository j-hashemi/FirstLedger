namespace FirstLedger.Service.Requests
{
    public class TransactionRequest
    {
        /// <summary>
        /// Ledger Id.
        /// </summary>
        public Guid LedgerId { get; set; }

        /// <summary>
        /// Transaction amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Transaction type.
        /// </summary>
        public int TransactionType { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }
    }
}
