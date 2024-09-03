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
        public async Task<MonitoredApp> AddMonitoredAppAsync(MonitoredApp addMonitoredApp)
        {
            await _dbContext.MonitoredApps.AddAsync(addMonitoredApp);
            await _dbContext.SaveChangesAsync();
            return addMonitoredApp;
        }

        public Task<MonitoredApp?> GetMonitoredAppByAppIdAsync(string appId)
        {
            var foundMonitoredApp = _dbContext.MonitoredApps.FirstOrDefaultAsync(x => x.AppId == appId);
            if(foundMonitoredApp == null)
            {
                return null;
            }
            return foundMonitoredApp;
        }

        public async Task<MonitoredApp?> GetMonitoredAppByIdAsync(Guid id)
        {
            var foundMonitoredApp = await _dbContext.MonitoredApps.FirstOrDefaultAsync(x => x.Id == id);
            if(foundMonitoredApp == null)
            {
                return null;
            }

            return foundMonitoredApp;
        }

        public async Task<List<MonitoredApp>> GetMonitoredAppsAsync()
        {
            return await _dbContext.MonitoredApps.ToListAsync();

        }

        public async Task<MonitoredApp?> RemoveMonitoredAppAsync(Guid id)
        {
            var foundApp = await _dbContext.MonitoredApps.FindAsync(id);
            if(foundApp == null)
            {
                return null;
            }

            _dbContext.MonitoredApps.Remove(foundApp);
            await _dbContext.SaveChangesAsync();

            return foundApp;
        }

        public async Task<MonitoredApp?> UpdateMonitoredAppAsync(Guid id, MonitoredApp updateMonitoredApp)
        {
            var foundApp = await _dbContext.MonitoredApps.FindAsync(id);
            if(foundApp == null)
            {
                return null;
            }

            foundApp.Name = updateMonitoredApp.Name;
            foundApp.AppId = updateMonitoredApp.AppId;
            foundApp.LastChecked = updateMonitoredApp.LastChecked;
            foundApp.IsOnline = updateMonitoredApp.IsOnline;
            await _dbContext.SaveChangesAsync();

            return foundApp;
        }
    }
}
