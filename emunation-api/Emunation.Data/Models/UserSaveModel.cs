using System;
using System.Collections.Generic;
using System.Text;

namespace Emunation.Data.Models
{
    public class UserSaveModel
    {
        public int UserSaveId { get; set; }
        public int UserGameProfileId { get; set; }
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public DateTime LastPlayed { get; set; }
    }
}
