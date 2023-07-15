using Application.Common.Models.CrawlerService;
using Microsoft.AspNetCore.SignalR.Client;

namespace CrawlerWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HubConnection _connection;
        private readonly HttpClient _httpClient;

        public Worker(ILogger<Worker> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;

            _connection = new HubConnectionBuilder()
                .WithUrl($"https://localhost:7070/Hubs/SeleniumLogHub")
                .WithAutomaticReconnect()
                .Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _connection.On<CrawlerServiceNewOrderAddedDto>("NewOrderAdded", async (NewOrderAddedDto) =>
            {
                Console.WriteLine($"A new account added. Account Name is {NewOrderAddedDto.Order.RequestedAmount}");
                Console.WriteLine($"Our access token is {NewOrderAddedDto.AccessToken}");
                //new DriverManager().SetUpDriver(new EdgeConfig());
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", NewOrderAddedDto.AccessToken);
                //var result = await _httpClient.PostAsJsonAsync("Accounts/CrawlerServiceExample", NewOrderAddedDto, stoppingToken);
            });

            await _connection.StartAsync(stoppingToken);

            Console.WriteLine(_connection.State.ToString());
            Console.WriteLine(_connection.ConnectionId);

            //while (!stoppingToken.IsCancellationRequested)
            //{

            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}

        }
    }
}