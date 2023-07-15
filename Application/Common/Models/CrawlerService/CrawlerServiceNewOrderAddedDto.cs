using Application.Features.Orders.Commands.Add;

namespace Application.Common.Models.CrawlerService
{
    public class CrawlerServiceNewOrderAddedDto
    {
        public OrderAddCommand Order { get; set; }
        public string AccessToken { get; set; }

        public CrawlerServiceNewOrderAddedDto(OrderAddCommand order, string accessToken)
        {
            Order = order;

            AccessToken = accessToken;
        }
    }
}