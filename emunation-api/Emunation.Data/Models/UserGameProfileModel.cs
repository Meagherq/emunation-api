using Emunation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emunation.Data.Models
{
    public class UserGameProfileModel
    {
        public int UserGameProfileId { get; set; }
        public int GameId { get; set; }
        public GameModel Game { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsRecent { get; set; }
    }
}
