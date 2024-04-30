using System.ComponentModel.DataAnnotations;

namespace Mango.Web.DAL.Models.Dtos.Request
{
	public class LoginRequestDto
	{
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
