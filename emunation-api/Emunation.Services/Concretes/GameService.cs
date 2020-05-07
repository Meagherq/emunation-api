using Emunation.Data.Contexts;
using Emunation.Data.Models;
using Emunation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emunation.Services.Concretes
{
    public class GameService : IGameService
    {
        private readonly DataContext _context;
        public GameService(DataContext context)
        {
            _context = context;
        }

        public async Task<Game> GetGame()
        {
            var result = await _context.Games;

            return result;
        }
    }
}
