using Microsoft.AspNetCore.Mvc;

namespace Thu_y.Controllers
{
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            var info = $"[Thu y] WebApi Application is running normally on {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}...";
            return Ok(info);
        }

        [HttpGet("/version")]
        public IActionResult GetVersion()
        {
            var version = typeof(Program).Assembly.GetName().Version?.ToString();
            return Ok(version);
        }
    }
}
