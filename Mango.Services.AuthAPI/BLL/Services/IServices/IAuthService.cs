﻿using Mango.Services.AuthAPI.DAL.Models.Dtos.Request;
using Mango.Services.AuthAPI.DAL.Models.Dtos.Response;

namespace Mango.Services.AuthAPI.BLL.Services.IServices
{
	public interface IAuthService
	{
		Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
		Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto);
		Task<bool> AssignRoleAsync(RoleAssignRequestDto roleAssignRequestDto);
	}
}
