using Application.Common.Dtos;
using Application.Common.Interfaces;
using Application.Common.Models.Email;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Services;
using Microsoft.AspNetCore.SignalR.Client;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

public class Program
{
    private static SeleniumLogDto CreateLog(string message)
    {
        return new SeleniumLogDto(message);
    }

    static async Task ProcessRepositoriesAsync(HttpClient client, Order order)
    {
        var data = new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json");
        Console.WriteLine(JsonSerializer.Serialize(order));
        var response = await client.PostAsync(
            "https://localhost:7070/api/Orders/Add", data);

        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine(result);
    }

    public static async Task Main(string[] args)
    {

        Console.WriteLine("Welcome to the E-Commerce Crawler App");
        Console.WriteLine("Enter your e-mail address: ");
        string email = Console.ReadLine();
        Console.WriteLine("Select the Product Crawl Type:");
        Console.WriteLine("0 - All");
        Console.WriteLine("1 - OnDiscount");
        Console.WriteLine("2 - NonDiscount");
        Console.WriteLine("Enter your choice (0-2):");

        ProductCrawlType crawlType;
        while (!Enum.TryParse(Console.ReadLine(), out crawlType) || !Enum.IsDefined(typeof(ProductCrawlType), crawlType))
        {
            Console.WriteLine("Invalid selection. Please try again.");
        }

        Console.WriteLine("Please enter the number of products you want to scrape:");
        string input = Console.ReadLine();
        int numberOfProducts;
        while (!int.TryParse(input, out numberOfProducts) || numberOfProducts <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid number of products.");
            input = Console.ReadLine();
        }

        var order = new Order()
        {
            Id = Guid.NewGuid(),
            RequestedAmount = numberOfProducts,
            ProductCrawlType = crawlType,
        };

        order.OrderEvents = new List<OrderEvent>();
        string appDirectory = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
        IEmailService _emailService = new EmailManager(appDirectory);

        Thread.Sleep(5000);

        new DriverManager().SetUpDriver(new EdgeConfig());

        IWebDriver driver = new EdgeDriver();

        var hubConnection = new HubConnectionBuilder()
            .WithUrl($"https://localhost:7070/Hubs/SeleniumLogHub")
            .WithAutomaticReconnect()
            .Build();

        await hubConnection.StartAsync();

        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

        await ProcessRepositoriesAsync(client, order);

        try
        {
            await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(OrderStatus.BotStarted.ToString()));
            
            var productCrawler = new ProductCrawler(driver, hubConnection);
            await productCrawler.CrawlProducts(order);

            Console.ReadKey();

            await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(OrderStatus.OrderCompleted.ToString()));
            
            _emailService.SendEmailConfirmation(new SendEmailConfirmationDto()
            {
                Email = email,
            });

            driver.Quit();
        }
        catch (Exception exception)
        {

            Console.WriteLine(exception.ToString());
            driver.Quit();
        }
    }
}