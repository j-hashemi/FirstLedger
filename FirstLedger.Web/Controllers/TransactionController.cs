using FirstLedger.Domain.Entities;
using FirstLedger.Service.Requests;
using FirstLedger.Service.Responses;
using FirstLedger.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstLedger.WebCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        /// <summary>
        /// Ledger service.
        /// </summary>
        private readonly ILedgerService _ledgerService;

        /// <summary>
        /// Transaction controller constructor.
        /// </summary>
        /// <param name="ledgerService"></param>
        public TransactionController(ILedgerService ledgerService)
        {
            this._ledgerService = ledgerService;
        }

        [HttpGet(Name = "GetLedgerTransactions")]
        public async Task<TransactionsQueryResponse> Get(Guid ledgerId, int pageNumber = 1, int pageSize = 10, string searchText = "")
        {
            ArgumentNullException.ThrowIfNull(ledgerId, nameof(ledgerId));
            return await this._ledgerService.GetLedgerTransactions(ledgerId, pageNumber, pageSize, searchText);
        }

        [HttpPost(Name = "AddTransaction")]
        public async Task<Transaction> AddTransaction(TransactionRequest transactionRequest)
        {
            ArgumentNullException.ThrowIfNull(transactionRequest, nameof(transactionRequest));
            return await this._ledgerService.AddTransactionToLedger(transactionRequest);
        }

        [HttpPut(Name = "EditTransaction")]
        public async Task<Transaction> EditTransaction(EditTransactionRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));
            return await this._ledgerService.EditTransaction(request);
        }

    }
}
