using Application.Features.Orders.Commands.Add;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ApiControllerBase
    {
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(OrderAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAllAsync(OrderGetAllQuery query)
        {
            return Ok(await Mediator.Send(query));

        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAsync(OrderDeleteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
