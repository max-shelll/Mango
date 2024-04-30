using Mango.Web.BLL.Services.IServices;
using Mango.Web.DAL.Enums;
using Mango.Web.DAL.Models.Dtos.Request;
using Mango.Web.DAL.Models.Dtos.Response;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Mango.Web.BLL.Services
{
	public class BaseService : IBaseService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ITokenProvider _tokenProvider;

		public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
		{
			_httpClientFactory = httpClientFactory;
			_tokenProvider = tokenProvider;

		}

		public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
		{
			try
			{
				var client = _httpClientFactory.CreateClient("MangoAPI");
				var message = new HttpRequestMessage();

				message.Headers.Add("Accept", "application/json");

				if (withBearer)
				{
					var token = _tokenProvider.GetToken();
					message.Headers.Add("Authorization", $"Bearer {token}");
				}

				message.RequestUri = new Uri(requestDto.Url);

				if (requestDto.Data != null)
					message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");

				message.Method = requestDto.ApiType switch
				{
					ApiType.POST => HttpMethod.Post,
					ApiType.PUT => HttpMethod.Put,
					ApiType.DELETE => HttpMethod.Delete,
					_ => HttpMethod.Get,
				};

				var apiResponse = await client.SendAsync(message);

				switch (apiResponse.StatusCode)
				{
					case HttpStatusCode.NotFound:
						return new() { IsSuccess = false, Message = "Not Found" };
					case HttpStatusCode.Forbidden:
						return new() { IsSuccess = false, Message = "Access Denied" };
					case HttpStatusCode.Unauthorized:
						return new() { IsSuccess = false, Message = "Unauthorize" };
					case HttpStatusCode.InternalServerError:
						return new() { IsSuccess = false, Message = "Internal Server Error" };
					default:
						var apiContent = await apiResponse.Content.ReadAsStringAsync();
						var apiResonseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
						return apiResonseDto;
				}
			}
			catch (Exception ex)
			{
				var dto = new ResponseDto()
				{
					IsSuccess = false,
					Message = ex.Message.ToString()
				};
				return dto;
			}
		}
	}
}
