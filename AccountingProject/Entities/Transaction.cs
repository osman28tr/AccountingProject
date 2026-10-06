namespace AccountingProject.Entities
{
	public class Transaction : IEntity
	{
		public int CategoryId { get; set; }
		public decimal Amount { get; set; }
		public DateTime TransactionDate { get; set; }
		public string Note { get; set; }
		public Category Category { get; set; }
		public int Id { get; set; }
	}
}
