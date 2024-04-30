using Mango.Web.BLL.Services.IServices;
using Mango.Web.BLL.Utilities;

namespace Mango.Web.BLL.Services
{
	public class TokenProvider : ITokenProvider
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public TokenProvider(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public void SetToken(string token)
		{
			_contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
		}

		public string? GetToken()
		{
			string? token = null;
			var hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);

			return hasToken is true ? token : null;
		}

		public void ClearToken()
		{
			_contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
		}
	}
}
