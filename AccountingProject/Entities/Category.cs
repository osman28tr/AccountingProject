namespace AccountingProject.Entities
{
	public class Category : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Type Type { get; set; }		
	}
	public enum Type
	{
		Income,
		Expense,
		FixedExpense
	}
}
