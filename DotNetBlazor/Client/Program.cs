using DotNetBlazor.Client;
using DotNetBlazor.Client.Services;
using DotNetBlazor.Client.Utility;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:3000") });
builder.Services.AddScoped<ICacheHelper, CacheHelper>();
builder.Services.AddScoped<IAlertService, AlertService>();
builder.Services.AddScoped<IApiHelper, ApiHelper>();
builder.Services.AddScoped<IEventHelper, EventHelper>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProfieService, ProfileService>();

await builder.Build().RunAsync();
