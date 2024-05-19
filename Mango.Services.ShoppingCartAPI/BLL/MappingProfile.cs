using AutoMapper;
using Mango.Services.ShoppingCartAPI.DAL.Models;
using Mango.Services.ShoppingCartAPI.DAL.Models.Dtos;

namespace Mango.Services.ShoppingCartAPI.BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
        }
    }
}
