using HealthZoneAPI.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthZoneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightController(IWeightService _weightService) : ControllerBase
    {
        // GET: api/<WeightController>
        [HttpGet("get-all-active")]
        public async Task<IActionResult> GetActive()
        {

            var res = await _weightService.GetAllActive();

            return Ok(res);
        }

        [HttpPost("log-daily-weight")]
        public async Task<IActionResult> CreateDailyWeightValue([FromBody] double weight)
        {
            var res = await _weightService.CreateDailyWeightValue(weight);
            return Ok(res);
        }
    }
}
