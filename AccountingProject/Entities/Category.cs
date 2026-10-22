namespace AccountingProject.Entities
{
	public class Category : IEntity
	{
		public Category()
		{
			Transactions = new List<Transaction>();
			FixedExpenses = new List<FixedExpense>();
		}
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public TransactionType Type { get; set; }
		public List<Transaction> Transactions { get; set; }
		public List<FixedExpense> FixedExpenses { get; set; }
	}

	// System.Type ile çakışmasın diye "Type" yerine "TransactionType"; byte alt tipi => tinyint
	public enum TransactionType : byte
	{
		Income = 1,
		Expense = 2,
		FixedExpense = 3
	}
}
