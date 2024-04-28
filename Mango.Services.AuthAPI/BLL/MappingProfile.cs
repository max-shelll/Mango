using AutoMapper;
using Mango.Services.AuthAPI.DAL.Models.Dto;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, IdentityUser>().ReverseMap();
        }
    }
}
