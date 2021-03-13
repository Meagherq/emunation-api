using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Emunation.Data.Entities
{
    public class User
    {
        public User()
        {
            UserGameProfiles = new HashSet<UserGameProfile>();
        }

        public Guid UserId { get; set; }
        public virtual ICollection<UserGameProfile> UserGameProfiles { get; set; }
    }
}
