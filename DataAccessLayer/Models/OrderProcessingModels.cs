namespace DataAccessLayer.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DiscountPercent { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal TotalCost { get; set; }

        public Customer Customer { get; set; }
        public Product Product { get; set; }
    }
}
