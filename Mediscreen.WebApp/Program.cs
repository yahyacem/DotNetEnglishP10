using Mediscreen.Shared.Services;
using Mediscreen.WebApp.Services;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Client;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add environment variables.
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.ConfigureMapper();
builder.Services.ConfigureHttpClient();

// The following lines of code adds the ability to authenticate users of this web app.
builder.Services.AddMicrosoftIdentityWebAppAuthentication(configuration)
                    .EnableTokenAcquisitionToCallDownstreamApi(configuration.GetSection("GatewayAPI:Scopes").Get<string[]>())
                    .AddInMemoryTokenCaches();

// Add APIs.
builder.Services.AddDownstreamApi("GatewayAPI", configuration.GetSection("GatewayAPI"));

// Dependency Injection
builder.Services.AddSingleton<ConstantsService>();
builder.Services.AddScoped<IApiService, ApiService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
