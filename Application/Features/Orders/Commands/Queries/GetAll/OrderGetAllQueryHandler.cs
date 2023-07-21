using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Commands.Queries.GetAll
{
    public class OrderGetAllQueryHandler : IRequestHandler<OrderGetAllQuery, List<OrderGetAllDto>>
    {
        private readonly IApplicationDbContext _context;

        public OrderGetAllQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderGetAllDto>> Handle(OrderGetAllQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders
                .Select(order => new OrderGetAllDto
                {
                    Id = order.Id,
                    RequestedAmount = order.RequestedAmount,
                    ProductCrawlType = order.ProductCrawlType,
                    CreatedOn= order.CreatedOn

                })
                .ToListAsync(cancellationToken);

            return orders;
        }
    }
}
