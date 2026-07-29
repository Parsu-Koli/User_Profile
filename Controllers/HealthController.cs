using Microsoft.AspNetCore.Mvc;

namespace UploadForm_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Status = "Healthy",
                Time = DateTime.UtcNow
            });
        }
    }
}