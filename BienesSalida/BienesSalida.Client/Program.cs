using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddBlazorBootstrap();
/*builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
});*/
builder.Services.AddScoped( sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

Console.WriteLine(builder.HostEnvironment.BaseAddress);
await builder.Build().RunAsync();
