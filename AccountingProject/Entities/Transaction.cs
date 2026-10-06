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
		public string Note { get; set; }
		public Category Category { get; set; }
		public PaymentMethodType PaymentType { get; set; }
		public FixedExpense? FixedExpense { get; set; }
		public Debt? Debt { get; set; }
	}
	public enum PaymentMethodType : byte
	{
		Card = 1,
		Cash = 2,
		Transfer = 3
	}
}
