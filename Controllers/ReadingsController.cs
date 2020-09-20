using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nviromon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Nviromon.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReadingsController : ControllerBase
    {
        private readonly ILogger<ReadingsController> logger;
        private readonly DataService dataService;

        public ReadingsController(DataService dataService, ILogger<ReadingsController> logger)
        {
            this.dataService = dataService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetReadings()
        {
            var rng = new Random();

            var readings = await dataService.Readings.ToListAsync();

            return Ok(readings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReading(Guid id)
        {
            var rng = new Random();

            var value = await dataService.Readings.FirstOrDefaultAsync(x => x.Id == id);

            return Ok(value);
        }
    }
}
