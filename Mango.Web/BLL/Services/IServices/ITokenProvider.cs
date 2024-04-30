﻿namespace Mango.Web.BLL.Services.IServices
{
	public interface ITokenProvider
	{
		void SetToken(string token);
		string? GetToken();
		void ClearToken();
	}
}
