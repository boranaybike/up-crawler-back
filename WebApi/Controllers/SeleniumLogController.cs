using Application.Features.SeleniumLogs.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeleniumLogController : ApiControllerBase
    {
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAllAsync(SeleniumLogGetAllQuery query)
        {
            return Ok(await Mediator.Send(query));

        }
    }
}
