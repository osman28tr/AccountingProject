namespace AccountingProject.Entities
{
	// YENİ: v2'deki "Kurumun güncel borcu".
	public class Debt
	{
		public Debt()
		{
			Transactions = new List<Transaction>();
		}
		public int Id { get; set; }
		public string Creditor { get; set; } // Alacaklı
		public decimal Amount { get; set; }
		public DateTime? DueDate { get; set; }
		public List<Transaction> Transactions { get; set; }
	}
}
