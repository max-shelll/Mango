using AutoMapper;
using Mango.Services.AuthAPI.BLL.Services.IServices;
using Mango.Services.AuthAPI.DAL.Models.Dto;
using Mango.Services.AuthAPI.DAL.Models.Dto.Request;
using Mango.Services.AuthAPI.DAL.Models.Dto.Response;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        private readonly IMapper _mapper;

        public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;

            _jwtTokenGenerator = jwtTokenGenerator;

            _mapper = mapper;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);

                var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (user == null || isValid == false)
                {
                    return new LoginResponseDto() { User = null, Token = "" };
                }

                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenGenerator.GenerateToken(user, roles);

                var userDto = _mapper.Map<UserDto>(user);

                var loginResponseDto = new LoginResponseDto()
                {
                    User = userDto,
                    Token = token
                };

                return loginResponseDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            var user = new IdentityUser()
            {
                UserName = registrationRequestDto.UserName,
                Email = registrationRequestDto.Email,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);

                if (result.Succeeded)
                {
                    return string.Empty;
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> AssignRole(RoleAssignRequestDto roleAssignRequestDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(roleAssignRequestDto.Email);

                if (user == null)
                {
                    return false;
                }
                else if (await _roleManager.RoleExistsAsync(roleAssignRequestDto.RoleName) == false)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleAssignRequestDto.RoleName));
                }

                await _userManager.AddToRoleAsync(user, roleAssignRequestDto.RoleName);
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
