﻿using Application.Common.Models.CrawlerService;
using Application.Features.Orders.Commands.Add;
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

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrderAsync(CrawlerServiceNewOrderAddedDto newOrderAddedDto)
        {
            return Ok();
        }
    }
}
