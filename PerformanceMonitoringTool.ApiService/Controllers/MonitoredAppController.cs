using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerformanceMonitoringTool.ApiService.Models.DTOs;
using PerformanceMonitoringTool.ApiService.Repositories;

namespace PerformanceMonitoringTool.ApiService.Controllers
{
    [Route("api/[controller]")] // https://localhost:5001/api/MonitoredApp
    [ApiController]
    public class MonitoredAppController : ControllerBase
    {
        private readonly IMonitoredApps _monitoredAppsRepo;
        private readonly IMapper _mapper;

        public MonitoredAppController(IMonitoredApps monitoredAppsRepo, IMapper mapper)
        {
            _monitoredAppsRepo = monitoredAppsRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMonitoredAppsAsync()
        {
            var monitoredApps = await _monitoredAppsRepo.GetMonitoredAppsAsync();
            var monitoredAppsDto = _mapper.Map<List<MonitoredAppDto>>(monitoredApps);

            return Ok(monitoredAppsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonitoredAppByIdAsync(Guid id)
        {
            var monitoredApp = await _monitoredAppsRepo.GetMonitoredAppByIdAsync(id);

            if (monitoredApp == null)
            {
                return NotFound();
            }

            var monitoredAppDto = _mapper.Map<MonitoredAppDto>(monitoredApp);

            return Ok(monitoredAppDto);
        }
    }
}
