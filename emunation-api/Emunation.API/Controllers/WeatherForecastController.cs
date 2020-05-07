using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emunation.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace emunation_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IGameService _gameService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = _gameService.GetGame();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
         
            //return Ok("success");
        }
    }
}
