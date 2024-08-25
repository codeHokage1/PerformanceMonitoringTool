using PerformanceMonitoringTool.ApiService.Models.Entities;
using PerformanceMonitoringTool.ApiService.Models.DTOs;

namespace PerformanceMonitoringTool.ApiService.Repositories
{
    public interface IMonitoredApps
    {
        Task<List<MonitoredApp>> GetMonitoredAppsAsync();
        Task<MonitoredApp?> GetMonitoredAppByIdAsync(Guid id);
        Task<MonitoredApp> AddMonitoredAppAsync(AddMonitoredApp addMonitoredApp);
        Task<MonitoredApp?> UpdateMonitoredAppAsync(Guid id, UpdateMonitoredApp updateMonitoredApp);
        Task<MonitoredApp?> RemoveMonitoredAppAsync(Guid id);
    }
}
