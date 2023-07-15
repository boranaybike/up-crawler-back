namespace Application.Common.Dtos
{
    public class ProductDto
    {
        public Guid OrderId { get; set; }

        public bool IsOnSale { get; set; }

        public decimal Price { get; set; }

        public decimal SalePrice { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }


        public ProductDto (Guid orderId, bool isOnSale, decimal price, decimal salePrice, string picture, string name)
        {
            OrderId = orderId;
            IsOnSale = isOnSale;
            Price = price;
            SalePrice = salePrice;
            Picture = picture;
            Name = name;
        }
    }
}
