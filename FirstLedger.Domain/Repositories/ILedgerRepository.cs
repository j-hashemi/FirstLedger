using FirstLedger.Domain.Entities;

namespace FirstLedger.Domain.Repositories
{
    /// <summary>
    /// Ledger repository.
    /// </summary>
    public interface ILedgerRepository
    {
        /// <summary>
        /// Get default ledger.
        /// </summary>
        /// <returns>Ledger.</returns>
        Task<Ledger> GetDefaultLedger();

        /// <summary>
        /// Get ledger By Ledger Id.
        /// </summary>
        /// <param name="ledgerId">ledger Id.</param>
        /// <returns>Ledger.</returns>
        Task<Ledger> GetLedger(Guid ledgerId);

        /// <summary>
        /// Update ledger.
        /// </summary>
        /// <param name="ledger"></param>
        /// <returns></returns>
        Task UpdateLedger(Ledger ledger);
    }
}