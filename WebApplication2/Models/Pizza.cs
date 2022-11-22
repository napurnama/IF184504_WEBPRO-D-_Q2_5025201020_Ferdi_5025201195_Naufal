namespace Pizzeria.Models
{
	public class Pizza
	{
		public int Id { get; set; }
		public string? ImagePath { get; set; }
		public string? PizzaName{ get; set; }
		public decimal PizzaPrice { get; set; } = 5;
		public bool Tomato { get; set; }
		public bool Cheese { get; set; }
		public bool Mushroom { get; set; }
		public bool Chicken { get; set; }
		public bool Bbq{ get; set; }
		public string? Description { get; set; }
	}
}
