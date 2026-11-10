using System.Text.Json.Serialization;
using BeautySalon.Api.ExceptionHandlers;
using BeautySalon.Application;
using BeautySalon.Infrastructure.InMemory;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        // Перечисления (пол, специализация, категория) передаются строками: "Female", "Hairdresser"
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddOpenApi();

// Для перехода на БД достаточно заменить AddInMemoryStorage() на регистрацию из Infrastructure.MsSqlServer
builder.Services.AddInMemoryStorage();
builder.Services.AddApplication();

// Единое место, где исключения слоя Application превращаются в HTTP-ответы (404, 400)
builder.Services.AddExceptionHandler<ApplicationExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    // Описание API (/openapi/v1.json) и интерфейс Scalar (/scalar)
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();

/// <summary>
/// Точка входа API салона красоты. Класс открыт для интеграционных тестов.
/// </summary>
public partial class Program
{
}
