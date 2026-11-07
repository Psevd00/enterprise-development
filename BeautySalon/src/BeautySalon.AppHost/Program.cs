var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BeautySalon_Api>("beautysalon-api");

builder.Build().Run();
