using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Emunation.Data.Entities
{
    public class Game
    {
        public int GameId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
        public virtual UserGameProfile UserGameProfile { get; set; }
    }
}
