// God that's a boat load of usings
using System.Security.Claims;
using System.Text;
using GainsTrackerAPI.Components.Friends.Data;
using GainsTrackerAPI.Components.Friends.Services;
using GainsTrackerAPI.Components.Gains.Data;
using GainsTrackerAPI.Components.Gains.Services;
using GainsTrackerAPI.Components.Security.Models;
using GainsTrackerAPI.Components.Security.Services;
using GainsTrackerAPI.Configurations.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GainsTrackerAPI.Configurations;

public static class ProgramBuilderExtensions
{
    /// <summary>
    ///     Register all dependencies, scoped as well as transient.
    /// </summary>
    public static void RegisterEpicDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IGainsService, GainsService>();
        builder.Services.AddScoped<IMeasurementService, MeasurementService>();
        builder.Services.AddScoped<IFriendService, FriendService>();
        builder.Services.AddScoped<BigBrainFriends>();
        builder.Services.AddScoped<BigBrainWorkout>();
    }

    /// <summary>
    ///     Register the AppDbContext and map Identity to the user.
    ///     Also configure user account options (password, lockout settings, etc.)
    /// </summary>
    public static void ConfigureContextAndIdentity(this WebApplicationBuilder builder)
    {
        // Set DbContext.
        builder.Services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(builder.Configuration.GetConnectionString("databaseConnection")); });

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
            options.AddPolicy("AllowAngularDevClient",
                b =>
                {
                    b
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }

    /// <summary>
    ///     Add Authentication with JWT.
    /// </summary>
    /// <param name="builder"></param>
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
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
}
