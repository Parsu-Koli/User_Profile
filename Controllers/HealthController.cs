using Microsoft.AspNetCore.Mvc;

namespace UploadForm_Project.Controllers
{
    
    public class HealthController : ControllerBase
    {
       
        public IActionResult Health()
        {
            return Ok(new
            {
                Status = "Healthy",
                Time = DateTime.UtcNow
            });
        }
    }
}
