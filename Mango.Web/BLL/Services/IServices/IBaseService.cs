using Mango.Web.DAL.Models.Dtos.Request;
using Mango.Web.DAL.Models.Dtos.Response;

namespace Mango.Web.BLL.Services.IServices
{
	public interface IBaseService
	{
		Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
	}
}
