using System.ComponentModel.DataAnnotations;

namespace PerformanceMonitoringTool.Web.Components.Models
{
    public class FormData
    {
        [Required]
        [MinLength(5, ErrorMessage = "App Name must be more than 5 characters")]
        public string AppName { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "App URL must be more than 5 characters")]
        public string AppURL { get; set; }
    }
}
