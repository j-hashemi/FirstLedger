using FirstLedger.Service.Responses;
using FirstLedger.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstLedger.WebCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LedgerController : Controller
    {

        private readonly ILedgerService _ledgerService;

        /// <summary>
        /// Ledger controller constructor.
        /// </summary>
        /// <param name="ledgerRepository">ledger repository</param>
        public LedgerController(ILedgerService ledgerService)
        {
            this._ledgerService = ledgerService;
        }
        /// <summary>
        /// Get default ledger.
        /// </summary>
        /// <returns>Ledger.</returns>
        [HttpGet(Name = "Default")]
        public async Task<LedgerQueryResponse> GetDefultLedger()
        {
            return await this._ledgerService.GetDefaultLedger();
        }

        /// <summary>
        /// Get ledger balance.
        /// </summary>
        /// <returns>Ledger.</returns>
        [HttpGet("Balance/{ledgerId}")]
        public async Task<decimal> GetLedgerBalance(Guid ledgerId)
        {
            return await this._ledgerService.GetLedgerBalance(ledgerId);
        }

       
    }
}
