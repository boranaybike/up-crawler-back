using Application.Features.Products.Commands.Add;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(ProductAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
