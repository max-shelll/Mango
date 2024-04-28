using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.BLL.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IdentityUser user, IEnumerable<string> roles);
    }
}
