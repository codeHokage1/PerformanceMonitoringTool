var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.PerformanceMonitoringTool_ApiService>("apiservice");

builder.AddProject<Projects.PerformanceMonitoringTool_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
