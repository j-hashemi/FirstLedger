using FirstLedger.Domain.Entities;
using FirstLedger.Domain.Repositories;

namespace FirstLedger.Repository.Repositories
{
    public class LedgerRepository : ILedgerRepository
    {

        public static Dictionary<Guid, Ledger> IdempotencyMap = new Dictionary<Guid, Ledger>();
        
        /// <summary>
        /// Ledger Repository.
        /// </summary>
        public LedgerRepository()
        {
            AddDefaultLedger();
        }

        /// <summary>
        /// Get default ledger.
        /// </summary>
        /// <returns>Default Ledger.</returns>
        public async Task<Ledger> GetDefaultLedger()
        {
            return await Task.FromResult(IdempotencyMap.First().Value);
        }

        /// <summary>
        /// Get ledger By Ledger Id.
        /// </summary>
        /// <param name="ledgerId"></param>
        /// <returns></returns>
        public async Task<Ledger> GetLedger(Guid ledgerId)
        {
            if (IdempotencyMap.ContainsKey(ledgerId))
            {
                return await Task.FromResult(IdempotencyMap[ledgerId]);
            }
            else
            {
                return null;
            }
            
        }

        /// <summary>
        /// Update Ledger.
        /// </summary>
        /// <param name="ledger"></param>
        /// <returns></returns>
        public Task UpdateLedger(Ledger ledger)
        {
            ledger.LastUpdated = DateTime.UtcNow;
            IdempotencyMap[ledger.Id] = ledger;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Clear Data;
        /// this method is only for test purpose in real world scenario we don't need to use this approach.
        /// </summary>
        public void ClearData()
        {
            IdempotencyMap.Clear();
            AddDefaultLedger();

        }

        /// <summary>
        /// Default Ledger for system initialization.
        /// </summary>
        private void AddDefaultLedger()
        {

            if (IdempotencyMap.Count == 0)
            {
                var ledger = new Ledger("First Ledge");
                IdempotencyMap[ledger.Id] = ledger;
            }

        }
    }
}
