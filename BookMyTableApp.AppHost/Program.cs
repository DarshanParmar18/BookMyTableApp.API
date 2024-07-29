var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BookMyTableApp_API>("bookmytableapp-api");

builder.Build().Run();
