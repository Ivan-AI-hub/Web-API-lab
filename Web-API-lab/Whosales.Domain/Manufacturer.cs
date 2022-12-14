namespace Whosales.Domain
{
	public partial class Manufacturer
	{
		public Manufacturer()
		{
			Products = new HashSet<Product>();
		}

		public int ManufacturerId { get; set; }
		public string Name { get; set; } = null!;

		public virtual ICollection<Product> Products { get; set; }
	}
}
