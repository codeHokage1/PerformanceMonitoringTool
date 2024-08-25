using Microsoft.EntityFrameworkCore;
using PerformanceMonitoringTool.ApiService.Data;
using PerformanceMonitoringTool.ApiService.Models.DTOs;
using PerformanceMonitoringTool.ApiService.Models.Entities;

namespace PerformanceMonitoringTool.ApiService.Repositories
{
    public class MonitoredAppRepository : IMonitoredApps
    {
        private readonly AppDbContext _dbContext;

        public MonitoredAppRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<MonitoredApp> AddMonitoredAppAsync(AddMonitoredApp addMonitoredApp)
        {
            throw new NotImplementedException();
        }

        public Task<MonitoredApp?> GetMonitoredAppByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MonitoredAppDto>> GetMonitoredAppsAsync()
        {
            var monitoredApps = await _dbContext.MonitoredApps.ToListAsync();

            var monitoredAppsDto = new List<MonitoredAppDto>();

            foreach (var monitoredApp in monitoredApps)
            {
                monitoredAppsDto.Add(new MonitoredAppDto
                {
                    Id = monitoredApp.Id,
                    Name = monitoredApp.Name,
                    AppId = monitoredApp.AppId,
                    LastChecked = monitoredApp.LastChecked,
                    IsOnline = monitoredApp.IsOnline
                });
            }

            return monitoredAppsDto;
        }

        public Task<MonitoredApp?> RemoveMonitoredAppAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<MonitoredApp?> UpdateMonitoredAppAsync(Guid id, UpdateMonitoredApp updateMonitoredApp)
        {
            throw new NotImplementedException();
        }
    }
}
