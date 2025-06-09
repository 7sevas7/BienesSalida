using System.Text;
using BienesSalida;
using BienesSalida.Client.Pages;
using BienesSalida.Components;
using BienesSalida.ConexionesBD;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddBlazorBootstrap();


builder.Services.AddAuthentication();
builder.Services.AddAuthentication("Bearer").AddJwtBearer( opt => {
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9L''e[jZ$ZVy).G:#/@?`Ig3ZYmkd^X\""));
    var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters() { 
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = signingKey
    };
});


builder.Services.AddScoped<LocalStorageService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BienesSalida.Client._Imports).Assembly);
app.MapControllers();

app.UseCors(cors => cors.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());


app.Run();
