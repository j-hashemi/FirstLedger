
namespace FirstLedger.Service.Responses
{
    /// <summary>
    /// Ledger Query Response.
    /// </summary>
    public class LedgerQueryResponse
    {
        /// <summary>
        /// Ledger Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Balance.
        /// </summary>
        public decimal Balance { get; set; }
    }
}
