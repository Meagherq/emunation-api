using Emunation.Data.Contexts;
using Emunation.Data.Models;
using Emunation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Emunation.Services.Concretes
{
    public class GameService : IGameService
    {
        private readonly DataContext _context;
        public GameService(DataContext context)
        {
            _context = context;
        }

        public async Task<Game> GetGames()
        {
            var result = (Game) _context.Games.Where(game => game.Name != null);
            

            return result;
        }
        public async Task<Game> GetGame(String name)
        {
            var result = (Game) _context.Games.Where(game => game.Name == name);
            

            return result;
        }
        public async void AddGame(Game game)
        {
            _context.Games.Add(game);
            

            return;
        }
    }
}
