using Domain.Enums;

namespace Application.Features.Orders.Commands.Queries
{
    public class OrderGetByIdDto
    {
        public Guid Id { get; set; }
        public int RequestedAmount { get; set; }
        public ProductCrawlType? ProductCrawlType { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
