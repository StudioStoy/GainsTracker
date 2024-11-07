using System.Text.Json.Serialization;

namespace GainsTracker.Data;

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

        // Configure the HTTP request pipeline.
        var env = app.Environment;
        bool resetDatabase = args.Length > 0 && args[0].ToBool();
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

        // Custom exception handling
        app.AddGlobalErrorHandler();

        app.MapControllers();

        app.Run();
    }
}
