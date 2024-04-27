using Mango.Web.DAL.Models.Dto;

namespace Mango.Web.BLL.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
