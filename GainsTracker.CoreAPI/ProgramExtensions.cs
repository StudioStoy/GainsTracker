// Woah that's a boat load of usings

using System.Security.Claims;
using System.Text;
using DotNetEnv;
using GainsTracker.CoreAPI.Components.HealthMetrics.Data;
using GainsTracker.CoreAPI.Components.HealthMetrics.Services;
using GainsTracker.CoreAPI.Components.Security.Models;
using GainsTracker.CoreAPI.Components.Security.Services;
using GainsTracker.CoreAPI.Components.UserProfiles.Data;
using GainsTracker.CoreAPI.Components.UserProfiles.Services;
using GainsTracker.CoreAPI.Components.Workouts.Data;
using GainsTracker.CoreAPI.Components.Workouts.Services;
using GainsTracker.CoreAPI.Database;
using GainsTracker.CoreAPI.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GainsTracker.CoreAPI;

public static class ProgramExtensions
{
    /// <summary>
    ///     Register all dependencies, scoped as well as transient. Epic.
    /// </summary>
    public static void RegisterEpicDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IGainsService, GainsService>();
        builder.Services.AddScoped<IMeasurementService, MeasurementService>();
        builder.Services.AddScoped<IHealthMetricService, HealthMetricService>();
        builder.Services.AddScoped<ICatalogService, CatalogService>();
        builder.Services.AddScoped<IUserProfileService, UserProfileService>();
        builder.Services.AddScoped<BigBrainWorkout>();
        builder.Services.AddScoped<BigBrainHealthMetric>();
        builder.Services.AddScoped<BigBrainUserProfile>();
    }

    /// <summary>
    ///     Register the AppDbContext and map Identity to the user.
    ///     Also configure user account options (password, lockout settings, etc.)
    /// </summary>
    public static void ConfigureDatabaseAndIdentity(this WebApplicationBuilder builder)
    {
        // Load the appsettings.json file
        builder.Configuration.AddJsonFile("appsettings.json", true, true);

        string? connection = builder.Configuration
            .GetSection($"ConnectionStrings:{builder.Environment.EnvironmentName}")
            .Value;
        builder.Configuration.GetSection("ConnectionStrings:connection").Value = connection;

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("connection")!
                .Replace("{host}", Env.GetString("DB_HOST"))
                .Replace("{database}", Env.GetString("DB_NAME"))
                .Replace("{password}", Env.GetString("DB_PASS"))
                .Replace("{username}", Env.GetString("DB_USER"))
            );
        });

        // Map Identity to User and the database.
        builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;

            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 5;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+!#$^";
            options.User.RequireUniqueEmail = false;
        });
    }

    /// <summary>
    ///     Add CORS policy for local development and testing purposes.
    /// </summary>
    public static void ConfigureCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowBlazorDevClient",
                b =>
                {
                    b
                        .WithOrigins("https://localhost:7093", "http://localhost:5027", "http://localhost:5027")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }

    /// <summary>
    ///     Add Authentication with JWT.
    /// </summary>
    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Add the JWT Bearer.
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
                };
            });
    }

    public static void EnableDataProtection(this WebApplicationBuilder builder)
    {
        builder.Services.AddDataProtection().UseCryptographicAlgorithms(
            new AuthenticatedEncryptorConfiguration
            {
                EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            });
    }

    /// <summary>
    ///     Swagger documentation with authentication.
    /// </summary>
    public static void AddSwaggerDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Gains Tracker API", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. \r\n" +
                              "Enter 'Bearer' [space] and then your token in the text input below.\n" +
                              "Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });
    }

    /// <summary>
    ///     Useful for debugging.
    /// </summary>
    /// <param name="execute">Flag to execute this function or not.</param>
    public static void ResetAndUpdateDatabase(this WebApplication app, bool execute = true)
    {
        if (!execute)
            return;

        if (Env.GetString("ASPNETCORE_ENVIRONMENT") == "Production")
        {
            Console.WriteLine("nuh uh no resetting in production");
            return;
        }
            
        using IServiceScope scope = app.Services.CreateScope();
        AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        Console.WriteLine("Resetting database..");
        db.Database.EnsureDeleted();
        db.Database.Migrate();
    }

    public static void EnsureDatabaseIsCreated(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        Console.WriteLine("Applying possible migrations..");
        db.Database.Migrate();
    }

    /// <summary>
    ///     Configure global exception handling.
    /// </summary>
    public static void AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}
