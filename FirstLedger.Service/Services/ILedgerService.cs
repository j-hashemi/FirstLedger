using FirstLedger.Domain.Entities;
using FirstLedger.Service.Requests;
using FirstLedger.Service.Responses;

namespace FirstLedger.Service.Services
{
    /// <summary>
    /// Ledger Service.
    /// </summary>
    public interface ILedgerService
    {

        /// <summary>
        /// Default Ledger.
        /// </summary>
        /// <returns>Ledger.</returns>
        Task<LedgerQueryResponse> GetDefaultLedger();

        /// <summary>
        /// Get Ledger Balance 
        /// </summary>
        /// <param name="ledgerId">ledger Id.</param>
        /// <returns></returns>
        Task<decimal> GetLedgerBalance(Guid ledgerId);

        /// <summary>
        /// Add transaction to ledger.
        /// </summary>
        /// <param name="transactionRequest">transaction Request</param>
        /// <returns>Transaction</returns>
        Task<Transaction> AddTransactionToLedger(TransactionRequest transactionRequest);

        /// <summary>
        /// Get ledger transactions.
        /// </summary>
        /// <param name="ledgerId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchText"></param>
        /// <returns>Transactions Query Response.</returns>
        Task<TransactionsQueryResponse> GetLedgerTransactions(Guid ledgerId, int pageNumber = 1, int pageSize = 10, string searchText = "");

        /// <summary>
        /// Edit Transaction.
        /// </summary>
        /// <param name="request">EditTransactionRequest.</param>
        /// <returns>Transaction.</returns>
        Task<Transaction> EditTransaction(EditTransactionRequest request);
    }
}
