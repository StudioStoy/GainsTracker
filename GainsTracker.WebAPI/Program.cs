using System.Text.Json.Serialization;
using DotNetEnv;
using GainsTracker.Common.Extensions;
using GainsTracker.WebAPI;

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
builder.ConfigureDatabaseAndIdentity();
builder.ConfigureAuthentication();
builder.AddSwaggerDocumentation();
builder.ConfigureCors();
builder.EnableDataProtection();


var app = builder.Build();

// Configure the HTTP request pipeline.
var env = app.Environment;
var resetDatabase = args.Length > 0 && args[0].ToBool();
if (env.IsDevelopment() || env.IsEnvironment("Docker") || env.EnvironmentName == "Staging")
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ResetAndUpdateDatabase(resetDatabase);
}

if (!resetDatabase)
    app.EnsureDatabaseIsCreated();

if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseCors("AllowBlazorDevClient");

// Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
