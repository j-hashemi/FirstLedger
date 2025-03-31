namespace FirstLedger.Domain.Entities
{
    /// <summary>
    /// Ledger entity.
    /// </summary>
    public class Ledger
    {
        /// <summary>
        /// Ledger constructor.
        /// </summary>
        /// <param name="name">name.</param>
        /// <exception cref="ArgumentNullException">name is required.</exception>
        public Ledger(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            this.Name = name;
            this.LastUpdated = DateTime.Now;
            this.Transactions = new List<Transaction>();
            this.Balance = 0;
        }

        /// <summary>
        /// Ledger ID.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Ledger name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ledger update time .
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Ledger balance.
        /// </summary>
        public decimal Balance { get; private set; }

        /// <summary>
        /// Ledger transactions.
        /// </summary>
        public List<Transaction> Transactions { get; set; }

        /// <summary>
        /// Add transaction to ledger.
        /// </summary>
        /// <param name="newTransaction"></param>
        public void AddTransaction(Transaction newTransaction)
        {
            if (newTransaction == null)
            {
                throw new ArgumentNullException(nameof(newTransaction));
            }


            if (newTransaction.IsCredit)
            {
                this.Balance += newTransaction.Amount;
            }
            else
            {
                if (this.Balance < newTransaction.Amount)
                {
                    throw new Exception("Insufficient balance.");
                }

                this.Balance -= newTransaction.Amount;
            }

            if (Transactions == null)
            {
                Transactions = new List<Transaction>();
            }
            Transactions.Add(newTransaction);
        }

        /// <summary>
        /// Edit Transaction
        /// </summary>
        /// <param name="transactionId">transactionId</param>
        /// <param name="newAmount">Amount</param>
        /// <param name="description">Description</param>
        /// <return>Transaction.</return>
        public Transaction EditTransaction(Guid transactionId, decimal newAmount, string description)
        {
            Transaction existTransaction = this.Transactions.FirstOrDefault(x => x.Id == transactionId) ?? throw new Exception("Transaction Not Found");
            decimal differentAmount = newAmount - existTransaction.Amount;

            if (existTransaction.IsCredit)
            {

                this.Balance += differentAmount;
                if (this.Balance < 0)
                {
                    throw new Exception("Insufficient balance.");
                }
            }
            else
            {
                if (differentAmount > this.Balance)
                {
                    throw new Exception("Insufficient balance.");
                }
                this.Balance -= differentAmount;
            }
            existTransaction.Amount = newAmount;
            existTransaction.Description = description;
            return existTransaction;
        }
    }
}
