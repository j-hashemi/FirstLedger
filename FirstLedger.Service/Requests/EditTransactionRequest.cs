
namespace FirstLedger.Service.Requests
{
    /// <summary>
    /// Edit Transaction Request.
    /// </summary>
    public class EditTransactionRequest
    {
        /// <summary>
        /// transaction Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ledger Id.
        /// </summary>
        public Guid LedgerId { get; set; }
    }
}
