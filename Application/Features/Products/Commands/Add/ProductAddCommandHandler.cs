using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.Add
{
    public class ProductAddCommandHandler: IRequestHandler<ProductAddCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProductAddCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<int>> Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Picture = request.Picture,
                SalePrice = request.SalePrice,
                IsOnSale = request.IsOnSale,
                OrderId = request.OrderId,
            };

            await _applicationDbContext.Products.AddAsync(product, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<int>($"The searched product  was successfully added.");
        }

       
    }
}
