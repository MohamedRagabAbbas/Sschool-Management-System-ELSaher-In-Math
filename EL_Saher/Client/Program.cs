global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using EL_Saher.Client;
using EL_Saher.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IManager, Manager>();
builder.Services.AddScoped<IUserServices, UserServices>();


builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();



await builder.Build().RunAsync();
