using Application.Common.Dtos;
using Application.Common.Models.CrawlerService;
using Application.Features.Products.Commands.Add;
using Microsoft.AspNetCore.SignalR.Client;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Collections.ObjectModel;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

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
                .WithUrl("https://localhost:7070/Hubs/SeleniumLogHub")
                .WithAutomaticReconnect()
                .Build();
        }

        private SeleniumLogDto CreateLog(string message)
        {
            return new SeleniumLogDto(message);
        }
        private ProductDto CreateProduct(Guid orderId, bool isOnSale, decimal price, decimal salePrice, string picture, string name)
        {
            return new ProductDto(orderId, isOnSale, price, salePrice, picture, name);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _connection.On<CrawlerServiceNewOrderAddedDto>("NewOrderAdded", async (newOrderAddedDto) =>
            {
                Console.WriteLine($"A new order added. Requested amount: {newOrderAddedDto.Order.RequestedAmount}");
                Console.WriteLine($"Access token: {newOrderAddedDto.AccessToken}");

                new DriverManager().SetUpDriver(new EdgeConfig());
                IWebDriver driver = new EdgeDriver();
                Thread.Sleep(5000);

                await _connection.InvokeAsync("SendLogNotificationAsync", CreateLog("Bot started"));

                driver.Navigate().GoToUrl("https://4teker.net/");
                await _connection.InvokeAsync("SendLogNotificationAsync", CreateLog("Navigated to the website"));

                ReadOnlyCollection<IWebElement> NumberOfPage = driver.FindElements(By.CssSelector(".page-item"));


                for (int i = 1; i <= 10; i++)
                {
                    driver.Navigate().GoToUrl($"https://4teker.net/?currentPage={i}");

                    Thread.Sleep(5000);

                    IReadOnlyCollection<IWebElement> card = driver.FindElements(By.CssSelector(".card.h-100"));

                    await _connection.InvokeAsync("SendLogNotificationAsync", CreateLog($"There are {card.Count} products found on page {i}."));

                    foreach (IWebElement cards in card)
                    {
                        IWebElement name = cards.FindElement(By.CssSelector(".fw-bolder.product-name"));

                        bool isProductOnSale(string element)
                        {
                            try
                            {
                                cards.FindElement(By.CssSelector(element));
                                return true;
                            }
                            catch (NoSuchElementException)
                            {
                                return false;
                            }
                        }

                        if (isProductOnSale(".sale-price"))
                        {
                            IWebElement salePrice = cards.FindElement(By.CssSelector(".sale-price"));
                            IWebElement price = cards.FindElement(By.CssSelector(".text-muted.text-decoration-line-through.price"));
                            IWebElement image = cards.FindElement(By.CssSelector(".card-img-top"));
                            decimal.TryParse(price.Text, out decimal priceDecimal);
                            decimal.TryParse(salePrice.Text, out decimal salePriceDecimal);

                            await _connection.InvokeAsync("SendProductAsync", CreateProduct(newOrderAddedDto.Order.Id, true, priceDecimal, salePriceDecimal, image.Text, name.Text));

                        }
                        else
                        {
                            IWebElement price = cards.FindElement(By.CssSelector(".price"));
                            IWebElement image = cards.FindElement(By.CssSelector(".card-img-top"));
                            decimal.TryParse(price.Text, out decimal priceDecimal);
                            Console.WriteLine(image.Text);

                            await _connection.InvokeAsync("SendProductAsync", CreateProduct(newOrderAddedDto.Order.Id, false, priceDecimal, 0, image.Text, name.Text));
                        }
                    }
                }
                // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newOrderAddedDto.AccessToken);
                // var result = await _httpClient.PostAsJsonAsync("Accounts/CrawlerServiceExample", newOrderAddedDto, stoppingToken);
                await _connection.InvokeAsync("SendLogNotificationAsync", CreateLog("Order Completed"));

                driver.Quit();

            });


            await _connection.StartAsync(stoppingToken);
            Console.WriteLine($"SignalR connection state: {_connection.State}");
            Console.WriteLine($"SignalR connection ID: {_connection.ConnectionId}");

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(10000, stoppingToken);
            //}
        }
    }
}
