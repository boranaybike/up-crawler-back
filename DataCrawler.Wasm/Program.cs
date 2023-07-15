using Application.Common.Interfaces;
using DataCrawler.Wasm;
using DataCrawler.Wasm.Services;
using Domain.Services;
using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseMySql(builder.Configuration.GetConnectionString("MariaDB")));

//builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());


var apiUrl = builder.Configuration.GetSection("ApiUrl").Value!;

var signalRUrl = builder.Configuration.GetSection("SignalRUrl").Value!;


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });

builder.Services.AddSingleton<IUrlHelperService>(new UrlHelperService(signalRUrl));

//builder.Services.AddSingleton(signalRUrl);


await builder.Build().RunAsync();
