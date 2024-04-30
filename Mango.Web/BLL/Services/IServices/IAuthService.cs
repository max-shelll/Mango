using Mango.Web.DAL.Models.Dtos.Request;
using Mango.Web.DAL.Models.Dtos.Response;

namespace Mango.Web.BLL.Services.IServices
{
	public interface IAuthService
	{
		Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
		Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
		Task<ResponseDto> AssignRoleAsync(RoleAssignRequestDto roleAssignRequestDto);
	}
}
