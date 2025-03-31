using FirstLedger.Domain.Entities;
using FirstLedger.Domain.Repositories;
using FirstLedger.Service.Requests;
using FirstLedger.Service.Responses;

namespace FirstLedger.Service.Services
{
    /// <summary>
    /// Ledger Service.
    /// </summary>
    public class LedgerService : ILedgerService
    {
        private ILedgerRepository _ledgerRepository;

        /// <summary>
        /// LedgerService.
        /// </summary>
        /// <param name="ledgerRepository">ledgerRepository.</param>
        public LedgerService(ILedgerRepository ledgerRepository)
        {
            this._ledgerRepository = ledgerRepository;
        }

        /// <summary>
        /// Add Transaction to Ledger
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>Transaction.</returns>
        /// <exception cref="Exception">if the ledger can't be found.</exception>
        public async Task<Transaction> AddTransactionToLedger(TransactionRequest request)
        {
            Ledger ledger = await GetLedger(request.LedgerId);
            Transaction newTransaction = Transaction.CreateNewTransaction(request.Amount, request.TransactionType, request.Description, ledger);

            ledger.AddTransaction(newTransaction);

            await this._ledgerRepository.UpdateLedger(ledger);
            return newTransaction;
        }

        /// <summary>
        /// EditTransaction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Transaction> EditTransaction(EditTransactionRequest request)
        {
            //In real world scenario we are not fetching ledger with all transactions to edit but in here because 
            // we are keeping all the data in memory I use this approach. 

            Ledger ledger = await GetLedger(request.LedgerId);
            var transaction = ledger.EditTransaction(request.Id, request.Amount, request.Description);

            await this._ledgerRepository.UpdateLedger(ledger);
            return transaction;

        }

        /// <summary>
        /// Get default ledger. 
        /// </summary>
        /// <returns>Ledger.</returns>
        public async Task<LedgerQueryResponse> GetDefaultLedger()
        {
            Ledger ledger = await this._ledgerRepository.GetDefaultLedger();

            return new LedgerQueryResponse
            {
                Balance = ledger.Balance,
                Id = ledger.Id,
                Name = ledger.Name,
            };
        }

        /// <summary>
        /// Ledger Balance by ledger Id .
        /// </summary>
        /// <param name="ledgerId">ledger Id.</param>
        /// <returns>balance of ledger.</returns>
        public async Task<decimal> GetLedgerBalance(Guid ledgerId)
        {
            Ledger ledger = await GetLedger(ledgerId);
           
            return ledger.Balance;
        }


        /// <summary>
        /// Get Ledger Transactions 
        /// </summary>
        /// <param name="ledgerId">ledger id</param>
        /// <param name="pageNumber">page number (default value is 1).</param>
        /// <param name="pageSize">page size default value is 10.</param>
        /// <param name="searchText"></param>
        /// <returns>Transactions Query Response.</returns>
        public async Task<TransactionsQueryResponse> GetLedgerTransactions(Guid ledgerId, int pageNumber=1, int pageSize=10, string searchText="")
        {
            // In real world scenario we never get transaction from ledger object
            // we directly query from ledger table but in this implementation because its gets application more complecated
            // I prefer to fetch transactions with this approach.

            TransactionsQueryResponse response = new TransactionsQueryResponse();
            Ledger ledger = await GetLedger(ledgerId);

            var query = ledger.Transactions.AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                var transactions = query.Where(x => x.Description.Contains(searchText));
            }
            
            response.TotalCount = query.Count();
            response.Items =  query.Select(x => new TransactionQueryResponseItem()
             {
                 DateTime = x.DateTime,
                 Description = x.Description,
                 Id = x.Id,
                 LedgerId = x.LedgerId,
                 Amount = x.Amount,
                 LedgerName = ledger.Name,
                 TransactionType = (int)x.Type,
             }).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return response;
            
        }

        private async Task<Ledger> GetLedger(Guid ledgerId)
        {
            return await this._ledgerRepository.GetLedger(ledgerId) ?? throw new Exception("Ledger not found.");
        }
    }
}
