using Mango.Web.DAL.Models.Dto.Request;
using Mango.Web.DAL.Models.Dto.Response;

namespace Mango.Web.BLL.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
