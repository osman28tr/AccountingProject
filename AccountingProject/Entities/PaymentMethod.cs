namespace AccountingProject.Entities
{
	public class PaymentMethod
	{
		public int Id { get; set; }
		public PaymentMethodType PaymentType { get; set; }
	}
	public enum PaymentMethodType
	{
		Card,
		Cash,
		Transfer
	}
}
