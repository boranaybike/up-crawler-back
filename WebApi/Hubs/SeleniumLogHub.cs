using Application.Common.Dtos;
using Application.Common.Models.CrawlerService;
using Application.Features.Orders.Commands.Add;
using Application.Features.Orders.Commands.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    public class SeleniumLogHub:Hub
    {
        private ISender? _mediator; 
        private readonly IHttpContextAccessor _contextAccessor;        

        public SeleniumLogHub(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public async Task SendLogNotificationAsync(SeleniumLogDto log)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("NewSeleniumLogAdded", log);
        }

        public async Task SendProductAsync(ProductDto product)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("NewProductAdded", product);
        }
        public async Task ConsoleLogDeneme(string msg)
        
        {
            Console.WriteLine("console deneme");
           
         }
        protected ISender Mediator => _mediator ??= _contextAccessor.HttpContext.RequestServices.GetRequiredService<ISender>();
        
        [Authorize]
        public async Task AddANewOrder(OrderAddCommand orderAddCommand)
        {
            var accessToken = Context.GetHttpContext().Request.Query["access_token"];

            var orderId = await Mediator.Send(orderAddCommand);

            var orderGetById = await Mediator.Send(new OrderGetByIdQuery(orderId.Data));

            await Clients.All.SendAsync("NewOrderAdded", new CrawlerServiceNewOrderAddedDto(orderGetById, accessToken));

        }
    }
}
