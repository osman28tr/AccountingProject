namespace AccountingProject.Entities
{
	public class Budget : IEntity
	{
		public int Id { get; set; }
		public decimal PoolAmount { get; set; }
		public decimal MonthlyLimit { get; set; }
	}
}
