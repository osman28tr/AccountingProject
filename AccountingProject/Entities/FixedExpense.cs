namespace AccountingProject.Entities
{
	// YENİ: v2'deki "Aylık sabit gider" tanımı. "Bu ayı işle" her ay bunlardan Transaction üretir.
	public class FixedExpense : IEntity
	{
		public FixedExpense()
		{
			Transactions = new List<Transaction>();
		}
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public decimal Amount { get; set; }
		public int DayOfMonth { get; set; }             // 1-31
		public Category Category { get; set; } = null!;
		public List<Transaction> Transactions { get; set; }
	}
}
