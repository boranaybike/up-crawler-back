using Application.Features.Orders.Commands.Queries;

namespace Application.Common.Models.CrawlerService
{
    public class CrawlerServiceNewOrderAddedDto
    {
        public OrderGetByIdDto Order { get; set; }
        public string AccessToken { get; set; }

        public CrawlerServiceNewOrderAddedDto(OrderGetByIdDto order, string accessToken)
        {
            Order = order;

            AccessToken = accessToken;
        }
    }
}