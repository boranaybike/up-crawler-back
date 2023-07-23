using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Commands.Queries.GetAll
{
    public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, List<ProductGetAllDto>>
    {
        private readonly IApplicationDbContext _context;

        public ProductGetAllQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductGetAllDto>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Select(product => new ProductGetAllDto
                {
                    Id = product.Id,
                    OrderId = (Guid)product.OrderId,
                    Name = product.Name,
                    Picture = product.Picture,
                    IsOnSale = product.IsOnSale,
                    Price = product.Price,
                    SalePrice = product.SalePrice
                })
                .ToListAsync(cancellationToken);

            return products;
        }
    }
}
