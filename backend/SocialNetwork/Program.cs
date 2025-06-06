using System.Text.Json.Serialization;
using FilmMatch.Persistence.Configurations.FilmMatch.Persistence;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Persistence.Extensions;

namespace FilmMatch;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Конфигурация сервисов ДО builder.Build()
        builder.Services.AddPersistenceLayer(builder.Configuration);
        var configuration = builder.Configuration;
        builder.Services.AddSingleton(configuration);

        builder.Services.AddDbContext<ApplicationDBContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Добавляем сервисы Swagger до Build()
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Добавляем контроллеры и настройки JSON
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        var app = builder.Build();

        // Конфигурация middleware ПОСЛЕ Build()
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.MapControllers(); // Не забудьте добавить маршрутизацию контроллеров

        app.Run();
    }
}