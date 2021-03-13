using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emunation.Data.Entities;
using Emunation.Data.Models;
using Emunation.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace emunation_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserGameProfileController : ControllerBase
    {
        private readonly ILogger<UserGameProfileController> _logger;
        private readonly IGameService _gameService;

        public UserGameProfileController(ILogger<UserGameProfileController> logger, IGameService gameService)
        {
            _logger = logger;
            _gameService = gameService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _gameService.GetAllGameProfilesByUserId(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var result = await _gameService.GetAllGameProfilesByUserId();

        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }

        //    //return Ok("success");
        //}

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddUserGameProfile(UserGameProfileCreateModel profileToAdd)
        {
            var result = await _gameService.AddUserGameProfile(profileToAdd);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
