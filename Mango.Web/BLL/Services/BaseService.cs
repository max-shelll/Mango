using Mango.Web.BLL.Services.IServices;
using Mango.Web.DAL.Enums;
using Mango.Web.DAL.Models.Dto;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Mango.Web.BLL.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("MangoAPI");
                var message = new HttpRequestMessage();

                message.Headers.Add("Accept", "application/json");

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
