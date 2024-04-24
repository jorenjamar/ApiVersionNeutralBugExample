using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersionNeutralBugExample.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiController]
    [Route("[controller]")]
    public class BrokenController : ControllerBase
    {
        private readonly ILogger<BrokenController> _logger;

        public BrokenController(ILogger<BrokenController> logger)
        {
            _logger = logger;
        }

        [HttpPost("task")]
        public string DoTask()
        {
            return "task";
        }

        [ApiVersionNeutral]
        [HttpGet("{id}")]
        public string DoId(string id)
        {
            return "The id is " + id;
        }
    }
}