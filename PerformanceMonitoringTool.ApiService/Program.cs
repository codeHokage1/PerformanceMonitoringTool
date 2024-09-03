using Microsoft.EntityFrameworkCore;
using PerformanceMonitoringTool.ApiService;
using PerformanceMonitoringTool.ApiService.Data;
using PerformanceMonitoringTool.ApiService.Mappings;
using PerformanceMonitoringTool.ApiService.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Add the API explorer.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the database context.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}); 

// Add the controllers.
builder.Services.AddControllers();

// Add the repository.
builder.Services.AddScoped<IMonitoredApps, MonitoredAppRepository>();

// Add the mapping
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Add the hosted service - background monitoring.
builder.Services.AddHostedService<BackgroundMonitoringService>();

// Add the hosted service - consumer.
builder.Services.AddHostedService<ConsumerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthorization();

app.MapDefaultEndpoints();

app.Run();

