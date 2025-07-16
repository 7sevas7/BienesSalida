using System.Text;
using BienesSalida.Client;
using BienesSalida.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ? Configuración de CORS
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
*/
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:7266/") // Usa tu origen exacto
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // Importante si usas cookies/autenticación
    });
});

// ? Servicios necesarios para la API
builder.Services.AddControllers();


// ? Configuración de Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddBlazorBootstrap();
builder.Services.AddScoped(sp =>
{
    var nav = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri("https://172.16.9.10/ControlSalidaBienes") };
    //return new HttpClient { BaseAddress = new Uri(nav.BaseUri) };
});
// ? Configuración de autenticación con JWT
builder.Services.AddAuthentication("Bearer").AddJwtBearer(opt =>
{
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9L''e[jZ$ZVy).G:#/@?`Ig3ZYmkd^X\""));
    var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = signingKey
    };
});

// ? Servicio local de almacenamiento
builder.Services.AddScoped<LocalStorageService>();
//builder.Services.AddSingleton<LocalStorageService>();

var app = builder.Build();

// ? Configuración del pipeline
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// ? Aplicar CORS antes de la configuración de rutas
app.UseCors("CorsPolicy");
//app.UsePathBase("/ControlSalidaBienes");
//app.UseRouting();
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BienesSalida.Client._Imports).Assembly);

app.Run();


