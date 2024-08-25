namespace PerformanceMonitoringTool.ApiService.Models.DTOs
{
    public class MonitoredAppDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AppId { get; set; }
        public DateTime LastChecked { get; set; }
        public bool IsOnline { get; set; }
    }
}
