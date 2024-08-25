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
