using Application.Common.Models.CrawlerService;
using Application.Features.Orders.Commands.Add;
using Application.Features.Orders.Commands.Queries;
using Application.Features.Products.Commands.Add;
using Application.Features.Products.Commands.Queries.GetById;
using Application.Features.SeleniumLogs.Add;
using Application.Features.SeleniumLogs.Queries.GetById;
using MediatR;
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
        public async Task SendLogNotificationAsync(SeleniumLogAddCommand command)

        {
            var logId = await Mediator.Send(command);

            var seleniumLogGetById = await Mediator.Send(new SeleniumLogGetByIdQuery(logId.Data));
            await Clients.AllExcept(Context.ConnectionId).SendAsync("NewSeleniumLogAdded", seleniumLogGetById);
        }

        public async Task SendProductAsync(ProductAddCommand command)
        {            
            var productId = await Mediator.Send(command);

            var productGetById = await Mediator.Send(new ProductGetByIdQuery(productId.Data));

            await Clients.AllExcept(Context.ConnectionId).SendAsync("NewProductAdded", productGetById);

        }
        protected ISender Mediator => _mediator ??= _contextAccessor.HttpContext.RequestServices.GetRequiredService<ISender>();
        
        public async Task<Guid> AddANewOrder(OrderAddCommand orderAddCommand)
        {
            var accessToken = Context.GetHttpContext().Request.Query["access_token"];

            var orderId = await Mediator.Send(orderAddCommand);

            var orderGetById = await Mediator.Send(new OrderGetByIdQuery(orderId.Data));

            await Clients.All.SendAsync("NewOrderAdded", new CrawlerServiceNewOrderAddedDto(orderGetById, accessToken));

            return orderId.Data;
        }
    }
}
