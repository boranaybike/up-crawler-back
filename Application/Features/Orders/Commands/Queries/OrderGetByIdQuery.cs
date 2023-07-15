using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Features.Orders.Commands.Queries
{
    public class OrderGetByIdQuery : IRequest<Response<Order>>
    {
        public Guid Id { get; set; }

        public OrderGetByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
