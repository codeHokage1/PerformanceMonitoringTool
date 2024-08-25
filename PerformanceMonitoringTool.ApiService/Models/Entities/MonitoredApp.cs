namespace PerformanceMonitoringTool.ApiService.Models.Entities
{
    public class MonitoredApp
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AppId { get; set; }
        public DateTime LastChecked { get; set; }
        public bool IsOnline { get; set; }
    }
}
