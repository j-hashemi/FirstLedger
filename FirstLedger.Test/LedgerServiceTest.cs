using FirstLedger.Domain.Entities;
using FirstLedger.Domain.Enums;
using FirstLedger.Repository.Repositories;
using FirstLedger.Service.Requests;
using FirstLedger.Service.Responses;
using FirstLedger.Service.Services;

namespace FirstLedger.Test
{
    public class LedgerServiceTest : IDisposable
    {
        private readonly ILedgerService _ledgerService;
        private readonly LedgerRepository _repository;
        public LedgerServiceTest()
        {
            this._repository = new LedgerRepository();
            this._ledgerService = new LedgerService(this._repository);
        }

        [Fact]
        public async Task GetDefaultLedger_ShouldNotBeNull_WhenProgramStarts()
        {
            //Arrange and Act
            LedgerQueryResponse defaultLedger = await this._ledgerService.GetDefaultLedger();

            // Asserts
            Assert.NotNull(defaultLedger);
        }

        [Fact]
        public async Task GetLedgerBalane_ShouldBeZero_WhenNoTransactionExists()
        {
            // Arrange 
            LedgerQueryResponse ledger = await this._ledgerService.GetDefaultLedger();

            // Act
            decimal balance = await this._ledgerService.GetLedgerBalance(ledger.Id);

            //Asserts
            Assert.Equal(0, balance);
        }

        [Fact]
        public async Task AddTransactionToLedger_ShouldBalanceRaise_WhenDepositTransaction()
        {
            // Arrange
            LedgerQueryResponse ledger = await this._ledgerService.GetDefaultLedger();

            // Act
            var request = new TransactionRequest()
            {
                Amount = 10,
                LedgerId = ledger.Id,
                TransactionType = (int)TransactionType.Deposit,
                Description = "Description"
            };
            Transaction transaction = await this._ledgerService.AddTransactionToLedger(request);

            Assert.NotNull(transaction);
            Assert.True(transaction.IsCredit);


            decimal balance = await this._ledgerService.GetLedgerBalance(ledger.Id);


            // Asserts
            Assert.NotEqual(0, balance);
            Assert.Equal(transaction.Amount, balance);
            Assert.True(transaction.IsCredit);
            Assert.Equal(transaction.Description, request.Description);
        }

        [Fact]
        public async Task AddTransactionToLedger_ShouldThrowException_WhenTransactionAmountEqualOrLessThanZero()
        {
            // Arrange
            LedgerQueryResponse ledger = await this._ledgerService.GetDefaultLedger();

            // Act
            var request = new TransactionRequest()
            {
                Amount = -1,
                LedgerId = ledger.Id,
                TransactionType = (int)TransactionType.Deposit,
                Description = "Description"
            };

            // Asserts
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => this._ledgerService.AddTransactionToLedger(request));
        }

        [Fact]
        public async Task AddTransactionToLedger_ShouldThrowException_WhenTransactionLedgerNotExists()
        {
            // Act
            var request = new TransactionRequest()
            {
                Amount = 100,
                LedgerId = Guid.NewGuid(),
                TransactionType = (int)TransactionType.Deposit,
                Description = "Description"
            };

            // Asserts
            await Assert.ThrowsAsync<Exception>(() => this._ledgerService.AddTransactionToLedger(request));
        }

        [Fact]
        public async Task TransactionList_ShouldHaveOneTransaction_WhenAddDepositTransaction()
        {
            // Arrange
            LedgerQueryResponse ledger = await this._ledgerService.GetDefaultLedger();

            // Act
            var request = new TransactionRequest()
            {
                Amount = 10,
                LedgerId = ledger.Id,
                TransactionType = (int)TransactionType.Deposit,
                Description = "Description"
            };

            Transaction transaction = await this._ledgerService.AddTransactionToLedger(request);

            // for this test we only call method with default parameters 
            var resposne = await this._ledgerService.GetLedgerTransactions(ledger.Id);

            // Asserts
            Assert.NotNull(resposne);
            Assert.Equal(1, resposne.TotalCount);
            Assert.NotNull(resposne.Items);
            Assert.NotEmpty(resposne.Items);
            Assert.Equal(1, resposne.Items?.Count);

            Assert.Equal(transaction.Amount, resposne.Items[0].Amount);
            Assert.Equal(transaction.Description, resposne.Items[0].Description);
        }


        /// <summary>
        /// Clear Data after each running test.
        /// </summary>
        public void Dispose()
        {
            this._repository.ClearData();
        }
    }
}
