using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emunation.Services.Interfaces;
using Emunation.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace emunation_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IGameService _gameService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            var result = await _gameService.GetGames();
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
        [Authorize]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetGame(String name)
        {
            var result = await _gameService.GetGame(name);
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
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Game>> PostTodoItem(Game gameItem)
        {

            var random = new Random();
            gameItem.ID = random.Next();


            _gameService.AddGame(gameItem);
            
            return CreatedAtAction("GameItem", new { id = gameItem.ID }, gameItem);
        }
    }
}
