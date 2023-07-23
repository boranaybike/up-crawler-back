using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Commands.Queries
{
    public class OrderGetByIdQueryHandler : IRequestHandler<OrderGetByIdQuery, OrderGetByIdDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OrderGetByIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<OrderGetByIdDto> Handle(OrderGetByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _applicationDbContext.Orders.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return OrderGetByIdDtoMapper(order);
        }
        private OrderGetByIdDto OrderGetByIdDtoMapper(Order order)
        {
            return new OrderGetByIdDto()
            {
                Id = order.Id,
                RequestedAmount = order.RequestedAmount,
                ProductCrawlType = order.ProductCrawlType,
                CreatedOn = order.CreatedOn,
            };
        }
    }
}
