using System.Text;
using System.Text.Json.Serialization;
using FilmMatch.Application.Extensions;
using FilmMatch.Application.Interfaces;
using FilmMatch.Authentication;
using FilmMatch.Infrastructure.Extensions;
using FilmMatch.Infrastructure.Services;
using FilmMatch.Persistence;
using FilmMatch.Persistence.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using S3;
using FilmMatch.Middleware;

namespace FilmMatch;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Конфигурация сервисов ДО builder.Build()
        builder.Services.AddPersistenceLayer(builder.Configuration);
        builder.Services.AddInfrastructureLayer();
        builder.Services.AddApplicationLayer();
        builder.Services.AddS3Storage(builder.Configuration.GetSection("Application:S3").Get<S3Options>());
        
        builder.Services.AddHttpContextAccessor();
        
        builder.Services.AddUserContext();
        var configuration = builder.Configuration;
        builder.Services.AddSingleton(configuration);
        
        
        // Вынести в отдельный класс
        // Настройки сваггера
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(configuration["Jwt:Scheme"], new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = configuration["Jwt:Scheme"]
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = configuration["Jwt:Scheme"]
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        
        // Добавление Identity
        builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // Добавление аутентификации
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
                // // Добавлено для поддержки аутентификации SignalR через query string
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notificationhub"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

        builder.Services.AddDbContext<IDbContext, ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Добавляем сервисы Swagger до Build()
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Добавляем контроллеры и настройки JSON
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        var app = builder.Build();

        // Конфигурация middleware ПОСЛЕ Build()
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();
        // Настройка статических файлов
        app.UseStaticFiles(new StaticFileOptions
        {
            ServeUnknownFileTypes = true,
            DefaultContentType = "application/octet-stream"
        });

        app.MapControllers();
        app.MapHub<NotificationHub>("/notificationhub");

        app.Run();
    }
}