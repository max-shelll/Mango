using AutoMapper;
using Mango.Services.ProductAPI.DAL.Models;
using Mango.Services.ProductAPI.DAL.Models.Dtos;

namespace Mango.Services.ProductAPI.BLL
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<ProductDto, Product>().ReverseMap();
		}
	}
}
