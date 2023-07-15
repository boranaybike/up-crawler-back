using Application.Common.Dtos;

namespace Application.Common.Interfaces
{
    public interface ISeleniumService
    {
        Task SaveProductToDatabase(ProductDto product, CancellationToken cancellationToken);
    }
}
