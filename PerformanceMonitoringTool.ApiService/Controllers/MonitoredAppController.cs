using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerformanceMonitoringTool.ApiService.Repositories;

namespace PerformanceMonitoringTool.ApiService.Controllers
{
    [Route("api/[controller]")] // https://localhost:5001/api/MonitoredApp
    [ApiController]
    public class MonitoredAppController : ControllerBase
    {
        private readonly IMonitoredApps _monitoredAppsRepo;

        public MonitoredAppController(IMonitoredApps monitoredAppsRepo)
        {
            _monitoredAppsRepo = monitoredAppsRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetMonitoredAppsAsync()
        {
            var monitoredApps = await _monitoredAppsRepo.GetMonitoredAppsAsync();

            return Ok(monitoredApps);
        }
    }
}
