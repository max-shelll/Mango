namespace Mango.Services.AuthAPI.DAL.Models.Configs
{
	public class JwtOptions
	{
		public string Secret { get; set; } = string.Empty;
		public string Issuer { get; set; } = string.Empty;
		public string Audience { get; set; } = string.Empty;
	}
}
