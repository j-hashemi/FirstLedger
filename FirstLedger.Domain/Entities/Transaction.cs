using FirstLedger.Domain.Enums;

namespace FirstLedger.Domain.Entities
{
    /// <summary>
    /// Transaction entity.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Transaction ID.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Transaction description.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Transaction amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Transaction date and time.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Transaction type(Deposit, Withdraw).
        /// </summary>
        public TransactionType Type { get; set; }

        /// <summary>
        /// Ledger ID.
        /// </summary>
        public Guid LedgerId { get; set; }

        /// <summary>
        /// is credit transaction.
        /// </summary>
        public bool IsCredit { get; set; }

        /// <summary>
        /// Factory method to create new transaction.
        /// </summary>
        /// <param name="amount">transaction amount.</param>
        /// <param name="transactionType">transaction type.</param>
        /// <param name="ledger">ledger.</param>
        /// <returns></returns>
        /// <exception cref="Exception">if type is invalid.</exception>
        public static Transaction CreateNewTransaction(decimal amount, int transactionType,string description, Ledger ledger)
        {

            if(amount < 0) throw new ArgumentOutOfRangeException("amount should be more than zero");

            switch ((TransactionType)transactionType)
            {
                case TransactionType.Deposit:
                    return new Transaction
                    {
                        Amount = amount,
                        LedgerId = ledger.Id,
                        DateTime = DateTime.Now,
                        Type = TransactionType.Deposit,
                        IsCredit = true,
                        Description = description
                    };
                case TransactionType.Withdraw:
                    return new Transaction
                    {
                        Amount = amount,
                        LedgerId = ledger.Id,
                        DateTime = DateTime.Now,
                        IsCredit = false,
                        Type = TransactionType.Withdraw,
                        Description = description
                    };
                default:
                    throw new Exception("Invalid transaction type.");

            }
        }
    }
}