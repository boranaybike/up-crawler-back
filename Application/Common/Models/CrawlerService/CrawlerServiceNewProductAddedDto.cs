using Application.Features.Products.Commands.Queries.GetById;

namespace Application.Common.Models.CrawlerService
{
    public class CrawlerServiceNewProductAddedDto
    {

        public ProductGetByIdDto Product { get; set; }
        public string AccessToken { get; set; }

        public CrawlerServiceNewProductAddedDto(ProductGetByIdDto product, string accessToken)
        {
            Product = product;

            AccessToken = accessToken;
        }
    }
}
