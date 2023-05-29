using System.Text.Json.Serialization;
using GainsTracker.CoreAPI.Configurations;

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
        builder.ConfigureContextAndIdentity();
        builder.ConfigureAuthentication();
        builder.AddSwaggerDocumentation();
        builder.ConfigureCors();
        builder.RegisterEpicDependencies();

        WebApplication app = builder.Build();

        app.ResetAndUpdateDatabase();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowBlazorDevClient");
        app.UseHttpsRedirection();

        // Authentication
        app.UseAuthentication();
        app.UseAuthorization();

        // Custom exception handling
        app.AddGlobalErrorHandler();

        app.MapControllers();

        app.Run();
    }
}
