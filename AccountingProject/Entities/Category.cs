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
		public string Name { get; set; }
		public Type Type { get; set; }
		public List<Transaction> Transactions { get; set; }
		public List<FixedExpense> FixedExpenses { get; set; }
	}
	public enum Type
	{
		Income,
		Expense,
		FixedExpense
	}
}
