using MediatR;

namespace Application.Features.Orders.Commands.Queries
{
    public class OrderGetByIdQuery : IRequest<OrderGetByIdDto>
    {
        public Guid Id { get; set; }

        public OrderGetByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
