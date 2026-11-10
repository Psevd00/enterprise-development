var builder = DistributedApplication.CreateBuilder(args);

// Ссылка на Scalar появится рядом с сервисом в Aspire Dashboard
builder.AddProject<Projects.BeautySalon_Api>("beautysalon-api")
    .WithUrl("/scalar", "Scalar");

builder.Build().Run();
