namespace Pizzeria.Models
{
	public class Order
	{
		public int Id { get; set; }
		public string CstName { get; set; }
		public DateTime CreatedDate { get; set; }
		public string PizzaName { get; set; }
		public decimal PizzaPrice { get; set; }
		public OrderStatus OrderStatus { get; set; }
	}

	public enum OrderStatus
	{
		Queued,
		Cooking, 
		Ready,
		Delivered
	}
}

