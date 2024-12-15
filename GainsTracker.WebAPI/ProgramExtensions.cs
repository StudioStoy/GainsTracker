// Woah that's a boat load of usings

#region

using System.Reflection;
using System.Security.Claims;
using System.Text;
using DotNetEnv;
using GainsTracker.Infrastructure;
using GainsTracker.Infrastructure.Auth;
using GainsTracker.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

#endregion

namespace GainsTracker.WebAPI;

public static class ProgramExtensions
{
    /// <summary>
    ///     Register all dependencies, scoped as well as transient. Epic.
    /// </summary>
    public static void RegisterEpicDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthServices()
            .AddWorkoutServices()
            .AddHealthMetricServices()
            .AddUserProfileServices()
            .AddFriendServices()
            .AddGainsServices();
    }

    public static void ConfigureDatabaseAndIdentity(this WebApplicationBuilder builder, bool useInMemory = false)
    {
        // Load the appsettings.json configuration file
        builder.Configuration.AddJsonFile("appsettings.json", false, true);

        // Retrieve the connection string with placeholders from appsettings.json
        var connectionStringTemplate = builder.Configuration.GetConnectionString("Development")
                                       ?? throw new InvalidOperationException(
                                           "Connection string 'Development' not found in configuration.");

        // Replace placeholders with environment variable values
        var connectionString = connectionStringTemplate
            .Replace("{host}", Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost")
            .Replace("{database}", Environment.GetEnvironmentVariable("DB_NAME") ?? "gainstracker_db")
            .Replace("{username}", Environment.GetEnvironmentVariable("DB_USER") ?? "stoy")
            .Replace("{password}", Environment.GetEnvironmentVariable("DB_PASS") ?? "gainstracker_local");

        builder.Services.AddDataServices(connectionString, useInMemory);

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
        var bitSecret = builder.Configuration["JWT:Secret"]!.Replace("{secretJWT}", Env.GetString("JWT_SECRET"));
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bitSecret)),
                };
            });
    }

    public static void EnableDataProtection(this WebApplicationBuilder builder)
    {
        builder.Services.AddDataProtection().UseCryptographicAlgorithms(
            new AuthenticatedEncryptorConfiguration
            {
                EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                ValidationAlgorithm = ValidationAlgorithm.HMACSHA256,
            });
    }

    /// <summary>
    ///     Swagger documentation with authentication.
    /// </summary>
    public static void AddSwaggerDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Gains Tracker API",
                Description = "Web API for managing GainsTracker resources, developed by Studio Stoy.",
                Contact = new OpenApiContact
                {
                    Name = "Studio Stoy",
                    Url = new Uri("https://studiostoy.nl"),
                },
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. \r\n" +
                              "Enter 'Bearer' [space] and then your token in the text input below.\n" +
                              "Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                },
            });
            
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }

    /// <summary>
    ///     Useful for debugging.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="execute">Flag to execute this function or not.</param>
    public static void ResetAndUpdateDatabase(this WebApplication app, bool execute = true)
    {
        if (!execute)
            return;

        if (Env.GetString("ASPNETCORE_ENVIRONMENT") == "Production")
            Console.WriteLine("nuh uh no resetting in production");

        var scope = app.Services.CreateScope();
        scope.ResetDatabase();
    }

    public static void EnsureDatabaseIsCreated(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        scope.ApplyMigrationsToDatabase();
    }

    /// <summary>
    ///     When this is added, all exceptions that are thrown are caught and processed by this handler to return the
    ///     correct status code.
    /// </summary>
    public static void AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}
