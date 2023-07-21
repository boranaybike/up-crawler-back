using MediatR;

namespace Application.Features.Orders.Commands.Queries.GetAll
{
    public class OrderGetAllQuery : IRequest<List<OrderGetAllDto>>
    {
    }
}
