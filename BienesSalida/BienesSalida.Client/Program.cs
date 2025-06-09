using BienesSalida.Client.Pages;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddBlazorBootstrap();
builder.Services.AddScoped<HttpClient>( sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<LocalStorageService>();
await builder.Build().RunAsync();
