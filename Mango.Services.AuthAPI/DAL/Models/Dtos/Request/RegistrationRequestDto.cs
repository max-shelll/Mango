namespace Mango.Services.AuthAPI.DAL.Models.Dtos.Request
{
	public class RegistrationRequestDto
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
	}
}
