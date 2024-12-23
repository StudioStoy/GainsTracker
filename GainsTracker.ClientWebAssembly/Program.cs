#region

using BlazorTransitionableRoute;
using GainsTracker.ClientWebAssembly;
using GainsTracker.UI.Services.API;
using GainsTracker.UI.Services.API.Interfaces;
using GainsTracker.UI.Services.Auth;
using GainsTracker.UI.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

#endregion

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Local dummy data or an actual internet connection to the GainsTracker backend REST API's.
if (builder.Configuration.GetValue<bool>("LOCAL_APIS"))
{
    builder.Services.AddScoped<IGainsAuthService, DummyAuthService>();
    builder.Services.AddScoped<IGainsTrackerService, DummyGainsTrackerService>();
}
else
{
    builder.Services.AddScoped<IGainsAuthService, GainsAuthService>();
    builder.Services.AddScoped<IGainsTrackerService, GainsTrackerService>();
}

builder.Services.AddSingleton<HttpClient>();
builder.Services
    .AddScoped<IRouteTransitionInvoker,
        DefaultRouteTransitionInvoker>();
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
