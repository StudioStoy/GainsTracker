using System.Text.Json.Serialization;
using DotNetEnv;
using GainsTracker.Common.Extensions;
using GainsTracker.WebAPI;

var resetDatabase = args.Contains("-reset");
var useInMemoryDatabase = args.Contains("-inmemory");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

// Configuration.
Env.Load();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add domain services
builder.RegisterEpicDependencies();
builder.ConfigureDatabaseAndIdentity(useInMemoryDatabase);
builder.ConfigureAuthentication();
builder.AddSwaggerDocumentation();
builder.ConfigureCors();
builder.EnableDataProtection();


var app = builder.Build();

// Configure the HTTP request pipeline.
var env = app.Environment;
if (env.IsDevelopment() || env.IsEnvironment("Docker") || env.EnvironmentName == "Staging")
{
    app.UseSwagger();
    app.UseSwaggerUI();

    if (!useInMemoryDatabase)
        app.ResetAndUpdateDatabase(resetDatabase);
}

if (!resetDatabase && !useInMemoryDatabase)
    app.EnsureDatabaseIsCreated();

if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseCors("AllowBlazorDevClient");

// Authentication
app.UseAuthentication();
app.UseAuthorization();

app.AddGlobalErrorHandler();

app.MapControllers();

app.Run();
