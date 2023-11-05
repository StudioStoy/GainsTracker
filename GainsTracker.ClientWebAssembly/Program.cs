using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GainsTracker.ClientWebAssembly;
using GainsTracker.UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IGainsTrackerService, GainsTrackerService>();
builder.Services.AddScoped<IGainsAuthService, DummyAuthService>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddScoped<BlazorTransitionableRoute.IRouteTransitionInvoker, BlazorTransitionableRoute.DefaultRouteTransitionInvoker>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
