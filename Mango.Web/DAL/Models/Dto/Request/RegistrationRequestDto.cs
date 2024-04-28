using System.ComponentModel.DataAnnotations;

namespace Mango.Web.DAL.Models.Dto.Request
{
    public class RegistrationRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public string RoleName { get; set; }
    }
}
