namespace PerformanceMonitoringTool.Web
{
    public class MonitoredAppService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MonitoredAppService> _logger;

        public MonitoredAppService(HttpClient httpClient, ILogger<MonitoredAppService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<HttpResponseMessage> AddMonitoredApp(AddMonitoredApp app)
        {
            _logger.LogInformation($"App to send - Name: {app.Name}, Id: {app.AppId}");
            return await _httpClient.PostAsJsonAsync("api/monitoredapps", app);
        }

        public async Task<List<MonitoredAppDto>> GetAllMonitoredApps()
        {
            return await _httpClient.GetFromJsonAsync<List<MonitoredAppDto>>("api/monitoredapps");
        }
    }
}
