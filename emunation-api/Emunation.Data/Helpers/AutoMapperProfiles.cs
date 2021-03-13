using AutoMapper;
using Emunation.Data.Entities;
using Emunation.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emunation.Data.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserGameProfileCreateModel, UserGameProfile>();
            CreateMap<UserGameProfile, UserGameProfileModel>().ReverseMap();
            CreateMap<UserSave, UserSaveModel>().ReverseMap();
            CreateMap<Game, GameModel>().ReverseMap();
        }
    }
}
