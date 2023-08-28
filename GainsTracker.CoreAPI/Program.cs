using System.Text.Json.Serialization;
using DotNetEnv;
using GainsTracker.Common.Extensions;

namespace GainsTracker.CoreAPI;

public static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        builder.Services.AddEndpointsApiExplorer();

        // Configuration.
        Env.Load();
        builder.RegisterEpicDependencies();
        builder.ConfigureDatabaseAndIdentity();
        builder.ConfigureAuthentication();
        builder.AddSwaggerDocumentation();
        builder.ConfigureCors();
        builder.EnableDataProtection();

        WebApplication app = builder.Build();

        var env = app.Environment;
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment() || env.IsEnvironment("Docker") || env.EnvironmentName == "Staging")
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            
            bool resetDatabase = args.Length > 0 && args[0].ToBool();
            app.ResetAndUpdateDatabase(resetDatabase);
        }

        if (!app.Environment.IsDevelopment())
            app.UseHttpsRedirection();

        app.UseCors("AllowBlazorDevClient");

        // Authentication
        app.UseAuthentication();
        app.UseAuthorization();

        // Custom exception handling
        app.AddGlobalErrorHandler();

        app.MapControllers();

        app.Run();
    }
}
