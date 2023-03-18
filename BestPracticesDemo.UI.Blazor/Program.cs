using BestPracticesDemo.UI.Blazor.Services;
using BestPracticesDemo.UI.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BestPracticesDemo.UI.Blazor.Services.Base;
using BestPracticesDemo.UI.Blazor.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using BestPracticesDemo.UI.Blazor.Auth;
using Blazored.LocalStorage;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();


//builder.Services.AddSingleton(new HttpClient
//{
//    BaseAddress = new Uri("https://localhost:44398")
//});


builder.Services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri("https://localhost:44398"));
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IEventDataService, EventDataService>();
builder.Services.AddScoped<ICategoryDataService, CategoryDataService>();
//builder.Services.AddScoped<IOrderDataService, OrderDataService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
await builder.Build().RunAsync();
