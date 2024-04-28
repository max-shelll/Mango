using Mango.Web.DAL.Models.Dto.Request;
using Mango.Web.DAL.Models.Dto.Response;
using Microsoft.AspNetCore.Identity.Data;

namespace Mango.Web.BLL.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto> AssignRoleAsync(RoleAssignRequestDto roleAssignRequestDto);
    }
}
