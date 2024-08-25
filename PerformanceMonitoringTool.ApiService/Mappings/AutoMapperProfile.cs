using AutoMapper;
using PerformanceMonitoringTool.ApiService.Models.DTOs;
using PerformanceMonitoringTool.ApiService.Models.Entities;

namespace PerformanceMonitoringTool.ApiService.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MonitoredApp, MonitoredAppDto>().ReverseMap();
            CreateMap<AddMonitoredApp, MonitoredApp>().ReverseMap();
            CreateMap<UpdateMonitoredApp, MonitoredApp>().ReverseMap();
        }
    }
}
