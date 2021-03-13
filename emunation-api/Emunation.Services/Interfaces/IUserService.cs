using Emunation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emunation.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(Guid objectId);
    }
}
