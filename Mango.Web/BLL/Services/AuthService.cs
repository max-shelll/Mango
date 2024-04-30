using Mango.Web.BLL.Services.IServices;
using Mango.Web.BLL.Utilities;
using Mango.Web.DAL.Enums;
using Mango.Web.DAL.Models.Dtos.Request;
using Mango.Web.DAL.Models.Dtos.Response;

namespace Mango.Web.BLL.Services
{
	public class AuthService : IAuthService
	{
		private readonly IBaseService _baseService;

		public AuthService(IBaseService baseService)
		{
			_baseService = baseService;
		}

		public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.POST,
				Url = SD.AuthAPIBase + $"/api/auth/login",
				Data = loginRequestDto
			}, withBearer: false);
		}

		public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.POST,
				Url = SD.AuthAPIBase + $"/api/auth/register",
				Data = registrationRequestDto
			}, withBearer: false);
		}

		public async Task<ResponseDto> AssignRoleAsync(RoleAssignRequestDto roleAssignRequestDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.POST,
				Url = SD.AuthAPIBase + $"/api/auth/assignRole",
				Data = roleAssignRequestDto
			});
		}
	}
}
