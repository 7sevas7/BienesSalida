using BienesSalida.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddBlazorBootstrap();
//builder.Services.AddScoped<HttpClient>( sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<LocalStorageService>();

builder.Services.AddScoped(sp =>
{
    var nav = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri("https://172.16.9.10/ControlSalidaBienes") };
    //return new HttpClient { BaseAddress = new Uri(nav.BaseUri) };
});

await builder.Build().RunAsync();
