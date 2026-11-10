using System.Text.Json.Serialization;
using BeautySalon.Application.Services;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Domain.Interfaces;
using BeautySalon.Infrastructure.InMemory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddOpenApi();

builder.Services.AddSingleton<BeautySalonDataStore>();
builder.Services.AddSingleton<IClientRepository, ClientRepository>();
builder.Services.AddSingleton<IMasterRepository, MasterRepository>();
builder.Services.AddSingleton<IServiceRepository, ServiceRepository>();
builder.Services.AddSingleton<IAppointmentRepository, AppointmentRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IMasterService, MasterService>();
builder.Services.AddScoped<IServiceService, SalonServiceService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Beauty Salon API v1"));
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();