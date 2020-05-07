using Emunation.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emunation.Services.Interfaces
{
    public interface IGameService
    {
        Task<Game> GetGames();
        Task<Game> GetGame(string name);

        void AddGame(Game game);
    }
}
