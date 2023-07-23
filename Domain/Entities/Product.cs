namespace Domain.Entities
{
    public class Product
    {

        public Guid Id { get; set; }

        public Guid? OrderId { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public bool IsOnSale { get; set; }

        public string Price { get; set; }
        
        public string? SalePrice { get; set; }

        public Order Order { get; set; }

    }
}
