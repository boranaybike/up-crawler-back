using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Orders.Commands.Add
{
    public class OrderAddCommand : IRequest<Response<Guid>>
    {
        public int RequestedAmount { get; set; }
        //public int TotalFoundAmount { get; set; }
        public string ?ProductCrawlType { get; set; }
        //public ICollection<OrderEvent>? OrderEvents { get; set; } // Bot started...
    }
}
