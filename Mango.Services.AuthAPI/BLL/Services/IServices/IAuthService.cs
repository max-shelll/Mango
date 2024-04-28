using Mango.Services.AuthAPI.DAL.Models.Dto.Request;
using Mango.Services.AuthAPI.DAL.Models.Dto.Response;

namespace Mango.Services.AuthAPI.BLL.Services.IServices
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<bool> AssignRole(RoleAssignRequestDto roleAssignRequestDto);
    }
}
