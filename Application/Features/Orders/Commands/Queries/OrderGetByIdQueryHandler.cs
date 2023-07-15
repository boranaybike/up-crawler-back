using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Commands.Queries
{
    public class OrderGetByIdQueryHandler : IRequestHandler<OrderGetByIdQuery, Response<Order>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OrderGetByIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<Order>> Handle(OrderGetByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _applicationDbContext.Orders.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (order == null)
            {
                return new Response<Order>($"Order with id {request.Id} not found.");
            }

            return new Response<Order>(order);
        }
    }
}
