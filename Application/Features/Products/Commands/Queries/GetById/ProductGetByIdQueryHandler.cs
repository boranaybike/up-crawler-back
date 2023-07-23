using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Commands.Queries.GetById
{
    public class ProductGetByIdQueryHandler: IRequestHandler<ProductGetByIdQuery, ProductGetByIdDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProductGetByIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ProductGetByIdDto> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _applicationDbContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return ProductGetByIdDtoMapper(product);
        }

        private ProductGetByIdDto ProductGetByIdDtoMapper(Product ?product)
        {
            return new ProductGetByIdDto()
            {
                Id = product.Id,
                OrderId = (Guid)product.OrderId,
                Name  = product.Name,
                Picture = product.Picture,
                IsOnSale = product.IsOnSale,
                Price = product.Price,
                SalePrice = product.SalePrice
            };
        }
    }
}
