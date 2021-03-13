using Emunation.Data.Entities;
using Emunation.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emunation.Services.Interfaces
{
    public interface IGameService
    {
        Task<UserGameProfileModel> GetGameProfileById();
        Task<List<UserGameProfileModel>> GetAllGameProfilesByUserId(Guid userId);
        Task<UserGameProfileModel> AddUserGameProfile(UserGameProfileCreateModel profileToAdd);
    }
}
