using BeautySalon.Infrastructure.InMemory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<BeautySalonDataStore>();

builder.Services.AddTransient<ClientRepository>();
builder.Services.AddTransient<MasterRepository>();
builder.Services.AddTransient<ServiceRepository>();
builder.Services.AddTransient<AppointmentRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();