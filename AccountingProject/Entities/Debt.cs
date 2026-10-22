namespace AccountingProject.Entities
{
	// YENİ: v2'deki "Kurumun güncel borcu".
	public class Debt : IEntity
	{
		public Debt()
		{
			Payments = new List<Transaction>();
		}
		public int Id { get; set; }
		public string Creditor { get; set; } = null!;   // Alacaklı
		public decimal TotalAmount { get; set; }
		public DateTime? DueDate { get; set; }
		public List<Transaction> Payments { get; set; }
	}
}
