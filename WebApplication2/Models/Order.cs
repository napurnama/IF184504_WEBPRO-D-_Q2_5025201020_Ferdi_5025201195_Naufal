namespace Pizzeria.Models
{
	public class Order
	{
		public int Id { get; set; }
		public string CstName { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public string PizzaName { get; set; }
		public decimal PizzaPrice { get; set; }
		//public string OrderStatus { get; set; } = OrderStatusConstants.Queued;

		public string CstId { get; set; }
	}

	public enum OrderStatusConstants
	{
		Queued,
		Cooking,
		Ready,
		Delivered,
    }
}

