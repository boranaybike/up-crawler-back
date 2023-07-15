using Domain.Enums;

namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public int RequestedAmount { get; set; }
        public int TotalFoundAmount { get; set; }
        public ProductCrawlType ProductCrawlType { get; set; }
        public ICollection<OrderEvent>? OrderEvents { get; set; } // Bot started...
        public DateTimeOffset CreatedOn { get; set; }

        public List<Product> Products { get; set; }
    }
}
