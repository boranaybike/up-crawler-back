using Application.Common.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    public class SeleniumLogHub:Hub
    {
        public async Task SendLogNotificationAsync(SeleniumLogDto log)
        {
           await Clients.AllExcept(Context.ConnectionId).SendAsync("NewSeleniumLogAdded", log);
        }
        public async Task SendProductAsync(ProductDto product)
        {

           await Clients.AllExcept(Context.ConnectionId).SendAsync("NewProductAdded", product);

        }
    }
}
