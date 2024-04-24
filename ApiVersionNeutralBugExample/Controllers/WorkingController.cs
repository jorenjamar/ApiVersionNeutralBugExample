using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersionNeutralBugExample.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiController]
    [Route("[controller]")]
    public class WorkingController : ControllerBase
    {
        private readonly ILogger<WorkingController> _logger;

        public WorkingController(ILogger<WorkingController> logger)
        {
            _logger = logger;
        }

        [HttpPost("task")]
        public string DoTask()
        {
            return "task";
        }

        [HttpGet("{id}")]
        public string DoId(string id)
        {
            return "The id is " + id;
        }
    }
}