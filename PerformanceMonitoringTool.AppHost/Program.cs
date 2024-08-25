var builder = DistributedApplication.CreateBuilder(args);

// Add a SQL Server resource
var sqlServerResource = builder.AddSqlServer("sql")
    .AddDatabase("PWC-PMT");

var apiService = builder.AddProject<Projects.PerformanceMonitoringTool_ApiService>("apiservice")
                        .WithReference(sqlServerResource);

builder.AddProject<Projects.PerformanceMonitoringTool_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
