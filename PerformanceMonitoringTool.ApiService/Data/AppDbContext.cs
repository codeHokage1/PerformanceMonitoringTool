using Microsoft.EntityFrameworkCore;
using PerformanceMonitoringTool.ApiService.Models.Entities;

namespace PerformanceMonitoringTool.ApiService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }

        public DbSet<MonitoredApp> MonitoredApps { get; set; }
    }
}
