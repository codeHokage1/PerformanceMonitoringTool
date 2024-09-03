using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerformanceMonitoringTool.ApiService.Models.DTOs;
using PerformanceMonitoringTool.ApiService.Models.Entities;
using PerformanceMonitoringTool.ApiService.Repositories;

namespace PerformanceMonitoringTool.ApiService.Controllers
{
    [Route("api/[controller]")] // https://localhost:5001/api/MonitoredApps
    [ApiController]
    public class MonitoredAppsController : ControllerBase
    {
        private readonly IMonitoredApps _monitoredAppsRepo;
        private readonly IMapper _mapper;

        public MonitoredAppsController(IMonitoredApps monitoredAppsRepo, IMapper mapper)
        {
            _monitoredAppsRepo = monitoredAppsRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMonitoredAppsAsync()
        {
            try
            {
                var monitoredApps = await _monitoredAppsRepo.GetMonitoredAppsAsync();
                var monitoredAppsDto = _mapper.Map<List<MonitoredAppDto>>(monitoredApps);

                return Ok(monitoredAppsDto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonitoredAppByIdAsync(Guid id)
        {
            try
            {
                var monitoredApp = await _monitoredAppsRepo.GetMonitoredAppByIdAsync(id);

                if (monitoredApp == null)
                {
                    return NotFound();
                }

                var monitoredAppDto = _mapper.Map<MonitoredAppDto>(monitoredApp);

                return Ok(monitoredAppDto);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMonitoredApp(AddMonitoredApp addMonitoredApp)
        {
            try
            {
                var addApp = _mapper.Map<MonitoredApp>(addMonitoredApp);
                addApp = await _monitoredAppsRepo.AddMonitoredAppAsync(addApp);
                var addedApp = _mapper.Map<MonitoredAppDto>(addApp);

                return Ok(addedApp);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonitoredApp(Guid id)
        {
            try
            {
                var deleteApp = await _monitoredAppsRepo.RemoveMonitoredAppAsync(id);
                if (deleteApp == null)
                {
                    return NotFound();
                }

                return NoContent();
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMonitoredApp(Guid id, UpdateMonitoredApp updateMonitoredApp)
        {
            try
            {
                var updateApp = _mapper.Map<MonitoredApp>(updateMonitoredApp);
                var updatedApp = await _monitoredAppsRepo.UpdateMonitoredAppAsync(id, updateApp);

                if (updatedApp == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<MonitoredAppDto>(updateApp));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
