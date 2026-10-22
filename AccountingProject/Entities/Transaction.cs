namespace AccountingProject.Entities
{
	public class Transaction : IEntity
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public int? FixedExpenseId { get; set; }
		public int? DebtId { get; set; }
		public decimal Amount { get; set; }
		public DateTime TransactionDate { get; set; }
		public string? Note { get; set; }
		public PaymentMethod? PaymentMethod { get; set; }   // Eski kayıtlarda boş olabilir
		public Category Category { get; set; } = null!;
		public FixedExpense? FixedExpense { get; set; }
		public Debt? Debt { get; set; }
	}

	// byte alt tipi sayesinde SQL Server'da tinyint olur
	public enum PaymentMethod : byte
	{
		Card = 1,
		Cash = 2,
		BankTransfer = 3
	}
}
