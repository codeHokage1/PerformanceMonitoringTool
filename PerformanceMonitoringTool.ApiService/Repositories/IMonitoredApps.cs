using PerformanceMonitoringTool.ApiService.Models.Entities;
using PerformanceMonitoringTool.ApiService.Models.DTOs;

namespace PerformanceMonitoringTool.ApiService.Repositories
{
    public interface IMonitoredApps
    {
        Task<List<MonitoredApp>> GetMonitoredAppsAsync();
        Task<MonitoredApp?> GetMonitoredAppByIdAsync(Guid id);
        Task<MonitoredApp?> GetMonitoredAppByAppIdAsync(string name);
        Task<MonitoredApp> AddMonitoredAppAsync(MonitoredApp addMonitoredApp);
        Task<MonitoredApp?> UpdateMonitoredAppAsync(Guid id, MonitoredApp updateMonitoredApp);
        Task<MonitoredApp?> RemoveMonitoredAppAsync(Guid id);
    }
}
