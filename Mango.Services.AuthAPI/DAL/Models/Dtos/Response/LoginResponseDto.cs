namespace Mango.Services.AuthAPI.DAL.Models.Dtos.Response
{
	public class LoginResponseDto
	{
		public UserDto User { get; set; }
		public string Token { get; set; }
	}
}
