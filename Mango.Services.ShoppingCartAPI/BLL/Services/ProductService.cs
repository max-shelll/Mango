using Mango.Services.ShoppingCartAPI.BLL.Services.IServices;
using Mango.Services.ShoppingCartAPI.DAL.Models.Dtos;
using Newtonsoft.Json;

namespace Mango.Services.ShoppingCartAPI.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var client = _httpClientFactory.CreateClient("Product");

            var response = await client.GetAsync($"/api/product");
            var apiContent = await response.Content.ReadAsStringAsync();

            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(resp.Result));
            }

            return new List<ProductDto>();
        }
    }
}
