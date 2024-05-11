using Mango.Services.AuthAPI.BLL.Services.IServices;
using Mango.Services.AuthAPI.DAL.Models.Dtos.Request;
using Mango.Services.AuthAPI.DAL.Models.Dtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.BLL.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthAPIController : ControllerBase
	{
		private readonly IAuthService _authService;
		private ResponseDto _response;

		public AuthAPIController(IAuthService authService)
		{
			_authService = authService;
			_response = new();
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
		{
			try
			{
				var loginResponse = await _authService.LoginAsync(model);

				if (loginResponse.User == null)
				{
					_response.IsSuccess = false;
					_response.Message = "Username or password is incorrect";

					return BadRequest(_response);
				}

				_response.Result = loginResponse;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;

				return BadRequest(_response);
			}
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
		{
			try
			{
				var errorMessage = await _authService.RegisterAsync(model);

				if (!string.IsNullOrEmpty(errorMessage))
				{
					_response.IsSuccess = false;
					_response.Message = errorMessage;

					return BadRequest(_response);
				}

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;

				return BadRequest(_response);
			}
		}

		[HttpPost("assignRole")]
		public async Task<IActionResult> AssignRole([FromBody] RoleAssignRequestDto model)
		{
			try
			{
				var assignRoleSuccessful = await _authService.AssignRoleAsync(model);

				if (!assignRoleSuccessful)
				{
					_response.IsSuccess = false;
					_response.Message = "Error encountered";

					return BadRequest(_response);
				}

				return Ok(_response);

			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;

				return BadRequest(_response);
			}
		}
	}
}
