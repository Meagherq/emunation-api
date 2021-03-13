using AutoMapper;
using Emunation.Data.Contexts;
using Emunation.Data.Entities;
using Emunation.Data.Models;
using Emunation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emunation.Services.Concretes
{
    public class GameService : IGameService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public GameService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserGameProfileModel> GetGameProfileById()
        {
            var result = await _context.UserGameProfiles.Include(x => x.Game).Include(x => x.Saves).FirstOrDefaultAsync(x => x.UserGameProfileId == 1);

            var mappedModel = _mapper.Map<UserGameProfileModel>(result);

            return mappedModel;
        }

        public async Task<List<UserGameProfileModel>> GetAllGameProfilesByUserId(Guid userId)
        {
            var result = await _context.UserGameProfiles
                .Include(x => x.Game)
                .Include(x => x.Saves)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var mappedModel = _mapper.Map<List<UserGameProfileModel>>(result);

            return mappedModel;
        }

        public async Task<UserGameProfileModel> AddUserGameProfile(UserGameProfileCreateModel profileToAdd)
        {
            var entityToAdd = _mapper.Map<UserGameProfile>(profileToAdd);

            var result = await _context.UserGameProfiles
                .AddAsync(entityToAdd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Error adding game to profile" + ex.Message);
            }

            var mappedModel = _mapper.Map<UserGameProfileModel>(result.Entity);

            return mappedModel;
        }
    }
}
