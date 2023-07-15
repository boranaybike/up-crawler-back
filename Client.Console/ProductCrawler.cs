using Application.Common.Dtos;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.SignalR.Client;
using OpenQA.Selenium;
using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json;

public class ProductCrawler
{
    private IWebDriver driver;
    private HubConnection hubConnection;

    public ProductCrawler(IWebDriver driver, HubConnection hubConnection)
    {
        this.driver = driver;
        this.hubConnection = hubConnection;
    }

    static async Task ProcessRepositoriesAsync(HttpClient client, ProductDto product)
    {
        var data = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
        Console.WriteLine(JsonSerializer.Serialize(product));
        var response = await client.PostAsync(
            "https://localhost:7070/api/Products/Add", data);

        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine(result);
    }

    private SeleniumLogDto CreateLog(string message)
    {
        return new SeleniumLogDto(message);
    }

    public async Task CrawlProducts(Order order)
    {
        await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(OrderStatus.CrawlingStarted.ToString()));

        int pageNumber = 1;
        int productCount = 0;
        int printedProductCount = 0;

        while (pageNumber <= 10 && printedProductCount < order.RequestedAmount)
        {
            driver.Navigate().GoToUrl($"http://finalproject.dotnet.gg/?currentPage={pageNumber}");
            Thread.Sleep(4000);

            List<IWebElement> productPictures = driver.FindElements(By.CssSelector(".card-img-top")).ToList();
            List<IWebElement> productNames = driver.FindElements(By.CssSelector(".fw-bolder.product-name")).ToList();
            List<IWebElement> salePrices = new List<IWebElement>();
            List<IWebElement> normalPrices = new List<IWebElement>();
            List<IWebElement> discountPrices = new List<IWebElement>();

            if (order.ProductCrawlType == ProductCrawlType.All || order.ProductCrawlType == ProductCrawlType.OnDiscount)
            {
                salePrices = driver.FindElements(By.CssSelector(".sale-price")).ToList();
                discountPrices = driver.FindElements(By.CssSelector(".text-muted.text-decoration-line-through.price")).ToList();
            }

            if (order.ProductCrawlType == ProductCrawlType.All || order.ProductCrawlType == ProductCrawlType.NonDiscount)
            {
                normalPrices = driver.FindElements(By.CssSelector(".price")).ToList();
            }

            await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Navigated to next page."));
            var count = 0;
            if (order.ProductCrawlType == ProductCrawlType.All)
            {
                count = productNames.Count;
            }
            else if (order.ProductCrawlType == ProductCrawlType.OnDiscount)
            {
                count = salePrices.Count;
            }
            else if (order.ProductCrawlType == ProductCrawlType.NonDiscount)
            {
                count = normalPrices.Count;
            }

            for (int i = 0; i < count && printedProductCount < order.RequestedAmount; i++)
            {
                bool isOnSale = false;
                decimal price = 0;
                decimal salePrice = 0;

                if (salePrices.Count > i)
                {
                    string salePriceString = salePrices[i].Text;
                    string discountPriceString = discountPrices[i].Text;
                    Decimal.TryParse(salePriceString, NumberStyles.Currency, CultureInfo.CurrentCulture, out price);
                    Decimal.TryParse(discountPriceString, NumberStyles.Currency, CultureInfo.CurrentCulture, out salePrice);
                    isOnSale = true;
                }

                else if (normalPrices.Count > i)
                {
                    string priceString = normalPrices[i].Text;
                    Decimal.TryParse(priceString, NumberStyles.Currency, CultureInfo.CurrentCulture, out price);
                }

                Console.WriteLine(printedProductCount + " ==> " + productNames[i].Text);


                string name = productNames[i].Text;
                string image = productPictures[i].GetAttribute("src");
                printedProductCount++;

                productCount++;

                ProductDto productDto = new ProductDto(order.Id, isOnSale, price, salePrice, image, name);

                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

                await ProcessRepositoriesAsync(client, productDto);

                await hubConnection.InvokeAsync("SendProductAsync", productDto);
            }

            pageNumber++;
            Thread.Sleep(2500);
        }
    }
}
