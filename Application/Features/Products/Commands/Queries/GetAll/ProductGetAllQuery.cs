using MediatR;

namespace Application.Features.Products.Commands.Queries.GetAll
{
    public class ProductGetAllQuery : IRequest<List<ProductGetAllDto>>
    {
    }
}
