using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Emunation.Data.Entities
{
    public class UserSave
    {
        public int UserSaveId { get; set; }
        public int UserGameProfileId { get; set; }
        public virtual UserGameProfile UserGameProfile { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public DateTime LastPlayed { get; set; }
    }
}
