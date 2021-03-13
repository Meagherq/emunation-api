using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Emunation.Data.Entities
{
    public class UserGameProfile
    {
        public UserGameProfile()
        {
            Saves = new HashSet<UserSave>();
        }

        public int UserGameProfileId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public virtual ICollection<UserSave> Saves { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsRecent { get; set; }
    }
}
