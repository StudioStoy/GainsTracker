using System.Text.Json.Serialization;
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
        builder.RegisterEpicDependencies();
        builder.ConfigureContextAndIdentity();
        builder.ConfigureAuthentication();
        builder.AddSwaggerDocumentation();
        builder.ConfigureCors();
        builder.EnableDataProtection();

        WebApplication app = builder.Build();

        var resetDatabase = args.Length > 0 && args[0].ToBool();
        app.ResetAndUpdateDatabase(resetDatabase);
        if (!resetDatabase)
            app.EnsureDatabaseIsCreated();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
        {
            app.UseSwagger();
            app.UseSwaggerUI();
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
