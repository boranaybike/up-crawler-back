namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public int RequestedAmount { get; set; }
        public int TotalFoundAmount { get; set; }
        public string ProductCrawlType { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

        public List<Product> Products { get; set; }
        public List<SeleniumLog> SeleniumLogs { get; set; }
    }
}
