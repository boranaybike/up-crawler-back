using MediatR;

namespace Application.Features.Products.Commands.Queries.GetById
{
    public class ProductGetByIdQuery : IRequest<ProductGetByIdDto>
    {
        public Guid Id { get; set; }

        public ProductGetByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
