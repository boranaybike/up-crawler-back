namespace Application.Common.Dtos
{
    public class ProductDto
    {
        public Guid OrderId { get; set; }

        public bool IsOnSale { get; set; }

        public string Price { get; set; }

        public string SalePrice { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }


        public ProductDto (Guid orderId, bool isOnSale, string price, string salePrice, string picture, string name)
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
